using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public abstract class Person
    {
        private string name;
        private string surname;

        public enum GenderChoice
        {
            Man,
            Woman
        }

        //ĪPAŠĪBAS:
        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) //pārbauda, vai vārds nav ierakstīts kā tukšums vai atstarpe
                {
                    name = value;
                }
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) //pārbauda, vai uzvārds nav ierakstīts kā tukšums vai atstarpe
                {
                    surname = value;
                }
            }
        }

        public string FullName
        {
            get { return $"{Name} {Surname}"; }
        }

        public GenderChoice Gender
        {get; set;}

        //METODES:
        public override string ToString()
        {
            return $"Name: {Name}\nSurname: {Surname}\nFull Name: {FullName}\nGender: {Gender}";
        }
    }
}
