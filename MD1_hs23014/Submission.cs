using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public class Submission
    {
        //ĪPAŠĪBAS:
        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int Score { get; set; }

        //METODES:
        public override string ToString()
        {
            return $"Student: {Student.ToString()}\nSubmission Time: {SubmissionTime}\nAssignment: {Assignment.ToString()}\nScore: {Score}\n";
        }
    }
}
