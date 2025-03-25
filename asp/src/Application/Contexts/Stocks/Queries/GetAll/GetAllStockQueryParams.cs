using Domain.Enums.Stocks;

namespace Application.Contexts.Stocks.GetAll;

public class GetAllStockQueryParams
{
    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    public ESortBy? SortBy { get; set; } = ESortBy.Id;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}