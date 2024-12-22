using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public class Course
    {
        //ĪPAŠĪBAS:
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        //METODES:
        public override string ToString()
        {
            return $"Course Name: {Name}\nTeacher: {Teacher.ToString()}\n";
        }
    }
}
