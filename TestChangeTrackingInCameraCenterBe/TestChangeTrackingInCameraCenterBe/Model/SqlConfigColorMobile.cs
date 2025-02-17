using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_config_color_mobile")]
public class SqlConfigColorMobile
{
    [Key]
    public long ID { get; set; }
    public string data { get; set; } = "";
}
