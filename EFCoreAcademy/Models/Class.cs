namespace EFCoreAcademy;

public class Class : BaseEntity
{
    public string Title { get; set; }
    public Professor Professor { get; set; }
    public List<Student> Students { get; set; }
    
}