using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public static ResultDto Success(string message = "Success") => new()
        {
            IsSuccess = true,
            Message = message
        };

        public static ResultDto Fail(string message, IEnumerable<string>? errors = null) => new()
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}
namespace YourTrips.Core.DTOs
{
    public class ResultDto<T> : ResultDto
    {
        public T Data { get; set; }

        public static ResultDto<T> Success(T data, string message = "Success") => new()
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };

        public static new ResultDto<T> Fail(string message, IEnumerable<string>? errors = null) => new()
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}
