using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public class Teacher : Person
    {
        //ĪPAŠĪBAS:
        public DateTime ContractDate 
        {get; set;}

        //METODES:
        public override string ToString()
        {
            return $"{base.ToString()}\nContract Date: {ContractDate.ToShortDateString()}\n"; //atgriež datumu kā string
        }
    }
}
