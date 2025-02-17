using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_config_color")]
public class SqlConfigColor
{
    [Key]
    public long ID { get; set; }
    public string data { get; set; } = "";
}
