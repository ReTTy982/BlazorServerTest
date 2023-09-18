using DataHandler;
using Models;
using System.Reflection.Emit;

namespace Controller;


public class PersonController
{
    public readonly DataGenerator _generator = new();
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }

    public List<Student>? students;
    public List<Teacher>? teachers;

    public string? JsonData { get; set; }
    public string? SelectedRole { get; set; }
    public string? Subject { get; set; }
    public int Semester { get; set; }
    public Subjects SelectedClass { get; set; }
    public List<Subjects> ClassesList { get; set; } = new List<Subjects>();


    public void CreatePerson()
    {

    }

    protected async Task Init()
    {
        SelectedRole = "Student";
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
}