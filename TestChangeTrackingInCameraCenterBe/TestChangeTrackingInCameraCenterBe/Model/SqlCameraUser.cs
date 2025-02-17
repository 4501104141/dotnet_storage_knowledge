using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_camera_user")]
public class SqlCameraUser
{
    [Key]
    public long ID { get; set; }
    public SqlCamera? camera { get; set; }
    public SqlUser? user { get; set; }
}
