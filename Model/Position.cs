using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MaxNumber { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        
        ICollection<Person> Persons { get; set; }

// отображает отдел во вкладке Должность
        [NotMapped]
        public Department PositionDepartment
        {
            get
            {
                return StaffUnits.GetDepartmentById(DepartmentId); 
            }

        }

        // отображает всех сотрудников по Id позиции, метод дает возможность раскрытия списка
        [NotMapped]
        public List<Person> PositionPersons
        {
            get
            {
                return StaffUnits.GetPersonByPositionId(Id);
            }

        }
    }
}
