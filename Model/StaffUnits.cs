using StaffManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Model
{
    public static class StaffUnits
    {
        // получить все отделы
        public static List<Department> GetAllDepartment()
        {
            using (Context context = new Context())
            {
                return context.Departments.ToList();
            }
        }

        // получить всех сотрудников
        public static List<Person> GetAllPerson()
        {
            using (Context context = new Context())
            {
                return context.Persons.ToList();
            }
        }

        // получить все позиции
        public static List<Position> GetAllPosition()
        {
            using (Context context = new Context())
            {
                return context.Positions.ToList();
            }
        }

        // получить все спецработы
        public static List<SpecialWork> GetAllSpWork()
        {
            using (Context context = new Context())
            {
                return context.SpWorks.ToList();
            }
        }

        // создать отдел
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";
            using (Context context = new Context())
            {
                bool CheckIsExist = context.Departments.Any(d => d.Name == name); // существует ли отдел
                if (!CheckIsExist)
                {
                    Department newDepartment = new Department { Name = name };
                    context.Departments.Add(newDepartment);
                    context.SaveChanges();
                    result = "Сделано";
                }
                return result;
            }
        }


        // создать позицию
        public static string CreatePosition(string name, decimal salary, Department department, int maxNumber)
        {
            string result = "Уже существует";
            using (Context context = new Context())
            {
                bool CheckIsExist = context.Positions.Any(p => p.Name == name && p.Salary == salary);
                if (!CheckIsExist)
                {
                    Position newPosition = new Position 
                    { 
                        Name = name, Salary = salary, MaxNumber = maxNumber, DepartmentId = department.Id 
                    };
                    context.Positions.Add(newPosition);
                    context.SaveChanges();
                    result = "Сделано";
                }
                return result;
            }
        }

        // создать сотрудника
        public static string CreatePerson(string surname, string name, string firdname, string phone, DateTime birthday, Position position)
        {
            string result = "Уже существует";
            using (Context context = new Context())
            {
                bool CheckIsExist = context.Persons.Any(p => p.SurName == surname && p.Name == name && p.FirdName == firdname); // существует ли сотрудник ???добавить ФИО позицию
                if (!CheckIsExist)
                {
                    Person newPerson = new Person
                    {
                        Name = name, SurName=surname, FirdName=firdname, Phone=phone,
                        Birthday=birthday, PositionId=position.Id
                    };
                    context.Persons.Add(newPerson);
                    context.SaveChanges();
                    result = "Сделано";
                }
                return result;
            }
        }

        // создать спец.работу
        public static string CreateSpWork(string name)
        {
            string result = "Уже существует";
            using (Context context = new Context())
            {
                bool CheckIsExist = context.SpWorks.Any(s => s.Name == name); // существует ли
                if (!CheckIsExist)
                {
                    SpecialWork newSpWork = new SpecialWork
                    {
                        Name = name
                    };
                    context.SpWorks.Add(newSpWork);
                    context.SaveChanges();
                    result = "Сделано";
                }
                return result;
            }
        }


        // удалить отдел
        public static string DeleteDepartment(Department department)
        {
            string result = "Такого отдела не существует";
            using (Context context = new Context())
            {
                context.Departments.Remove(department);
                context.SaveChanges();
                result = $"Отдел {department.Name} удален";
            }
            return result;

        }

        // удалить позицию
        public static string DeletePosition(Position position)
        {
            string result = "Такой позиции не существует";
            using (Context context = new Context())
            {
                context.Positions.Remove(position);
                context.SaveChanges();
                result = $"Позиция {position.Name} удалена";
            }
            return result;

        }

        // удалить сотрудника
        public static string DeletePerson(Person person)
        {
            string result = "Такого сотрудника не существует";
            using (Context context = new Context())
            {
                context.Persons.Remove(person);
                context.SaveChanges();
                result = $"Сотрудник {person.Name} удален";
            }
            return result;

        }



        // удалить спец работу
        public static string DeleteSpWork(SpecialWork spWork)
        {
            string result = "Такой работы не существует";
            using (Context context = new Context())
            {
                context.SpWorks.Remove(spWork);
                context.SaveChanges();
                result = $"Работа {spWork.Name} удалена";
            }
            return result;

        }



        // редактировать отдел
        public static string EditDepartment(Department oldDepartment, string newName) // на вход подаем новое свойство
        {
            string result = "Такого отдела не существует";
            using (Context context = new Context())
            {
                Department department = context.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;

                context.SaveChanges();
                result = $"Отдел {department.Name} изменен";
            }
            return result;

        }


        // редактировать позицию
        public static string EditPosition(Department oldPosition, string newName, int newMaxNum, decimal newSalary, Department newDepartment)
        {
            string result = "Такой позиции не существует";
            using (Context context = new Context())
            {
                Position position = context.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                // FirstOrDefault допускает нулевое значение, поэтому делаем проверку на Null

                if (position != null)
                {
                    position.Name = newName;
                    position.Salary = newSalary;
                    position.MaxNumber = newMaxNum;
                    position.DepartmentId = newDepartment.Id;

                    context.SaveChanges();
                    result = $"Позиция {position.Name} изменена";
                }

            }
            return result;

        }

        // редактировать сотрудника
        public static string EditPerson(Person oldPerson, string newSerName, string newName, string newFirdName, 
            string newPhone, DateTime newBirthday, Position newPosition)
        {
            string result = "Такой позиции не существует";
            using (Context context = new Context())
            {
                Person person = context.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                // FirstOrDefault допускает нулевое значение, поэтому делаем проверку на Null

                if (person != null)
                {
                    person.SurName= newSerName;
                    person.Name = newName;
                    person.FirdName = newFirdName;
                    person.Phone = newPhone;
                    person.Birthday = newBirthday; 
                    person.PositionId = newPosition.Id;

                    context.SaveChanges();
                    result = $"Информация о сотруднике {person.Name} изменена";
                }

            }
            return result;

        }

        // редактировать работу
        public static string EditSpWork(SpecialWork oldSpWork, string newName) // на вход подаем новое свойство
        {
            string result = "Такого отдела не существует";
            using (Context context = new Context())
            {
                SpecialWork spWork = context.SpWorks.FirstOrDefault(d => d.Id == oldSpWork.Id);
                
                spWork.Name = newName;

                context.SaveChanges();
                result = $"Название работы {spWork.Name} изменено";
            }
            return result;

        }

        // расчет возраста
        public static int AgeCount(DateTime Birthday)
        {
            int age = DateTime.Now.Year - Birthday.Year;
            if (Birthday < DateTime.Now.AddYears(-age)) age--;
            //age = (Birthday < DateTime.Now.AddYears(-age)) ? age : age--;



            return age;
        }

        // получить позицию по id 
        public static Position GetPositionById(int id) 
        {
            using (Context context = new Context()) 
            {
               return context.Positions.FirstOrDefault(p => p.Id == id);
            }
        }

        // получить отдел по id 
        public static Department GetDepartmentById(int id)
        {
            using (Context context = new Context())
            {
                return context.Departments.FirstOrDefault(p => p.Id == id);
            }
        }

        // получение всех сотрудников по Id позиции
        public static List<Person> GetPersonByPositionId(int id)
        {
            using (Context context = new Context())
            {
                List<Person> query= (from person in GetAllPerson()
                                    where person.PositionId == id
                                    select person).ToList();

                //List<Person> query = GetAllPosition().Select(p => p.Id == id);


                return query;
            }
        }

        // получить все позиции по id отдела
        public static List<Position> GetPositionByDepartmentId(int id)
        {
            using (Context context = new Context())
            {
                List<Position> query = (from p in GetAllPosition()
                                        where p.DepartmentId == id
                                        select p).ToList();

                //List<Person> query = GetAllPosition().Select(p => p.Id == id);


                return query;
            }
        }

    }
}
