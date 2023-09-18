namespace Models;


public class Teacher : Person
{
    public string? Subject { get; private set; }
    public string role = "Teacher";

    public Teacher(string Name, string Surname) : base(Name, Surname)
    {

    }

    public void SetSubject(Subjects subject)
    {

        this.Subject = subject.ToString();
    }
}