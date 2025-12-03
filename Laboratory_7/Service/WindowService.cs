using Laboratory_7.Model;
using Laboratory_7.View;

namespace Laboratory_7.Service
{
    public class WindowService : IWindowService
    {
        public bool ShowStudentEditDialog(Student student)
        {
            var viewModel = new StudentEditViewModel(student);
            var window = new StudentEditWindow(viewModel);

            return window.ShowDialog() == true;
        }
    }
}
