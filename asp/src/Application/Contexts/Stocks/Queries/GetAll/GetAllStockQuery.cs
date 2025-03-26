using Application.Contexts.Stocks.Dtos;
using Domain.Enums.Stocks;
using MediatR;

namespace Application.Contexts.Stocks.Queries.GetAll;

public class GetAllStockQuery: IRequest<IReadOnlyCollection<StockDtoWithComments>>
{
    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    public ESortBy SortBy { get; set; } = ESortBy.Id;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    // public GetAllStockQuery() {}

    // public GetAllStockQuery(
    //     string? symbol,
    //     string? companyName,
    //     ESortBy sortBy = ESortBy.Id,
    //     bool isDescending = false,
    //     int pageNumber = 1,
    //     int pageSize = 20
    // ) 
    // {
    //     Symbol = symbol;
    //     CompanyName = companyName;
    //     SortBy = sortBy;
    //     IsDescending = isDescending;
    //     PageNumber = pageNumber;
    //     PageSize = pageSize;
    // }
}