using MD1_hs23014;
using System.Collections.ObjectModel;

namespace md2hs23014;

public partial class AssignmentEditPage : ContentPage
{
	private Assignment _assignment;
	public ObservableCollection<Course> Courses { get; set; }
	public AssignmentEditPage(Assignment assignment) //konstruktors, kuram tiek padots rediģējamais uzdevums
	{
		InitializeComponent();

		_assignment = assignment;
        
        Courses = new ObservableCollection<Course>();

        LoadCourses();

        BindingContext = _assignment;
        
        CoursePicker.ItemsSource = Courses;

        CoursePicker.SelectedItem = Courses.FirstOrDefault(c => c.CourseId == _assignment.CourseId);
    }



    private void LoadCourses() //metode kursu ielādei
    {
        try
        {
            foreach (var course in App.DbContext.Courses.ToList())
            {
                Courses.Add(course);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to load courses: {ex.Message}", "OK");
        }
    }



    private async void OnSaveChangesClicked(object sender, EventArgs e) //saglabā izmainītos datus, nospiežot pogu
	{
        try
        {
            var assignmentToUpdate = App.DbContext.Assignments.FirstOrDefault(a => a.AssignmentId == _assignment.AssignmentId);

            if (assignmentToUpdate != null)
            {
                assignmentToUpdate.Description = DescriptionEntry.Text;

                assignmentToUpdate.Deadline = DeadlinePicker.Date;

                assignmentToUpdate.CourseId = ((Course)CoursePicker.SelectedItem)?.CourseId ?? 0;

                App.DbContext.SaveChanges(); //saglabā izmaiņas datubāzē
            }

            await Shell.Current.GoToAsync("//MainPage"); //atgriežas uz galveno lapu
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save changes: {ex.Message}", "OK");
        }
    }
}