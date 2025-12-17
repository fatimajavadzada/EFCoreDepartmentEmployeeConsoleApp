namespace EFCoreIntroConsoleApp;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }

    public List<Employee> Employees { get; set; }

    public override string ToString()
    {
        return $"{Id} | {Name} {Capacity}";
    }
}
