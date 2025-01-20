using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("tb_class")]
public class SqlClass
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public bool isdeleted { get; set; } = false;
    public SqlStateClass? state { get; set; }
    public List<SqlStudent> students { get; set; } = new List<SqlStudent>();
}
