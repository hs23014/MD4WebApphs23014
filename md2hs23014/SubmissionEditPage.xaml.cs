using MD1_hs23014;
using System.Collections.ObjectModel;
using System.Linq;

namespace md2hs23014;

public partial class SubmissionEditPage : ContentPage
{
	private Submission _submission;
	public SubmissionEditPage(Submission submission)  //Konstruktors, kas pieņem submission parametru, kas tiks rediģēts
	{
		InitializeComponent();

		_submission = submission;
        
        LoadStudents();

        BindingContext = _submission; 

        ScoreEntry.Text = _submission.Score.ToString();

        SubmissionTimePicker.Date = _submission.SubmissionTime;
    }



    private void LoadStudents() // Metode studentu ielādei
    {
        try
        {
            var students = App.DbContext.Students.ToList();

            StudentPicker.ItemsSource = students;

            StudentPicker.SelectedItem = students.FirstOrDefault(s => s.StudentId == _submission.StudentId);
        }

        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to load students: {ex.Message}", "OK");
        }
    }



    public async void OnSaveChangesClicked(object sender, EventArgs e)   //Saglabā izmaiņas
	{
        if (StudentPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select a student.", "OK");

            return;
        }

        if (!int.TryParse(ScoreEntry.Text, out int parsedScore))
        {
            await DisplayAlert("Error", "Please enter a valid score.", "OK");

            return;
        }

        try
        {
            // Atrod esošo Submission objektu datubāzē
            var existingSubmission = App.DbContext.Submissions.FirstOrDefault(s => s.SubmissionId == _submission.SubmissionId);

            if (existingSubmission != null)
            {
                //Atjaunina esošo Submission ar jaunajām vērtībām
                existingSubmission.StudentId = ((Student)StudentPicker.SelectedItem).StudentId;

                existingSubmission.SubmissionTime = SubmissionTimePicker.Date;

                existingSubmission.Score = parsedScore;

                App.DbContext.SaveChanges(); 
            }

            await DisplayAlert("Success", "Changes saved successfully.", "OK");

            await Shell.Current.GoToAsync("//MainPage");
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save changes: {ex.Message}", "OK");
        }
    }
}