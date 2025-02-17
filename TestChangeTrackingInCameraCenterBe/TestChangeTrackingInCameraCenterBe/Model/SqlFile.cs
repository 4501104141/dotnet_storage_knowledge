using System.ComponentModel.DataAnnotations;

namespace CameraCenterBe.Model;

public class SqlFile
{
    [Key]
    public long ID { get; set; }
    public string code { get; set; } = "";
    public byte[]? avatar { get; set; } = null;
}
