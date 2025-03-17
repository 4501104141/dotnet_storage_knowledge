using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSchool.Model;

[Table("tb_order")]
public class SqlOrder
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public SqlOrder? order { get; set; }
    public List<SqlOrder> orders { get; set; } = new List<SqlOrder>();
}
