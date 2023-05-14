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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StaffManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllDepartmentsView; // поля добавятся после инициализации и свяжутся со свойствами MainWindow
                                                   // ViewAllDepartments
        public static ListView AllPositionsView;
        public static ListView AllPersonsView;
        public static ListView AllSpWorksView;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new StaffManagerVM();
            AllDepartmentsView = ViewAllDepartmens; // наполняем свойства в MainWindow.xaml.sc 
            AllPersonsView = ViewAllPersons;
            AllPositionsView = ViewAllPositions;
            AllSpWorksView = ViewAllSpWorks;
        }
    }
}
