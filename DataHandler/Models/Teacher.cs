namespace Models;


public class Teacher : Person
{
    public string? Subject { get; private set; }
    public string role = "Teacher";

    public Teacher(string Name, string Surname) : base(Name, Surname)
    {

    }


    public Teacher(int id, string name, string surname, int age, string subject) :base(name,surname,age)
    {
        this.Id = id;
        this.Subject = subject;
    }

    public void SetSubject(Subjects subject)
    {

        this.Subject = subject.ToString();
    }
}