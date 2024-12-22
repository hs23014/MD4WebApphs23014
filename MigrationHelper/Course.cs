using MD1_hs23014;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationHelper
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        // Relācija: Kursam ir daudz uzdevumu (1:N)
        public ICollection<Assignment> Assignments { get; set; }
    }
}
