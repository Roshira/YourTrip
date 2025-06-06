using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.Admin.Data;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Admin;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure.Services.Admin.Data
{
    /// <summary>
    /// Service for comparing sequential and parallel sorting methods based on the number of routes per user.
    /// </summary>
    public class UserSortingService : IUserSortingService
    {
        private readonly YourTripsDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSortingService"/> class.
        /// </summary>
        /// <param name="context">The database context used to retrieve users and their routes.</param>
        public UserSortingService(YourTripsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Compares the execution time and results of sequential and parallel merge sort algorithms
        /// for sorting users by their number of routes.
        /// </summary>
        /// <returns>A <see cref="SortingComparisonResult"/> containing the results of both sorting methods.</returns>
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

        /// <summary>
        /// Performs a sequential merge sort on the user list based on route count.
        /// </summary>
        /// <param name="users">The list of users to sort.</param>
        /// <returns>A <see cref="SortingResult"/> containing sorted users and execution time.</returns>
        private Task<SortingResult> SortSequential(List<User> users)
        {
            var stopwatch = Stopwatch.StartNew();

            var userRoutes = users
                .Select(u => new UserRoutes
                {
                    User = MapToUserDto(u),
                    RoutesCount = u.Routes?.Count ?? 0
                })
                .ToList();

            userRoutes = MergeSort(userRoutes);

            stopwatch.Stop();

            return Task.FromResult(new SortingResult
            {
                UserRoutes = userRoutes,
                ExecutionTimeMs = stopwatch.Elapsed.TotalMilliseconds,
                MethodName = "Sequential (MergeSort)"
            });
        }

        /// <summary>
        /// Performs a parallel merge sort on the user list based on route count.
        /// </summary>
        /// <param name="users">The list of users to sort.</param>
        /// <returns>A <see cref="SortingResult"/> containing sorted users and execution time.</returns>
        private async Task<SortingResult> SortParallel(List<User> users)
        {
            var stopwatch = Stopwatch.StartNew();

            var userRoutes = users
                .Select(u => new UserRoutes
                {
                    User = MapToUserDto(u),
                    RoutesCount = u.Routes?.Count ?? 0
                })
                .ToList();

            var sorted = await ParallelMergeSort(userRoutes);

            stopwatch.Stop();
            var timeInMs = stopwatch.Elapsed.TotalMilliseconds;
            return new SortingResult
            {
                UserRoutes = sorted,
                ExecutionTimeMs = stopwatch.Elapsed.TotalMilliseconds,
                MethodName = "Parallel (MergeSort)"
            };
        }

        /// <summary>
        /// Recursively sorts a list using the merge sort algorithm.
        /// </summary>
        /// <param name="list">The list to sort.</param>
        /// <returns>The sorted list.</returns>
        private List<UserRoutes> MergeSort(List<UserRoutes> list)
        {
            if (list.Count <= 1) return list;

            int mid = list.Count / 2;
            var left = MergeSort(list.GetRange(0, mid));
            var right = MergeSort(list.GetRange(mid, list.Count - mid));

            return Merge(left, right);
        }

        /// <summary>
        /// Merges two sorted lists into a single sorted list in descending order by RoutesCount.
        /// </summary>
        /// <param name="left">The left sorted list.</param>
        /// <param name="right">The right sorted list.</param>
        /// <returns>The merged sorted list.</returns>
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

        /// <summary>
        /// Performs a parallel merge sort on a list with a specified threshold.
        /// </summary>
        /// <param name="list">The list to sort.</param>
        /// <param name="threshold">The threshold below which sequential sort is used.</param>
        /// <returns>The sorted list.</returns>
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

        /// <summary>
        /// Maps a <see cref="User"/> entity to a <see cref="UserDto"/>.
        /// </summary>
        /// <param name="user">The user entity to map.</param>
        /// <returns>The mapped <see cref="UserDto"/>.</returns>
        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
