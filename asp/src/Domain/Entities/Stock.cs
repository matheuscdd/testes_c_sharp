using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Entities;

[Table("Stocks")]
public class Stock: Entity
{
    public string Symbol { get; private set; }
    public string CompanyName { get; private set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; private set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LastDiv { get; private set; }

    public string Industry { get; private set; }
    public long MarketCap { get; private set; }
    public List<Comment> Comments { get; set; } = [];
    public List<Portfolio> Portfolios { get; set; } = [];

    // TODO - inserir validações no construtor
    protected Stock(
        string symbol, 
        string companyName, 
        decimal purchase, 
        decimal lastDiv, 
        string industry, 
        long marketCap
    )
    {
        validateSymbol(symbol);
        validateCompanyName(companyName);
        validatePurchase(purchase);
        validateLastDiv(lastDiv);
        validateIndustry(industry);
        validateMarketCap(marketCap);
        SetSymbol(symbol);
        SetCompanyName(companyName);
        SetPurchase(purchase);
        SetLastDiv(lastDiv);
        SetIndustry(industry);
        SetMarketCap(marketCap);
    }

    public void SetSymbol(string? symbol)
    {
        validateSymbol(symbol);
        Symbol = symbol!.ToUpper();
    }

    public void SetCompanyName(string? companyName)
    {
        validateCompanyName(companyName);
        CompanyName = companyName!;
    }

    public void SetIndustry(string? industry)
    {
        validateIndustry(industry);
        Industry = industry!;
    }

    public void SetPurchase(decimal? purchase)
    {
        validatePurchase(purchase);
        Purchase = (decimal) purchase!;
    }

    public void SetLastDiv(decimal? lastDiv)
    {
        validateLastDiv(lastDiv);
        LastDiv = (decimal) lastDiv!;
    }

    public void SetMarketCap(long? marketCap)
    {
        validateMarketCap(marketCap);
        MarketCap = (long) marketCap!;
    }

    private void validateEmpty(decimal? value, string name)
    {
        if (value == null) 
        {
            throw new ValidationCustomException($"{name} cannot be empty");
        }
    }

    private void validateEmpty(long? value, string name)
    {
        if (value == null) 
        {
            throw new ValidationCustomException($"{name} cannot be empty");
        }
    }

    private void validateDecimal(decimal value, string name, int maxPlacesInt, int maxPlacesDecimal)
    {
        var pattern = @"^\d{1," + maxPlacesInt + @"}(\.\d{1," + maxPlacesDecimal + @"})?$";
        var isValid = new Regex(pattern).IsMatch(value.ToString());
        if (!isValid)
        {
            throw new ValidationCustomException($"${name} is invalid decimal plates");
        }
    }

    private void validateLastDiv(decimal? lastDiv)
    {
        const string name = nameof(LastDiv);
        validateEmpty(lastDiv, name);
        validateDecimal((decimal) lastDiv!, name, 18, 2);
    }

    private void validateMarketCap(long? marketCap)
    {
        const string name = nameof(marketCap);
        validateEmpty(marketCap, name);
    }

    private void validatePurchase(decimal? purchase)
    {
        const string name = nameof(Purchase);
        validateEmpty(purchase, name);
        validateDecimal((decimal) purchase!, name, 18, 2);
    }

    private void validateSymbol(string? symbol)
    {
        const string name = nameof(Symbol);
        validateEmpty(symbol, name);
        validateLength(symbol!, name, 5, 10);
    }

    private void validateCompanyName(string? companyName)
    {
        const string name = nameof(CompanyName);
        validateEmpty(companyName, name);
        validateLength(companyName!, name, 5, 10);
    }

    private void validateIndustry(string? industry)
    {
        const string name = nameof(Industry);
        validateEmpty(industry, name);
        validateLength(industry!, name, 5, 10);
    }
}