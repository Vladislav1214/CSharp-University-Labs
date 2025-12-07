using Laboratory_7.Model;
using System.IO;
using System.Xml.Serialization;

namespace Laboratory_7.Service
{
    public class DataService
    {
        public readonly string _filePath;

        public DataService()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(baseDirectory, "Students.xml");
        }

        public async Task<List<Student>> LoadStudentsAsync()
        {
            if (!File.Exists(_filePath)) return new List<Student>();

            return await Task.Run(() =>
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                try
                {
                    using StreamReader reader = new StreamReader(_filePath);

                    List<Student>? student = serializer.Deserialize(reader) as List<Student>;

                    if (student == null) return new List<Student>();

                    return student;
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                try
                {
                    using StreamWriter writer = new StreamWriter(_filePath);
                    serializer.Serialize(writer, students);
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
