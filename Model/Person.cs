using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; } // поле Int не допускает Null поэтому и в БД NOT NULL
        public string SurName { get; set; }
        public string Name { get; set; } // поле String допускает Null поэтому и в БД NULL
        public string? FirdName { get; set; }
        public string? Phone { get; set; }

        public int PositionId { get; set; }
        public virtual Position Position { get; set; } // virtual можно не указывать

        [Column(TypeName = "date")] // Чтобы в базе данных был тип только date без time
        public DateTime? Birthday { get; set; }

        public ICollection<SpecialWork> SpecialWorks { get; set; }

        [NotMapped]
        public Position PersonPosition 
        {
            get 
            {
                return StaffUnits.GetPositionById(PositionId); // что бы получить позицию по ID, создадим метод который получает позицию по Id
            }

        }

        // расчет возраста
        public static int Age(DateTime Birthday)
        {
            int age = DateTime.Now.Year - Birthday.Year;


            return age;
        }

    }
}
