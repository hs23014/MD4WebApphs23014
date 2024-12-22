using MD1_hs23014;
using System.Collections.ObjectModel;
using System.Linq;
namespace md2hs23014;

public partial class NewSubmissionPage : ContentPage
{
    public NewSubmissionPage()
    {
        InitializeComponent();
        
        try
        {
            var db = App.DbContext;

            StudentPicker.ItemsSource = db.Students.ToList();

            AssignmentPicker.ItemsSource = db.Assignments.ToList();
        }

        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to load data for pickers: {ex.Message}", "OK");
        }
    }



    private async void OnAddSubmissionClicked(object sender, EventArgs e)  //Poga, kas pievieno jauno submission
	{
        if (StudentPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select a student.", "OK");

            return;
        }

        if (AssignmentPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select an assignment.", "OK");

            return;
        }

        if (!int.TryParse(ScoreEntry.Text, out int parsedScore))
        {
            await DisplayAlert("Error", "Please enter a valid score.", "OK");

            return;
        }



        var newSubmission = new Submission
        {
            StudentId = ((Student)StudentPicker.SelectedItem).StudentId,

            AssignmentId = ((Assignment)AssignmentPicker.SelectedItem).AssignmentId,

            SubmissionTime = SubmissionTimePicker.Date,

            Score = parsedScore
        };



        try
        {
            var db = App.DbContext;

            db.Submissions.Add(newSubmission);

            db.SaveChanges();

            await DisplayAlert("Success", "Submission added successfully.", "OK");

            await Shell.Current.GoToAsync("//MainPage"); 
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add submission: {ex.Message}", "OK");
        }
    }
}