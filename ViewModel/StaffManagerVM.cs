//using StaffManager.Model;
using StaffManager.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StaffManager.ViewModel
{
    public class StaffManagerVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


// все отделы
        private List<Department> allDepartments = StaffUnits.GetAllDepartment();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }

            set
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        }

// все сотрудники
        private List<Person> allPersons = StaffUnits.GetAllPerson();
        public List<Person> AllPersons
        {
            get { return allPersons; }

            set
            {
                allPersons = value;
                NotifyPropertyChanged("AllPersons");
            }
        }

        // все позиции
        private List<Position> allPositions = StaffUnits.GetAllPosition();
        public List<Position> AllPositions
        {
            get { return allPositions; }

            set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }

        // все спецработы
        private List<SpecialWork> allSpWorks = StaffUnits.GetAllSpWork();
        public List<SpecialWork> AllSpWorks
        {
            get { return allSpWorks; }

            set
            {
                allSpWorks = value;
                NotifyPropertyChanged("AllSpWorks");
            }
        }
        #region COMMANDS TO OPEN WINDOWS

        private bool CanAddCommandExecuted(object p)
        {
            if (Num != 0) return true; //команда доступна если не равно нулю, но обычно пишется return true
            else return false;
        }
        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecute, CanAddCommandExecuted); //действия которые мы определим в методах
                                                                                       //через конструктор передадутся в команду
                                                                                       //получится полноценная команда с 2-мя методами и событием

        }
    }

    private RelayCommand openAddNewDepartmentWin;
        public RelayCommand OpenAddNewDepartmentWin
        {
            get 
            { 
                return openAddNewDepartmentWin ?? new RelayCommand(obj =>
                {
                    OpenAppDepartmentWindowMethod();
                }
                );
            }
        }


        #endregion

        // Методы открытия окон
        #region OPEN WINDOW METHODS
        private void OpenAppDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SelectCenterPositionAndOpen(newDepartmentWindow);
        }

        private void OpenAppPositionWindowMethod()
        {
            AddNewPosition newPositionWindow = new AddNewPosition();
            SelectCenterPositionAndOpen(newPositionWindow);
        }

        private void OpenAppPersonWindowMethod()
        {
            AddNewPersonWindow newPersonWindow = new AddNewPersonWindow();
            SelectCenterPositionAndOpen(newPersonWindow);
        }

        private void OpenAppSpecialWorkWindowMethod()
        {
            AddNewSpecialWorkWindow newSpecialWorkWindow = new AddNewSpecialWorkWindow();
            SelectCenterPositionAndOpen(newSpecialWorkWindow);
        }



        private void OpenEditDepartmentWindowMethod()
        {
            EditDepartmentWindow newDepartmentWindow = new EditDepartmentWindow();
            SelectCenterPositionAndOpen(newDepartmentWindow);
        }

        private void OpenEditPositionWindowMethod()
        {
            EditPositionWindow newPositionWindow = new EditPositionWindow();
            SelectCenterPositionAndOpen(newPositionWindow);
        }

        private void OpenEditPersonWindowMethod()
        {
            EditPersonWindow newPersonWindow = new EditPersonWindow();
            SelectCenterPositionAndOpen(newPersonWindow);
        }

        private void OpenEditSpecialWorkWindowMethod()
        {
            EditSpecialWorkWindow newSpecialWorkWindow = new EditSpecialWorkWindow();
            SelectCenterPositionAndOpen(newSpecialWorkWindow);
        }

        private void SelectCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow; // собственик - главное окно
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner; // открытие по центру
            window.ShowDialog(); // не даст продолжить работу пока его не закроеш
        }
        #endregion

    }
}
