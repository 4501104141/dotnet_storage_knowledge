using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_group")]
public class SqlGroup
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public string des { get; set; } = "";
    public bool isDeleted { get; set; } = false;
    public List<SqlUserGroup> userGroups { get; set; } = new List<SqlUserGroup>();
    public List<SqlGroupCamera> groupCameras { get; set; } = new List<SqlGroupCamera>();
    public SqlCustomer? customer { get; set; }
}
