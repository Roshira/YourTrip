using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Admin.Data;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Admin;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure.Services.Admin.Data
{
    public class UserSortingService : IUserSortingService
    {
        private readonly YourTripsDbContext _context;

        public UserSortingService(YourTripsDbContext context)
        {
            _context = context;
        }

        public async Task<SortingComparisonResult> CompareSortingMethods()
        {
            var users = await _context.Users
                .Include(u => u.Routes)
                .AsNoTracking()
                .ToListAsync();

            var result = new SortingComparisonResult
            {
                SequentialResult = await SortSequential(users),
                ParallelResult = await SortParallel(users)
            };

            return result;
        }

        private Task<SortingResult> SortSequential(List<User> users)
        {
            var stopwatch = Stopwatch.StartNew();

            var userRoutes = users
                .Select(u => new UserRoutes
                {
                    User = u,
                    RoutesCount = u.Routes?.Count ?? 0
                })
                .ToList();

            userRoutes = MergeSort(userRoutes);

            stopwatch.Stop();

            return Task.FromResult(new SortingResult
            {
                UserRoutes = userRoutes,
                ExecutionTimeMs = stopwatch.ElapsedMilliseconds,
                MethodName = "Sequential (MergeSort)"
            });
        }

        private async Task<SortingResult> SortParallel(List<User> users)
        {
            var stopwatch = Stopwatch.StartNew();

            var userRoutes = users
                .Select(u => new UserRoutes
                {
                    User = u,
                    RoutesCount = u.Routes?.Count ?? 0
                })
                .ToList();

            var sorted = await ParallelMergeSort(userRoutes);

            stopwatch.Stop();

            return new SortingResult
            {
                UserRoutes = sorted,
                ExecutionTimeMs = stopwatch.ElapsedMilliseconds,
                MethodName = "Parallel (MergeSort)"
            };
        }
        private List<UserRoutes> MergeSort(List<UserRoutes> list)
        {
            if (list.Count <= 1) return list;

            int mid = list.Count / 2;
            var left = MergeSort(list.GetRange(0, mid));
            var right = MergeSort(list.GetRange(mid, list.Count - mid));

            return Merge(left, right);
        }

        private List<UserRoutes> Merge(List<UserRoutes> left, List<UserRoutes> right)
        {
            var result = new List<UserRoutes>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i].RoutesCount >= right[j].RoutesCount)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }

            while (i < left.Count) result.Add(left[i++]);
            while (j < right.Count) result.Add(right[j++]);

            return result;
        }
        private async Task<List<UserRoutes>> ParallelMergeSort(List<UserRoutes> list, int threshold = 1000)
        {
            if (list.Count <= 1)
                return list;

            if (list.Count < threshold)
                return MergeSort(list);

            int mid = list.Count / 2;
            var leftTask = Task.Run(() => ParallelMergeSort(list.GetRange(0, mid), threshold));
            var rightTask = Task.Run(() => ParallelMergeSort(list.GetRange(mid, list.Count - mid), threshold));

            await Task.WhenAll(leftTask, rightTask);

            return Merge(leftTask.Result, rightTask.Result);
        }

    }
}
