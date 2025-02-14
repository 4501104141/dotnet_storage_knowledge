using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSchool.Model;

[Table("tb_student")]
public class SqlStudent
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public bool isdeleted { get; set; } = false;
    public SqlSchool? school { get; set; }
    public SqlClass? classs { get; set; }
}
