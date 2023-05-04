using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Model
{
    /// <summary>
    /// Многие ко многим много работ во многих Persons
    /// </summary>
    public class SpecialWork
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public ICollection<Person> Persons { get; set; }
    }
}
