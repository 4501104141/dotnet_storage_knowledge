using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_config_lang")]
public class SqlConfigLang
{
    [Key]
    public long ID { get; set; }
    public string data { get; set; } = "";
}
