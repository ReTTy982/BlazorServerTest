namespace Models;


public class Student : Person
{
	public int Semester { get; private set; }
	public string[]? Classes { get; private set; }
	public string role = "Student";


	public Student(string Name, string Surname) : base(Name, Surname)
	{

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

