using Laboratory_7.Model;
using System.IO;
using System.Xml.Serialization;

namespace Laboratory_7.Service
{
    public class DataService
    {
        //private
        public readonly string _filePath;

        public DataService()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(baseDirectory, "Students.xml");
        }

        public async Task<List<Student>> LoadStudentsAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }

            return await Task.Run(() =>
            {
                var serializer = new XmlSerializer(typeof(List<StudentDto>));
                try
                {
                    using StreamReader reader = new StreamReader(_filePath);

                    List<StudentDto>? studentDtos = serializer.Deserialize(reader) as List<StudentDto>;

                    if (studentDtos == null)
                    {
                        return new List<Student>();
                    }

                    return studentDtos.Select(dto => new Student
                    {
                        FirstName = dto.FirstName ?? string.Empty,
                        LastName = dto.LastName ?? string.Empty,
                        Age = dto.Age,
                        Gender = dto.Gender ?? string.Empty
                    }).ToList();
                }
                catch (System.InvalidOperationException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Помилка десеріалізації XML: {ex.Message}");
                    return new List<Student>();
                }
                catch (IOException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Помилка доступу до файлу: {ex.Message}");
                    return new List<Student>();
                }
            });
        }

        public async Task SaveStudentsAsync(List<Student> students)
        {
            if (students == null) return;

            await Task.Run(() =>
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<StudentDto>));
                try
                {
                    using StreamWriter writer = new StreamWriter(_filePath);

                    List<StudentDto>? studentDtos = students.Select(s => new StudentDto
                    {
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Age = s.Age,
                        Gender = s.Gender
                    }).ToList();

                    serializer.Serialize(writer, studentDtos);
                }
                catch (IOException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Помилка запису до файлу: {ex.Message}");
                }
                catch (System.InvalidOperationException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Помилка серіалізації XML: {ex.Message}");
                }
            });
        }
    }
}
