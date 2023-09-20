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
            //var array = await _generator.AssignStudentsRoles(2);
            //students = array.ToList();

            await ReadStudentAsync(_data, connectionString);

            if (!students.Any())
            {
                SelectedRole = "Student";
                var array = await _generator.AssignStudentsRoles(5);
                foreach(Student student in array)
                {
                   await GenerateStudentAsync(_data, connectionString, student);
                }
                await ReadStudentAsync(_data, connectionString);
            }

        }
        if (teachers==null)
        {

            await ReadTeacherAsync(_data, connectionString);


            if (!teachers.Any())
            {
                SelectedRole = "Teacher";
                var array = await _generator.AssignTeachersRoles(5);
                foreach (Teacher teacher in array)
                {
                    await GenerateTeacherAsync(_data, connectionString, teacher);
                }
                await ReadTeacherAsync(_data, connectionString);
            }

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

    private async Task GenerateStudentAsync(IDataAccess _data, string? connectionString,Student student)
    {
        string sql = "insert into student (Name, Surname, Age, Semester, Classes) VALUES (@Name, @Surname, @Age, @Semester,@Classes)";
        await _data.SaveData(sql,
        new
        {
            Name = student.Name,
            Surname = student.Surname,
            Age = student.Age,
            Semester = student.Semester,
            Classes = string.Join(", ", student.Classes)
        }, connectionString);
    }

    private async Task GenerateTeacherAsync(IDataAccess _data, string? connectionString, Teacher teacher)
    {
        string sql = "insert into teacher (Name, Surname, Age, Subject) VALUES (@Name, @Surname, @Age, @Subject)";
        await _data.SaveData(sql, teacher, connectionString);
    }

    public async Task CreatePersonAsync(IDataAccess _data, string? connectionString)
    {
        string sql;
        if (SelectedRole == "Student")
        {

            string[] array = ClassesList.ConvertAll(subject => subject.ToString()).ToArray();
            Student student = new(Name, Surname, Age, Semester, array);

            try
            {
                sql = "insert into student (Name, Surname, Age, Semester, Classes) VALUES (@Name, @Surname, @Age, @Semester,@Classes)";
                await _data.SaveData(sql,
                new
                {
                    Name = student.Name,
                    Surname = student.Surname,
                    Age = student.Age,
                    Semester = student.Semester,
                    Classes = string.Join(", ", student.Classes)
                }, connectionString);
                await ReadStudentAsync(_data, connectionString);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



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

    public async Task ReadStudentAsync(IDataAccess _data, string? connectionString)
    {
        string sql = "select * from student";
        students = await _data.LoadData<Student, dynamic>(sql, new { }, connectionString);
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