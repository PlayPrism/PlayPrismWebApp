using PlayPrism.Core.Domain.Filters;

namespace PlayPrism.Contracts.V1.Requests.Filters;

public class PaginationFilter
{
    public PageInfo PageInfo { get; set; }
    public Filter[] Filters { get; set; }
}