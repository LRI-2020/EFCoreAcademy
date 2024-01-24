namespace EFCoreAcademy;

public class Student:  BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Class> Classes { get; set; }
    public Address Address { get; set; }
}