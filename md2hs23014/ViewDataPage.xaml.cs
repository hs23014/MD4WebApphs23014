using System.Collections.ObjectModel;
using MD1_hs23014;


namespace md2hs23014
{

	public partial class ViewDataPage : ContentPage
	{
		//Publiskās kolekcijas
        public ObservableCollection<Teacher> Teachers { get; set; }
		public ObservableCollection<Student> Students { get; set; }
		public ObservableCollection<Course> Courses { get; set; }
		public ObservableCollection<Assignment> Assignments { get; set; }
		public ObservableCollection<Submission> Submissions { get; set; }



        public ViewDataPage()
        {
            InitializeComponent();

            UpdateDataCollections();
        }



        private async void OnLoadDataClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Info", "Data reload is not necessary with a database-backed application.", "OK");
        }



        private async void OnSaveDataClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Info", "Data save is automatically managed by the database.", "OK");
        }

      

        private void OnResetDataClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "Reset functionality is not implemented.", "OK");
        }



        private void UpdateDataCollections()
        {
            try
            {
                //Inicializē kolekcijas
                Teachers ??= new ObservableCollection<Teacher>();

                Students ??= new ObservableCollection<Student>();
                
                Courses ??= new ObservableCollection<Course>();
                
                Assignments ??= new ObservableCollection<Assignment>();
                
                Submissions ??= new ObservableCollection<Submission>();

                //Notīra esošās datu kolekcijas
                Teachers.Clear();
                
                Students.Clear();
                
                Courses.Clear();
                
                Assignments.Clear();
                
                Submissions.Clear();



                var db = App.DbContext;

                foreach (var teacher in db.Teachers.ToList())
                    Teachers.Add(teacher);

                foreach (var student in db.Students.ToList())
                    Students.Add(student);

                foreach (var course in db.Courses.ToList())
                    Courses.Add(course);

                foreach (var assignment in db.Assignments.ToList())
                    Assignments.Add(assignment);

                foreach (var submission in db.Submissions.ToList())
                    Submissions.Add(submission);


                BindingContext = this;
            }

            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
            }
        }



        protected override void OnAppearing() //Tiek izsaukta, kad lapa tiek parādīta
        {
            base.OnAppearing();
            
            UpdateDataCollections();
        }
    
    }
}