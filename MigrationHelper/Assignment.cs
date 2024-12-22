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
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Relācija: Uzdevumam ir daudz iesniegumu (1:N)
        public ICollection<Submission> Submissions { get; set; }
    }
}
