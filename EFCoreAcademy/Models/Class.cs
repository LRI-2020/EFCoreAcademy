namespace EFCoreAcademy;

public class Class : BaseEntity
{
    public string Title { get; set; } = default!;
    public Professor Professor { get; set; } = default!;
    public List<Student> Students { get; set; } = default!;

}

// one to many //class >-----| professor  // - mandatory professor
// many to many // class >------< students // - mandatory student
// one to one //// professor |-----------| address (address is opt - professor mandatory)
// one to one //// student |-----------| address (address is opt - student mandatory)
// 