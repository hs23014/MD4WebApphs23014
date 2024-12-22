using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public class Student : Person
    {
        //ĪPAŠĪBAS:
        public string StudentIdNumber 
        { get; set; }

        //KONSTRUKTORI:
        public Student() { } //konstruktors bez parametriem

        public Student(string name, string surname, GenderChoice gender, string studentIdNumber)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            StudentIdNumber = studentIdNumber;
        }

        //METODES:
        public override string ToString()
        {
            return $"{base.ToString()}\nStudent ID Number: {StudentIdNumber}\n";
        }
    }
}
