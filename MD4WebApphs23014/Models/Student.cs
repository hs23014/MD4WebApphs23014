using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD4WebApphs23014.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string StudentIdNumber { get; set; }


        // Relācija: Studentam ir daudz iesniegumu (1:N)
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
