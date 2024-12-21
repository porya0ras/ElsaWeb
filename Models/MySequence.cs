using ElsaWeb.Models.Base;

namespace ElsaWeb.Models;

public class MySequence : ITenant
{
    public int TenantId { get; set; }
    public string JsonBody { get; set; }
    public RequestType Type { get; set; }
}
