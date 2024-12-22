using System.Collections.ObjectModel;
using MD1_hs23014;
namespace md2hs23014;

public partial class NewStudentPage : ContentPage
{
    public NewStudentPage() // Konstruktors bez parametriem
    {
        InitializeComponent();
    }



    private async void OnAddStudentClicked(object sender, EventArgs e)  //Pievieno jauno studentus
	{
        if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(SurnameEntry.Text) ||
            string.IsNullOrWhiteSpace(StudentIdEntry.Text) || GenderPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "All fields must be filled.", "OK");

            return;
        }

        var newStudent = new Student
		{
			Name = NameEntry.Text,

			Surname = SurnameEntry.Text,

			StudentIdNumber = StudentIdEntry.Text,

            Gender = GenderPicker.SelectedItem.ToString()
        };

        try
        {
           App.DbContext.Students.Add(newStudent); // Pievieno jauno studentu datubāzei

           App.DbContext.SaveChanges(); // Saglabā izmaiņas

            await DisplayAlert("Success", "Student added successfully.", "OK");

            await Shell.Current.GoToAsync("//MainPage"); // Atgriežas uz galveno lapu
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add student: {ex.Message}", "OK");
        }
    }
}