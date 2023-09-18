using RandomNameGeneratorLibrary;
using Newtonsoft.Json;
using Models;
namespace DataHandler;

public class DataGenerator
{
    private readonly PersonNameGenerator NameGenerator = new();
    public int idRange = 0;
    private readonly Random random = new();
    private readonly int subjectsLength = Enum.GetValues(typeof(Subjects)).Length;
    private enum Gender
    {
        Male,
        Female
    }

    private string GetName(Gender gender)
    {
        if (gender == Gender.Male) return NameGenerator.GenerateRandomMaleFirstAndLastName();
        else return NameGenerator.GenerateRandomFemaleFirstAndLastName();

    }


    private (string, string) SplitName(string fullName)
    {
        string[] words = fullName.Split(' ');

        return (words[0], words[1]);
    }


    public T[] GenerateSet<T>(int size, Func<string, string, T> createPerson) where T : Person
    {

        T[] set = new T[size];
        Type type = typeof(Gender);
        Array values = Enum.GetValues(type);
        int index;

        for (int i = 0; i < size; i++)
        {
            index = random.Next(values.Length);
            Gender gender = (Gender)values.GetValue(index)!;

            var namesData = SplitName(GetName(gender));
            T person = createPerson(namesData.Item1, namesData.Item2);
            person.SetId(this.idRange);
            this.idRange++;
            set[i] = person;

        }

        return set;
    }

    public Task<Student[]> AssignStudentsRoles(int size)
    {
        Student[] set = GenerateSet(size, (firstName, lastName) => new Student(firstName, lastName));
        for (int i = 0; i < set.Length; i++)
        {
            Student student = set[i];
            set[i] = CreateStudent(student);
        }
        return Task.FromResult(set);
    }

    public Task<Teacher[]> AssignTeachersRoles(int size)
    {
        Teacher[] set = GenerateSet(size, (firstName, lastName) => new Teacher(firstName, lastName));
        for (int i = 0; i < set.Length; i++)
        {
            Teacher teacher = set[i];
            set[i] = CreateTeacher(teacher);
        }
        return Task.FromResult(set);
    }

    public Student CreateStudent(Student student)
    {
        int age = this.random.Next(18, 31);
        int semester = this.random.Next(1, 8);
        Subjects[] allSubjects = (Subjects[])Enum.GetValues(typeof(Subjects));
        int subjectsTotal = this.random.Next(1, this.subjectsLength + 1);

        for (int i = allSubjects.Length - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            Subjects temp = allSubjects[i];
            allSubjects[i] = allSubjects[j];
            allSubjects[j] = temp;
        }
        Subjects[] takenSubjects = allSubjects.Take(subjectsTotal).ToArray();
        student.TakeClasses(takenSubjects);
        student.setAge(age);
        student.setSemester(semester);

        return student;
    }


    public Teacher CreateTeacher(Teacher teacher)
    {
        int age = this.random.Next(30, 100);
        Subjects[] allSubjects = (Subjects[])Enum.GetValues(typeof(Subjects));
        int randomIndex = random.Next(0, allSubjects.Length);
        Subjects randomSubject = allSubjects[randomIndex];
        teacher.SetSubject(randomSubject);
        teacher.SetAge(age);
        return teacher;
    }
}


public class JsonHandler
{

    public static string ClassToJson<T>(T data)
    {
        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    public static List<string> SetToJsonList<T>(T[] data)
    {
        List<string> jsonList = new List<string>();
        foreach (var item in data)
        {
            jsonList.Add(ClassToJson(item));
        }
        return jsonList;
    }
}



