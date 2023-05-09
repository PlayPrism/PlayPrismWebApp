using PlayPrism.Contracts.V1.Responses.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPrism.Contracts.Extensions
{
    /// <summary>
    ///     Extension methods for converting between the response and the model
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        ///     Wraps object to response
        /// </summary>
        /// <param name="response">Object that should be wrapped</param>
        /// <typeparam name="T">Type of object that should be wrapped</typeparam>
        /// <returns>A <see cref="ApiResponse{T}" /> wrapped object to response </returns>
        public static ApiResponse<T> ToApiResponse<T>(this T response)
        {
            return new ApiResponse<T> { Data = response };
        }

        /// <summary>
        ///     Wraps object to response
        /// </summary>
        /// <param name="response">Object that should be wrapped</param>
        /// <typeparam name="T">Type of object that should be wrapped</typeparam>
        /// <returns>A <see cref="ApiResponse{T}" /> wrapped object to response </returns>
        public static ApiListResponse<T> ToApiListResponse<T>(this IEnumerable<T> response)
        {
            return new ApiListResponse<T>() { Data = response };
        }

        /// <summary>
        ///     Wraps error to response with error
        /// </summary>
        /// <param name="error">Error that should be wrapped to response</param>
        /// <returns>A <see cref="ApiResponse{String}" /> wrapped error to response </returns>
        public static ApiResponse<string> ToErrorResponse(this string error)
        {
            return new ApiResponse<string>
            {
                Error = error,
                Data = null
            };
        }
    }
}
