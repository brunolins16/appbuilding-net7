namespace DemoApp.Model;

public class Person
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string DocumentId { get; set; }
}

public class Customer : Person
{
    public string Address { get; set; }
}

public class Employee  : Person
{
    public string BadgedId { get; set;}
}
