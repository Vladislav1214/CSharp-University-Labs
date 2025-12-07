namespace Laboratory_6.Model
{
    public class Employee
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EducationLevel Education { get; set; }
        public List<Language> KnowLanguage { get; set; } = new List<Language>();
        public ComputerLiteracyLevel ComputerLiteracy { get; set; }
        public int WorkExperience { get; set; }
        public bool Recommendations { get; set; }

        public Employee()
        {
            FullName = string.Empty;
        }
    }
}
