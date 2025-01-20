using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("tb_state_class")]
public class SqlStateClass
{
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public bool isdeleted { get; set; } = false;
    public List<SqlClass> classes { get; set; } = new List<SqlClass>();
}
