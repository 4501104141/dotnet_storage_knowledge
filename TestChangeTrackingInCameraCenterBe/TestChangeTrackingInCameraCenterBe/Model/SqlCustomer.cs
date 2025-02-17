using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_customer")]
public class SqlCustomer
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public string des { get; set; } = "";
    public bool isDeleted { get; set; } = false;
    public List<SqlGroup> groups { get; set; } = new List<SqlGroup>();
    public List<SqlUser> users { get; set; } = new List<SqlUser>();
    public List<SqlCamera> cameras { get; set; } = new List<SqlCamera>();
}
