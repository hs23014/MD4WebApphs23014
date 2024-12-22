using MD1_hs23014;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace md2hs23014;

public partial class AssignmentListPage : ContentPage
{
	public ObservableCollection<Assignment> Assignments { get; set; }
	public AssignmentListPage()
	{
		InitializeComponent();
        
        var db = App.DbContext;

        //ielādē uzdevumus ar kursiem un skolotājiem
        Assignments = new ObservableCollection<Assignment>(
            db.Assignments
              .Include(a => a.Course) 
              .ThenInclude(c => c.Teacher) 
              .ToList()
        );

        BindingContext = this;
	}



	private async void OnEditAssignmentClicked(object sender, EventArgs e) //nospiežot pogu, tiek pārvests uz Edit Assignment lapu
	{
		var button = sender as Button;

		var assignment = button?.BindingContext as Assignment;

		if (assignment != null) {
			await Navigation.PushAsync(new AssignmentEditPage(assignment));
		}
	}



    private async void OnDeleteAssignmentClicked(object sender, EventArgs e) //nospiežot pogu, assignments tiek noņemts
    {
        var button = sender as Button;

        var assignment = button?.BindingContext as Assignment;

        if (assignment != null)
        {
            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this assignment?", "Yes", "No");

            if (confirm)
            {
                try
                {
                   var db = App.DbContext;

                    var assignmentToRemove = db.Assignments.FirstOrDefault(a => a.AssignmentId == assignment.AssignmentId);

                    if (assignmentToRemove != null)
                    {
                        db.Assignments.Remove(assignmentToRemove);
                        db.SaveChanges();
                    }

                    Assignments.Remove(assignment);
                }

                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to delete assignment: {ex.Message}", "OK");
                }
            }
        }
    }
}