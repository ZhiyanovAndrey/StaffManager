using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Model
{
    /// <summary>
    /// один ко многим, могут находится несколько людей
    /// </summary>
    public class Department //  для этого создаем свойство ICollection от Person

    {
        public int Id { get; set; } // нужно называть DepartmentId потому что EF не поимет и оставит 
        public string Name { get; set; }

        public ICollection<Position> Positions { get; set; }

        //public ICollection<Person> Persons { get; set; } // связь с таблицей Person по одноименному столбцу, без него нет Primary Key
                                                         // в Department может быть много Person
    }
}
