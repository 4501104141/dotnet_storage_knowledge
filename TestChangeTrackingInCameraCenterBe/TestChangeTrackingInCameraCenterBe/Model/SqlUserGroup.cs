using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraCenterBe.Model;

[Table("tb_user_group")]
public class SqlUserGroup
{
    [Key]
    public long ID { get; set; }

    public SqlUser? user { get; set; }
    public SqlGroup? group { get; set; }
}
