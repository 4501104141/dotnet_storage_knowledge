using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_user")]
public class SqlUser
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public string token { get; set; } = "";
    public string username { get; set; } = "";
    public string password { get; set; } = "";
    public string des { get; set; } = "";
    public string phone { get; set; } = "";
    public string avatar { get; set; } = "";
    public bool isDeleted { get; set; } = false;
    public SqlRole? role { get; set; }
    public SqlCustomer? customer { get; set; }
    public List<SqlUserGroup> userGroups { get; set; } = new List<SqlUserGroup>();
    public List<SqlCameraUser> cameraUsers { get; set; } = new List<SqlCameraUser>();
}
