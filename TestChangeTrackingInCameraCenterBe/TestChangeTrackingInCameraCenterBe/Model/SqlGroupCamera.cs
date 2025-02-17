using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_group_camera")]
public class SqlGroupCamera
{
    [Key]
    public long ID { get; set; }
    public SqlGroup? group { get; set; }
    public SqlCamera? camera { get; set; }
}
