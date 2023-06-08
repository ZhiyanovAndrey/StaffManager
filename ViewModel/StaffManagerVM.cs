using StaffManager.Model;
using StaffManager.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        // свойства для работы с командами
        public static string DepartmentName { get; set; }

        public static string SpWorkName { get; set; }

        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public static Department PositionDepartment { get; set; }

        public static string PersonSurName { get; set; }
        public static string PersonName { get; set; }
        public static string PersonFirdName { get; set; }
        public static string PersonPhone { get; set; }
        public static DateTime PersonBirthday { get; set; }
        public static Position PersonPosition { get; set; }
        public static SpecialWork PersonSpWork { get; set; }

        // свойства для сохранения выделенной строки
        public TabItem SelectedTabItem { get; set; }
        public static Person SelectedPerson { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }
        public static SpecialWork SelectedSpWork { get; set; }


        // метод обнуляет все свойства
        private void SetNullValuesToPropeties()
        {
            DepartmentName = null;
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;
        }


        // метод вывода сообщения и закрытия окна
        private void ShowMessage(string massage)
        {
            MessageView massageView = new MessageView(massage); // передаем текст в наше окно MessageView
                                                                // там делаем конструктор и передаем текст в TB окна
            SelectCenterPositionAndOpen(massageView);
        }


        // метод обрамляет красным при ошибках
        private void SetRedControl(Window wnd, string controlName)
        {
            Control blok = wnd.FindName(controlName) as Control;
            blok.BorderBrush = Brushes.Red;
        }


        #region COMMANDS TO ADD

        private RelayCommand addNewPerson;
        public RelayCommand AddNewPerson
        {
            get
            {
                return addNewPerson ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultstring = string.Empty;
                    /* 
                     * не забыть в AddNewPersonWindow сделать  Command="{Binding AddNewPerson}" для привязки к команде
                     * сделать Binding PersonName и наименование соответствующего ТВ x:Name="NameTB" и всех остальных полей
                     * <ComboBox ItemsSource="{Binding AllPositions}" в AddNewPersonWindow
                     * и CommandParameter="{Binding ElementName=AddNewPositionWin}" для привязки всего окна к команде дать имя окну AddNewPositionWin
                     * прописать в Combobox  ItemsSource="{Binding AllPositions}" и DisplayMemberPath="Name" для отображения всех только имени, а не класса
                     * SelectedItem="{Binding PersonPosition}" для помещения выбранной позиции в одноименное свойство.
                     * прописать в ListView.View ко всем полям соответственно DisplayMemberBinding="{Binding Path=Name}
                     */
                    if (PersonSurName == null || PersonName.Replace(" ", "").Length == 0)
                    {
                        SetRedControl(wnd, "SurNameTB");
                    }
                    if (PersonName == null || PersonName.Replace(" ", "").Length == 0)
                    {
                        SetRedControl(wnd, "NameTB");
                    }
                    if (PersonPosition == null)
                    {
                        MessageBox.Show("Укажите должность");
                    }

                    else
                    {
                        resultstring = StaffUnits.CreatePerson(PersonSurName, PersonName, PersonFirdName, PersonPhone,
                                                    PersonBirthday, PersonPosition);
                        // а св-вa в текущем классе связываем с пом. DataContext = new StaffManagerVM();
                        ShowMessage(resultstring);
                        UpdateAll();
                        SetNullValuesToPropeties(); // обнулим все поля
                        wnd.Close();
                    }
                }
                );
            }
        }



        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    /* DepartmentName свойство кнопки Binding AddNewDepartment в окне AddNewDepartmentWindow
                       делаем проверки и вызываем метод CreateDepartment(DepartmentName) передав ему Name
                       что бы передать DepartmentName создадим свойство выше и делаем Binding DepartmentName 
                       в TB окна AddNewDepartmentWindow не забыть про DataContext= new StaffManagerVM()
                    */

                    Window wnd = obj as Window; // объявляем wnd, что бы в качестве объекта в эту команду передать само окно
                                                // добавим CommandParameter к кнопке которая добавит все окно к команде
                    string resultstring = string.Empty;

                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)  // если одни пробелы или пусто то подсвечивает красным
                    {
                        SetRedControl(wnd, "NameTB");
                    }
                    else
                    {
                        resultstring = StaffUnits.CreateDepartment(DepartmentName); // взять текст из свойства DepartmentName,
                                                                                    // а св-во в текущем классе связываем с пом. DataContext = new StaffManagerVM();
                        ShowMessage(resultstring);
                        UpdateAll();
                        SetNullValuesToPropeties(); // обнулим DepartmentName
                        wnd.Close();
                    }
                }
                );
            }
        }


        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultstring = string.Empty;
                    /* 
                     * не забыть в AddNewPositionWindow (у меня AddNewPosition)
                     * сделать Binding PositionName и наименование соответствующего ТВ x:Name="NameTB"
                     * сделать  Command="{Binding AddNewPosition}" для привязки к команде
                     * и CommandParameter="{Binding ElementName=AddNewPositionWin}" для привязки всего окна дать имя окну
                     * прописать в Combobox  DisplayMemberPath="Name" для отображений только имени, а не класса
                     * прописать в ListView.View ко всем полям соответственно DisplayMemberBinding="{Binding Path=Name}
                     */
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedControl(wnd, "NameTB");
                    }
                    if (PositionSalary == 0)
                    {
                        SetRedControl(wnd, "SalaryTB");
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedControl(wnd, "MaxNumberTB");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите отдел");
                    }
                    else
                    {
                        resultstring = StaffUnits.CreatePosition(PositionName, PositionSalary, PositionDepartment, PositionMaxNumber);
                        // а св-вa в текущем классе связываем с пом. DataContext = new StaffManagerVM();
                        ShowMessage(resultstring);
                        UpdateAll();
                        SetNullValuesToPropeties(); // обнулим все поля
                        wnd.Close();
                    }
                }
                );
            }
        }
        #endregion

        #region COMMANDS TO DELITE


        private RelayCommand delItem;
        public RelayCommand DelItem
        {
            get
            {
                return delItem ?? new RelayCommand(obj =>
                {
                    string resultstring = "Ничего не выбрано";

                    // первая часть if отделяет определенный Tab что бы выполнить определенное действие
                    // вторая часть если ПКМ в пустом месте выведет исключение т.к. переменная метода Remove(person) не может быть null
                    if (SelectedTabItem.Name == "PersonTab" && SelectedPerson != null)
                    {
                        resultstring = StaffUnits.DeletePerson(SelectedPerson); // принимает Person из свойства, св-во связано с SelectedItem
                        UpdateAll();
                    }
                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        resultstring = StaffUnits.DeletePosition(SelectedPosition);
                        UpdateAll();
                    }
                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        resultstring = StaffUnits.DeleteDepartment(SelectedDepartment);
                        UpdateAll();
                    }
                    if (SelectedTabItem.Name == "SpWorkTab" && SelectedSpWork != null)
                    {
                        resultstring = StaffUnits.DeleteSpWork(SelectedSpWork);
                        UpdateAll();
                    }


                    // если не одно из условий не попадает то сообщение
                    ShowMessage(resultstring);
                    SetNullValuesToPropeties();
                });
            }
        }

        #endregion

        #region COMMANDS TO EDIT
        private RelayCommand editPerson;
        public RelayCommand EditPerson 
        {
            get
            {
                return editPerson ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultstr = "Не выбран сотрудник";
                    string noPosition = "Не выбрана новая должность";

                    if (SelectedPerson != null)
                    {
                        if (PersonPosition != null)
                        {
                            resultstr = StaffUnits.EditPerson(SelectedPerson, PersonSurName, PersonName, PersonFirdName, PersonPhone, PersonBirthday, PersonPosition);
                            UpdateAll();
                            SetNullValuesToPropeties();
                            ShowMessage(resultstr);
                            window.Close();
                        }
                        else ShowMessage(noPosition);
                    }
                    else ShowMessage(resultstr);
                });
            }
        }


        private RelayCommand editPosition;
        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultstr = "Не выбрана должность";
                    string noDepartment = "Не выбрана новый отдел";

                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            resultstr = StaffUnits.EditPosition(SelectedPosition, PositionName, PositionMaxNumber, PositionSalary,  PositionDepartment);
                            UpdateAll();
                            SetNullValuesToPropeties();
                            ShowMessage(resultstr);
                            window.Close();
                        }
                        else ShowMessage(noDepartment);
                    }
                    else ShowMessage(resultstr);
                });
            }
        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultstr = "Не выбрана должность";


                        if (SelectedDepartment != null)
                        {
                            resultstr = StaffUnits.EditDepartment(SelectedDepartment, DepartmentName);
                            UpdateAll();
                            SetNullValuesToPropeties();
                            ShowMessage(resultstr);
                            window.Close();

                    }
                    else ShowMessage(resultstr);
                });
            }
        }

        #endregion



        #region COMMANDS TO OPEN WINDOWS

        private RelayCommand openEditItemWin;
        public RelayCommand OpenEditItemWin
        {
            get
            {
                return openEditItemWin ?? new RelayCommand(obj =>
                {
                    string resultstring = "Ничего не выбрано";

                    if (SelectedTabItem.Name == "PersonTab" && SelectedPerson != null)
                    {
                        OpenEditPersonWindowMethod(SelectedPerson);
                    }
                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMethod(SelectedPosition);
                    }
                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                    if (SelectedTabItem.Name == "SpWorkTab" && SelectedSpWork != null)
                    {

                        OpenEditSpecialWorkWindowMethod();
                    }
                });
            }
        }



        private RelayCommand openAddNewDepartmentWin;
        public RelayCommand OpenAddNewDepartmentWin // в VM делаем Binding к публичным командам
        {
            get
            {
                return openAddNewDepartmentWin ?? new RelayCommand(obj => // проверка непустой ли он если да то RelayComand
                {
                    OpenAppDepartmentWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddNewPositionWin;
        public RelayCommand OpenAddNewPositionWin
        {
            get
            {
                return openAddNewPositionWin ?? new RelayCommand(obj =>
                {
                    OpenAppPositionWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddNewPersonWin;
        public RelayCommand OpenAddNewPersonWin
        {
            get
            {
                return openAddNewPersonWin ?? new RelayCommand(obj =>
                {
                    OpenAppPersonWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddNewSpecialWorkWin;
        public RelayCommand OpenAddNewSpecialWorkWin // в VM делаем Binding к публичным командам
        {
            get
            {
                return openAddNewSpecialWorkWin ?? new RelayCommand(obj =>
                {
                    OpenAppSpecialWorkWindowMethod();
                }
                );
            }
        }



        #endregion


        #region SHOW STAFF IN WINDOW
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
        #endregion


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



        private void OpenEditDepartmentWindowMethod(Department department)
        {
            EditDepartmentWindow newDepartmentWindow = new EditDepartmentWindow(department);
            SelectCenterPositionAndOpen(newDepartmentWindow);
        }

        private void OpenEditPositionWindowMethod(Position position)
        {
            EditPositionWindow newPositionWindow = new EditPositionWindow(position);
            SelectCenterPositionAndOpen(newPositionWindow);
        }

        private void OpenEditPersonWindowMethod(Person person) // передаем инфо из БД в окно правки
        {
            EditPersonWindow newPersonWindow = new EditPersonWindow(person);
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





        #region UPDATE WINDOW METHODS

        // наполняем статичные свойства в гл.окне
        private void UpdateAll()
        {
            UpdateAllDepartmentsWin();
            UpdateAllPositionsWin();
            UpdateAllPersonsWin();
            UpdateAllSpWorksWin();
        }

        private void UpdateAllDepartmentsWin()
        {                                                   // св-во в vm c привязкой к NotifyPropertyChanged("AllPersons")
            AllDepartments = StaffUnits.GetAllDepartment(); // обновляем AllDepartments, но еще необходимо обновить весь View
                                                            // добавим в MainWindow.xaml.cs статичные поля
                                                            // привяжем их к свойствам ListView x:Name="ViewAllPositions"
                                                            // в MainWindow.xaml.sc наполним их после инициализации

            MainWindow.AllDepartmentsView.ItemsSource = null; // очищаем список
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments; // наполнили
            MainWindow.AllDepartmentsView.Items.Refresh();
        }

        private void UpdateAllPositionsWin()
        {
            AllPositions = StaffUnits.GetAllPosition();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }

        private void UpdateAllPersonsWin()
        {
            AllPersons = StaffUnits.GetAllPerson();
            MainWindow.AllPersonsView.ItemsSource = null; // наполнили обнулили и добавили
            MainWindow.AllPersonsView.Items.Clear();
            MainWindow.AllPersonsView.ItemsSource = AllPersons;
            MainWindow.AllPersonsView.Items.Refresh();
        }

        private void UpdateAllSpWorksWin()
        {
            AllSpWorks = StaffUnits.GetAllSpWork();
            MainWindow.AllSpWorksView.ItemsSource = null; // наполнили обнулили и добавили
            MainWindow.AllSpWorksView.Items.Clear();
            MainWindow.AllSpWorksView.ItemsSource = AllSpWorks;
            MainWindow.AllSpWorksView.Items.Refresh();
        }

        #endregion
    }
}
