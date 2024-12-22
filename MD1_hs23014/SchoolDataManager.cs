using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace MD1_hs23014  //kā paraugs ņemts kods no lekciju repozitorija (https://github.com/ElinaKalninaLU/Lekcija3_2024/blob/master/GeometryClasses/GeometryDataManager.cs)
{
    public class SchoolDataManager : IDataManager
    {
        private DataCollection dataCollection = new DataCollection();
        public DataCollection DataCollection => dataCollection;

        private string _path = @"C:\Temp\data.xml";

        //private readonly SchoolDbContext _dbContext;

        //TESTA DATU IZVEIDE:
        public bool CreateTestData()
        {
            dataCollection = new DataCollection();

            var teachers = new List<Teacher>
            {
                new Teacher { Name = "Anna", Surname = "Apsīte", Gender = Person.GenderChoice.Woman, ContractDate = new DateTime(2022, 9, 1) },
                new Teacher { Name = "Elīza", Surname = "Egle", Gender = Person.GenderChoice.Woman, ContractDate = new DateTime(2020, 8, 24) },
                new Teacher { Name = "Gustavs", Surname = "Grauds", Gender = Person.GenderChoice.Man, ContractDate = new DateTime(2008, 7, 19) }
            };

            var students = new List<Student>()
            {
               new Student {Name="Bruno", Surname="Bērzs", Gender=Person.GenderChoice.Man, StudentIdNumber="bb1001" },
               new Student { Name = "Diāna", Surname = "Druva", Gender = Person.GenderChoice.Woman, StudentIdNumber = "dd1002" },
               new Student { Name = "Katrīna", Surname = "Kļava", Gender = Person.GenderChoice.Woman, StudentIdNumber = "kk1003" }


            };

            var courses = new List<Course>()
            {
               new Course {Name="Matemātika", Teacher = teachers[0]},
               new Course { Name = "Ģeogrāfija", Teacher = teachers[1] },
               new Course { Name = "Vēsture", Teacher = teachers[2] }

            };

            var assignments = new List<Assignment>()
            {
               new Assignment {Description="Pabeigt 3. darba lapu", Deadline=new DateTime(2024, 10, 20), Course = courses[0] },
               new Assignment { Description = "Izveidot aprakstu par vienu no pasaules okeāniem", Deadline = new DateTime(2024, 10, 16), Course = courses[1] },
               new Assignment { Description = "Uzrakstīt eseju par Ziemassvētku kaujām", Deadline = new DateTime(2024, 11, 25), Course = courses[2] }

            };

            var submissions = new List<Submission>()
            {
              new Submission {Assignment=assignments[0], Student=students[0], SubmissionTime=DateTime.Now, Score=95 },
              new Submission { Assignment = assignments[1], Student = students[1], SubmissionTime = DateTime.Now, Score = 90 },
              new Submission { Assignment = assignments[2], Student = students[2], SubmissionTime = DateTime.Now, Score = 100 }

            };

            teachers.ForEach(t => dataCollection.AddTeacher(t));
            students.ForEach(s => dataCollection.AddStudent(s));
            courses.ForEach(c => dataCollection.AddCourse(c));
            assignments.ForEach(a => dataCollection.AddAssignment(a));
            submissions.ForEach(s => dataCollection.AddSubmission(s));

            return true;
        }


        //DATU IELĀDE:
        public bool Load()
        {
            return Load(_path);
        }

        public bool Load(string path)
        {
            if (File.Exists(_path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataCollection));
                using (TextReader reader = new StreamReader(_path))
                {
                    var loadedData = (DataCollection)serializer.Deserialize(reader);
                    if (loadedData != null)
                    {
                        dataCollection = loadedData;
                        return true;
                    }
                }
            }
            return false;
        }

        //DATU SAGLABĀŠANA:
        public bool Save()
        {
            return Save(_path);
        }

        public bool Save(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataCollection));
            using (TextWriter writer = new StreamWriter(_path))
            {
                serializer.Serialize(writer, dataCollection);
            }
            return true;
        }

        //DATU IZDRUKA:
        public string Print()
        {
            return dataCollection.ToString();
        }

        //DATU ATIESTATĪŠANA:
        public bool Reset()
        {
            dataCollection = new DataCollection();
            return true;
        }
    }
}