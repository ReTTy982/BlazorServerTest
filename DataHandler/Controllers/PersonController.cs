using DataHandler;
using Models;
using System.Reflection.Emit;

namespace Controller;


public class PersonController
{
    public readonly DataGenerator _generator = new();
    private string? _selectedRole = "Student";

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }

    public List<Student>? students;
    public List<Teacher>? teachers;

    public string? JsonData { get; set; }
    public string? SelectedRole
    { 
        get { return _selectedRole; }
        set { _selectedRole = value; }
    }
    public string? Subject { get; set; }
    public int Semester { get; set; }
    public Subjects SelectedClass { get; set; }
    public List<Subjects> ClassesList { get; set; } = new List<Subjects>();

    public Subjects SelectedSubject {  get; set; }


    public async Task Init()
    {
        if (students == null)
        {
            var array = await _generator.AssignStudentsRoles(2);
            students = array.ToList();

        }
        if (teachers == null)
        {
            var array = await _generator.AssignTeachersRoles(2);
            teachers = array.ToList();

        }

    }


    public void JsonDataStudents(int targetId)
    {
        var person = students.First(student => student.Id == targetId);
        JsonData = JsonHandler.ClassToJson<Student>(person);
    }

    public void JsonDataTeachers(int targetId)
    {
        var person = teachers.First(teacher => teacher.Id == targetId);
        JsonData = JsonHandler.ClassToJson<Teacher>(person);
    }

    public void CreatePerson()
    {
        if (SelectedRole == "Student")
        {
            string[] array = ClassesList.ConvertAll(subject => subject.ToString()).ToArray();
            Student student = new(_generator.idRange, Name, Surname, Age, Semester, array);
            students.Add(student);
            _generator.idRange++;
        }
        else if (SelectedRole == "Teacher")
        {
            string subject = SelectedSubject.ToString();
            Teacher teacher = new(_generator.idRange, Name, Surname, Age, subject);
            teachers.Add(teacher);
            _generator.idRange++;
        }

    }

    public void AddSelectedClass()
    {
        if (!ClassesList.Contains(SelectedClass))
        {
            ClassesList.Add(SelectedClass);
        }
    }

    public void RemoveSelectedClass(Subjects subject)
    {
        ClassesList.Remove(subject);
    }

}