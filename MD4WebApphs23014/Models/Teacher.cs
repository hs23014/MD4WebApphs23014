
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD4WebApphs23014.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime ContractDate { get; set; }

        // Relācija: Skolotājam ir daudz kursu (1:N)
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
