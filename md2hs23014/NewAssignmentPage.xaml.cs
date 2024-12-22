using MD1_hs23014;
using System.Collections.ObjectModel;

namespace md2hs23014;

public partial class NewAssignmentPage : ContentPage
{
    public NewAssignmentPage() // Konstruktors bez parametriem
    {
        InitializeComponent();

        try
        {
            var db = App.DbContext;

            CoursePicker.ItemsSource = db.Courses.ToList();
        }

        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to load courses: {ex.Message}", "OK");
        }
    }



    private async void OnAddAssignmentClicked(object sender, EventArgs e)  //Poga, kas pievieno jauno uzdevumu
	{
        var selectedCourse = (Course)CoursePicker.SelectedItem;

        if (selectedCourse == null)
        {
            await DisplayAlert("Error", "Please select a course for the assignment.", "OK");

            return;
        }

        var newAssignment = new Assignment
		{
			Description = DescriptionEntry.Text,

			Deadline = DeadlinePicker.Date,

            CourseId = selectedCourse.CourseId
		};

        try
        {
            var db = App.DbContext;

            db.Assignments.Add(newAssignment);

            db.SaveChanges(); // Saglabā uzdevumu datubāzē

            await DisplayAlert("Success", "Assignment added successfully.", "OK");

            await Shell.Current.GoToAsync("//MainPage"); // Pārved uz galveno lapu
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add assignment: {ex.Message}", "OK");
        }

    }
}