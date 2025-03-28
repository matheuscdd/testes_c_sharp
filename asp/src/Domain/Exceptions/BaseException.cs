using System.Diagnostics;

namespace Domain.Exceptions;
public abstract class BaseException: Exception
{
    public int StatusCode { get; }
    public string TraceId { get; }
    public string Type { get; }
    
    protected BaseException(string message, int statusCode, string type) : base(message)
    {
        Type = type;
        StatusCode = statusCode;
        TraceId = Activity.Current?.Id ?? "N/A";
    }

    // public override string ToString()
    // {
    //     return $"TraceId: {TraceId} - {base.ToString()}";
    // }
}