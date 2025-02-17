using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_camera")]
public class SqlCamera
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string link { get; set; } = "";
    public string des { get; set; } = "";
    public bool isDeleted { get; set; } = false;
    public long viewers { get; set; } = 0;
    public SqlCustomer? customer { get; set; }
    public List<SqlGroupCamera> groupCameras { get; set; } = new List<SqlGroupCamera>();
    public List<SqlCameraUser> cameraUsers { get; set; } = new List<SqlCameraUser>();
}
