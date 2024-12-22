using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public class Assignment
    {
        //ĪPAŠĪBAS:
        public DateTime Deadline { get; set; }
        public Course Course { get; set; }
        public string Description { get; set; }

        //METODES:
        public override string ToString()
        {
            return $"Deadline: {Deadline.ToShortDateString()}\nCourse: {Course.ToString()}Assignment Description: {Description}\n";
        }
    }
}
