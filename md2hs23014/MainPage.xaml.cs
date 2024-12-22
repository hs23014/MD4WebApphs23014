using MD1_hs23014;
using System.Collections.ObjectModel;
namespace md2hs23014
{
    public partial class MainPage : ContentPage   //atver lapas, kad nospiež uz pogas Main Page
    {
        int count = 0;

        private SchoolDataManager dataManager;



        public MainPage()
        {
            InitializeComponent();

            dataManager = new SchoolDataManager();

            dataManager.CreateTestData(); //ģenerē testa datus
        }

        

        private async void OnAddNewStudentClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewStudentPage));
        }



        private async void OnViewDataClicked(object sender, EventArgs e)
        {
            try
            {
                bool isDatabaseEmpty = !App.DbContext.Teachers.Any() &&
                                       !App.DbContext.Students.Any() &&
                                       !App.DbContext.Courses.Any() &&
                                       !App.DbContext.Assignments.Any() &&
                                       !App.DbContext.Submissions.Any();

                if (isDatabaseEmpty)
                {
                    bool createTestData = await DisplayAlert("Database is empty",
                        "The database is empty. Do you want to create test data?", "Yes", "No");

                    if (createTestData)
                    {
                        ((App)Application.Current).CreateTestData();

                        await DisplayAlert("Success", "Test data has been created successfully!", "OK");
                    }

                    else
                    {
                        await DisplayAlert("Notice", "No data available to display.", "OK");

                        return;
                    }
                }

                await Shell.Current.GoToAsync(nameof(ViewDataPage));
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }

        }



        private async void OnAddNewAssignmentClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewAssignmentPage));
        }



        private async void OnAddNewSubmissionClicked(object sender, EventArgs e)
        {
            var newSubmissionPage = new NewSubmissionPage();

            await Navigation.PushAsync(newSubmissionPage);
        }



        private async void OnAssignmentsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AssignmentListPage)); //Šo palīdzēja AI, jo iepriekšējais variants negāja
        }



        private async void OnSubmissionsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SubmissionListPage));
        }



        private void OnCreateTestDataClicked(object sender, EventArgs e)
        {
            try
            {
                ((App)Application.Current).CreateTestData(); //piekļūst App metodei

                DisplayAlert("Success", "Test data has been created successfully!", "OK");
            }

            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to create test data: {ex.Message}", "OK");
            }
        }

    }
}