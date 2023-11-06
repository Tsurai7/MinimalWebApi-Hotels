public class XmlResult<T> : IResult
{
    private static readonly XmlSerializer _xmSerializer = new(typeof(T));
    private readonly T _result;
    public XmlResult(T result) => _result = result;

    public Task ExecuteAsync(HttpContext context)
    {
        using var ms = new MemoryStream();
        _xmSerializer.Serialize(ms, _result);
        context.Response.ContentType = "application/xml";
        ms.Position = 0;
        return ms.CopyToAsync(context.Response.Body);       
    }
}

static class XmlResultExtensions
{
    public static IResult Xml<T>(this IResultExtensions _, T result) =>
        new XmlResult<T>(result);
}