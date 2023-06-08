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
    /// Логика взаимодействия для AddNewPosition.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position positionToEdit)
        {
            InitializeComponent();
            DataContext = new StaffManagerVM(); // сначала обьявили новый VM
            StaffManagerVM.SelectedPosition= positionToEdit;    // получаем инфо из VM и сохраняем обратно в StaffManagerVM
            StaffManagerVM.PositionName = positionToEdit.Name;  
            StaffManagerVM.PositionSalary = positionToEdit.Salary;  
            StaffManagerVM.PositionMaxNumber = positionToEdit.MaxNumber;  
        }
    }
}
