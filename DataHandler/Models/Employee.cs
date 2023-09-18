
using Newtonsoft.Json;

namespace Models;



public class Employee
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public int Age { get; set; }
	public string? Email { get; set; }



	public Employee(int id, string name, string surname, int age, string email)
	{
		this.Id = id;
		this.Name = name;
		this.Surname = surname;
		this.Age = age;
		this.Email = email;
	}
}

public static class Dumper
{
    public static string Dump(Employee obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}