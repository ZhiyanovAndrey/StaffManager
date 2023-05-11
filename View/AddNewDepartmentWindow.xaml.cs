
using System.Windows;
using StaffManager.ViewModel;

namespace StaffManager.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewDepartmentWindow.xaml
    /// </summary>
    public partial class AddNewDepartmentWindow : Window
    {
        public AddNewDepartmentWindow()
        {
            InitializeComponent();
            DataContext= new StaffManagerVM();
         
        }
    }
}
