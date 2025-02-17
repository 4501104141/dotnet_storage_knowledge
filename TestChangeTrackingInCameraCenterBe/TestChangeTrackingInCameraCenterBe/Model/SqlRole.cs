using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_role")]
public class SqlRole
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public string des { get; set; } = "";
    public bool isDeleted { get; set; } = false;
    public List<SqlUser> users { get; set; } = new List<SqlUser>();
}
