using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPrism.Contracts.V1.Responses.Api
{

    /// <summary>
    ///     Represents base api response.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        ///     Gets or sets api response data.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     Gets or sets api response error.
        /// </summary>
        public string Error { get; set; }
    }
}
