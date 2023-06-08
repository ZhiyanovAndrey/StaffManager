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
    /// Логика взаимодействия для AddNewPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        public EditPersonWindow(Person personToEdit) 
        {
            InitializeComponent();
            DataContext = new StaffManagerVM(); // сначала обьявили новый VM
            StaffManagerVM.SelectedPerson = personToEdit;  // получаем инфо из VM и сохраняем обратно в StaffManagerVM
            StaffManagerVM.PersonSurName=personToEdit.SurName;    
            StaffManagerVM.PersonName=personToEdit.Name;    
            StaffManagerVM.PersonFirdName=personToEdit.FirdName;    
            StaffManagerVM.PersonPhone=personToEdit.Phone;
            //StaffManagerVM.PersonBirthday = personToEdit.Birthday;
        }
    }
}
