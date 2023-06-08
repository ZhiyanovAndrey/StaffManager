using StaffManager.Model;
using StaffManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StaffManager.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow(Department departmentToEdit)
        {
            InitializeComponent();
            DataContext = new StaffManagerVM(); // сначала обьявили новый VM
            StaffManagerVM.SelectedDepartment = departmentToEdit;  // далее получили инфо из VM и сохраняем обратно в VM
            StaffManagerVM.DepartmentName= departmentToEdit.Name;
        }
    }
}
