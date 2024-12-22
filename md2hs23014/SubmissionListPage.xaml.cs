using MD1_hs23014;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace md2hs23014;

public partial class SubmissionListPage : ContentPage
{
	public ObservableCollection<Submission> Submissions { get; set; }
	public SubmissionListPage()
	{
		InitializeComponent();
        
        Submissions = new ObservableCollection<Submission>(
            App.DbContext.Submissions
                .Include(s => s.Assignment) // Iekļauj saistīto Assignment
                .ThenInclude(a => a.Course) // Iekļauj saistīto Course
                .ThenInclude(c => c.Teacher) // Iekļauj saistīto Teacher
                .Include(s => s.Student) // Iekļauj saistīto Student
                .ToList()
        );

        BindingContext = this;
	}



	private async void OnEditSubmissionClicked(object sender, EventArgs e)  //Poga, kas pārved uz Edit Submission lapu
	{
		var button = sender as Button;

		var submission = button?.BindingContext as Submission;

		if (submission != null) {
			await Navigation.PushAsync(new SubmissionEditPage(submission));
		}
	}



    private async void OnDeleteSubmissionClicked(object sender, EventArgs e)  //Poga, kas izdzēš submission
    {
        var button = sender as Button;

        var submission = button?.BindingContext as Submission;

        if (submission != null)
        {
            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this submission?", "Yes", "No");

            if (confirm)
            {
                try
                {
                   var db = App.DbContext;

                    // Izdzēš submission no datubāzes
                    var submissionToRemove = db.Submissions.FirstOrDefault(s => s.SubmissionId == submission.SubmissionId);

                    if (submissionToRemove != null)
                    {
                        db.Submissions.Remove(submissionToRemove);

                        db.SaveChanges();
                    }

                    Submissions.Remove(submission);

                    await DisplayAlert("Success", "Submission deleted successfully.", "OK");
                }

                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to delete submission: {ex.Message}", "OK");
                }
            }
        }
    }
}