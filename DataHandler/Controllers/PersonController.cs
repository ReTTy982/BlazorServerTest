using DataHandler;
using DataHandler.Models;
using Models;
using System.Configuration.Internal;
using static Org.BouncyCastle.Math.EC.ECCurve;

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


    public async Task Init(IDataAccess _data, string? connectionString)
    {
        if (students == null)
        {
            var array = await _generator.AssignStudentsRoles(2);
            students = array.ToList();

        }
        if (teachers == null)
        {
            //var array = await _generator.AssignTeachersRoles(2);
            //teachers = array.ToList();
            await ReadTeacherAsync(_data,connectionString );

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

    public async Task CreatePersonAsync(IDataAccess _data, string? connectionString )
    {
        string sql;
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
            
            _generator.idRange++;

            try
            {
                sql = "insert into teacher (Name, Surname, Age, Subject) VALUES (@Name, @Surname, @Age, @Subject)";
                await _data.SaveData(sql, teacher, connectionString);
                await ReadTeacherAsync(_data, connectionString);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


		}

    }

    public async Task ReadTeacherAsync(IDataAccess _data, string? connectionString)
    {
        string sql = "select * from teacher";
        teachers = await _data.LoadData<Teacher, dynamic>(sql, new { }, connectionString);
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