using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("tb_teacher")]
public class SqlTeacher
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public string name { get; set; } = "";
    public string des { get; set; } = "";
    public string des2 { get; set; } = "";
    public string des3 { get; set; } = "";
    public string des4 { get; set; } = "";
    public string des5 { get; set; } = "";
    public string des6 { get; set; } = "";
    public string des7 { get; set; } = "";
    public string des8 { get; set; } = "";
    public string des9 { get; set; } = "";
    public string des10 { get; set; } = "";
    public bool isdeleted { get; set; } = false;
    public SqlSchool? school { get; set; }
}
