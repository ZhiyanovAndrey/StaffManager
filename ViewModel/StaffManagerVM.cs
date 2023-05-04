using StaffManager.Model;
using StaffManager.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Методы открытия окон

        private void OpenAppDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
        }



    }
}
