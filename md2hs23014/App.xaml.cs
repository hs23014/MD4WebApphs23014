using MD1_hs23014;
using Microsoft.EntityFrameworkCore;

namespace md2hs23014
{
    public partial class App : Application
    {
        public static SchoolDataManager dm { get; set; }
        public static SchoolDbContext DbContext { get; private set; }  //palīdz veikt darbības ar datubāzi

        public App()    //paraugs ņemts no lekciju repozitorija (https://github.com/ElinaKalninaLU/Lekcija3_2024/blob/master/Lekcija6/App.xaml.cs)
        {
            InitializeComponent();

            MainPage = new AppShell();

            InitializeDbContext();
        }



        private void InitializeDbContext() //izveido savienojumu ar datubāzi
        {
            try
            {
                string connectionString = File.ReadAllText(@"C:\Temp\ConnS.txt");

                var options = new DbContextOptionsBuilder<SchoolDbContext>() 
                    .UseSqlite(connectionString)
                    .Options;

                DbContext = new SchoolDbContext(options);

                DbContext.Database.EnsureCreated();
            }

            catch (Exception ex)
            {
                MainPage.DisplayAlert("Error", $"Failed to initialize the database: {ex.Message}", "OK");
            }
        }



        protected override void OnSleep()   //saglabā datus, kad lietotne tiek izslēgta
        {
            App.dm.Save();
        }

        public void CreateTestData()  //izveido testa datus
        {
            try
            {
                var context = App.DbContext;

                if (!context.Teachers.Any() && !context.Students.Any() &&   //pārbauda, vai datubāze ir tukša
                    !context.Courses.Any() && !context.Assignments.Any() &&
                    !context.Submissions.Any())
                {
                    var teachers = new List<Teacher>
            {
                new Teacher { Name = "Anna", Surname = "Apsīte", Gender = "Woman", ContractDate = new DateTime(2022, 9, 1) },
                new Teacher { Name = "Elīza", Surname = "Egle", Gender = "Woman", ContractDate = new DateTime(2020, 8, 24) },
                new Teacher { Name = "Gustavs", Surname = "Grauds", Gender = "Man", ContractDate = new DateTime(2008, 7, 19) }
            };

                    var students = new List<Student>
            {
                new Student { Name = "Bruno", Surname = "Bērzs", Gender = "Man", StudentIdNumber = "bb1001" },
                new Student { Name = "Diāna", Surname = "Druva", Gender = "Woman", StudentIdNumber = "dd1002" },
                new Student { Name = "Katrīna", Surname = "Kļava", Gender = "Woman", StudentIdNumber = "kk1003" }
            };

                    var courses = new List<Course>
            {
                new Course { Name = "Matemātika", Teacher = teachers[0] },
                new Course { Name = "Ģeogrāfija", Teacher = teachers[1] },
                new Course { Name = "Vēsture", Teacher = teachers[2] }
            };

                    var assignments = new List<Assignment>
            {
                new Assignment { Description = "Pabeigt 3. darba lapu", Deadline = new DateTime(2024, 10, 20), Course = courses[0] },
                new Assignment { Description = "Izveidot aprakstu par vienu no pasaules okeāniem", Deadline = new DateTime(2024, 10, 16), Course = courses[1] },
                new Assignment { Description = "Uzrakstīt eseju par Ziemassvētku kaujām", Deadline = new DateTime(2024, 11, 25), Course = courses[2] }
            };

                    var submissions = new List<Submission>
            {
                new Submission { Assignment = assignments[0], Student = students[0], SubmissionTime = DateTime.Now, Score = 95 },
                new Submission { Assignment = assignments[1], Student = students[1], SubmissionTime = DateTime.Now, Score = 90 },
                new Submission { Assignment = assignments[2], Student = students[2], SubmissionTime = DateTime.Now, Score = 100 }
            };

                    context.Teachers.AddRange(teachers);  //pievieno datubāzei
                    context.Students.AddRange(students);
                    context.Courses.AddRange(courses);
                    context.Assignments.AddRange(assignments);
                    context.Submissions.AddRange(submissions);

                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                MainPage.DisplayAlert("Error", $"Failed to create test data: {ex.Message}", "OK");
            }
        }
    }
}
