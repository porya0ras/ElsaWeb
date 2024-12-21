using ElsaWeb.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsaWeb.Models;

public class Request: ITenant
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RequestType Type { get; set; }
    public int Owner { get; set; }
    public int RefObjectId { get; set; }
    public int TenantId { get; set; }
}
