using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("tb_school")]
public class SqlSchool
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public string des { get; set; } = "";
    public string des2 { get; set; } = "";
    public bool isdeleted { get; set; } = false;
    public List<SqlStudent> students { get; set; } = new List<SqlStudent>();
    public List<SqlTeacher> teachers { get; set; } = new List<SqlTeacher>();
}
