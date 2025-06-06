using System;
using System.Collections.Generic;

namespace YourTrips.Application.DTOs
{
    /// <summary>
    /// Base class for operation results with status, message, and possible errors.
    /// </summary>
    public class ResultDto
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message describing the result of the operation.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Collection of error messages if any errors occurred.
        /// </summary>
        public IEnumerable<string>? Errors { get; set; }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        /// <param name="message">Optional success message.</param>
        /// <returns>A successful <see cref="ResultDto"/> instance.</returns>
        public static ResultDto Success(string message = "Success") => new()
        {
            IsSuccess = true,
            Message = message
        };

        /// <summary>
        /// Creates a failure result.
        /// </summary>
        /// <param name="message">Failure message.</param>
        /// <param name="errors">Optional list of error messages.</param>
        /// <returns>A failure <see cref="ResultDto"/> instance.</returns>
        public static ResultDto Fail(string message, IEnumerable<string>? errors = null) => new()
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }

    /// <summary>
    /// Generic class for operation results that include data.
    /// </summary>
    /// <typeparam name="T">Type of the data returned on success.</typeparam>
    public class ResultDto<T> : ResultDto
    {
        /// <summary>
        /// Data returned when the operation is successful.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Creates a successful result including data.
        /// </summary>
        /// <param name="data">The data to return.</param>
        /// <param name="message">Optional success message.</param>
        /// <returns>A successful <see cref="ResultDto{T}"/> instance with data.</returns>
        public static ResultDto<T> Success(T data, string message = "Success") => new()
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };

        /// <summary>
        /// Creates a failure result.
        /// </summary>
        /// <param name="message">Failure message.</param>
        /// <param name="errors">Optional list of error messages.</param>
        /// <returns>A failure <see cref="ResultDto{T}"/> instance.</returns>
        public static new ResultDto<T> Fail(string message, IEnumerable<string>? errors = null) => new()
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}
