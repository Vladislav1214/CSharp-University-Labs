using Laboratory_6.Model;
using System.IO;
using System.Text.Json;

namespace Laboratory_6.Service
{
    public class EmployeeService
    {
        private List<Employee> _employees = new List<Employee>();

        private const string FilePath = "employees.json";

        public EmployeeService()
        {
        }

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(_employees);
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                return;
            if (_employees.Any(u => u.FullName == employee.FullName) && _employees.Any(u => u.DateOfBirth == employee.DateOfBirth))
                return;

            _employees.Add(employee);
        }

        public void UpdateEmployee(Employee originalEmployee, Employee updatedEmployee)
        {
            if (originalEmployee == null || updatedEmployee == null)
                return;

            int index = _employees.IndexOf(originalEmployee);
            if (index != -1)
            {
                _employees[index] = updatedEmployee;
            }
            else
            {
                return;
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
                return;

            _employees.Remove(employee);
        }

        public List<Employee> FilterEmployees(EducationLevel? education, int? minExperience, int? maxExperience, ComputerLiteracyLevel? computerLiteracy)
        {
            IEnumerable<Employee> filteredList = _employees;

            if (education.HasValue)
                filteredList = filteredList.Where(e => e.Education == education.Value);

            if (minExperience.HasValue)
                filteredList = filteredList.Where(e => e.WorkExperience >= minExperience.Value);

            if (maxExperience.HasValue)
                filteredList = filteredList.Where(e => e.WorkExperience <= maxExperience.Value);

            if (computerLiteracy.HasValue)
                filteredList = filteredList.Where(e => e.ComputerLiteracy == computerLiteracy.Value);

            return filteredList.ToList();
        }
        public static async Task<EmployeeService> CreateAsync()
        {
            var service = new EmployeeService();
            await service.LoadEmployeesAsync();
            return service;
        }

        private async Task LoadEmployeesAsync()
        {
            if (!File.Exists(FilePath))
                return;

            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                {
                    if (fs.Length == 0)
                        return;

                    _employees = await JsonSerializer.DeserializeAsync<List<Employee>>(fs) ?? new List<Employee>();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task SaveEmployeesAsync()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fs, _employees, options);
            }
        }
    }
}
