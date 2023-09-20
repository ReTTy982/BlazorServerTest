namespace Models;


public class Student : Person
{
	public int Semester { get; private set; }
	public string[]? Classes { get; private set; }
	public string role = "Student";


	public Student(string Name, string Surname) : base(Name, Surname)
	{

	}

	public Student(int id,string name, string surname, int age,int semester, string[] classes) : base(name,surname,age)
	{
		this.Id = id;
		this.Semester = semester;
		this.Classes = classes;

	}
	// Constructor for database read
    public Student(int id, string name, string surname, int age, int semester, string classes) : base(name, surname, age)
    {
        this.Id = id;
        this.Semester = semester;
        this.Classes = classes.Split(",");

    }
	// Constructor for database write
    public Student(string name, string surname, int age, int semester, string[] classes) : base(name, surname, age)
    {
        this.Semester = semester;
        this.Classes = classes;

    }

    public void TakeClasses(Subjects[] classes)
	{
		string[] stringClasses = new string[classes.Length];
		for (int i = 0; i < classes.Length; i++)
		{
			string subject = classes[i].ToString();
			stringClasses[i] = subject;
		}
		if (this.Classes == null)
		{
			this.Classes = stringClasses;
		}
		else
		{
			this.Classes.Concat(stringClasses);
		}
	}

	public void setAge(int age)
	{
		this.Age = age;
	}


	public void setSemester(int semester)
	{
		this.Semester = semester;
	}


}

