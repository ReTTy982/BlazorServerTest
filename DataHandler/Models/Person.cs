using Newtonsoft.Json;

namespace Models;

public class Person
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public string Surname { get; protected set; }
    public int? Age { get; protected set; }


    public Person(string Name, string Surname, int Age)
    {
        this.Name = Name;
        this.Surname = Surname;
        this.Age = Age;
    }

    public Person(string Name, string Surname)
    {
        this.Name = Name;
        this.Surname = Surname;
    }

    public string GetInfo()
    {
        string output = JsonConvert.SerializeObject(this);
        return output;
    }

    public void SetId(int id)
    {
        this.Id = id;
    }

    public void SetAge(int age)
    {
        this.Age = age;
    }



}