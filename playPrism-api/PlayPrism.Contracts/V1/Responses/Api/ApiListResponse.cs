using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPrism.Contracts.V1.Responses.Api
{
    /// <summary>
    ///     Represents api response with list of data.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public class ApiListResponse<T> : BaseResponse<IEnumerable<T>>
    {
        /// <summary>
        ///     Gets or sets total items count
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        ///     Creates list response.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="totalCount">Total items count</param>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        public static ApiListResponse<T> CreateListResponse(IEnumerable<T> data, int? totalCount = null,
            string errors = null)
        {
            return new ApiListResponse<T>
            { Data = data, Error = errors, TotalCount = totalCount };
        }
    }
}
