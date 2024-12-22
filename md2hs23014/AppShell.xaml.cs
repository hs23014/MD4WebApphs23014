namespace md2hs23014
{
    public partial class AppShell : Shell
    {
        public AppShell()    //lapas maršruta reģistrācija
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ViewDataPage), typeof(ViewDataPage));
            Routing.RegisterRoute(nameof(NewStudentPage), typeof(NewStudentPage));
            Routing.RegisterRoute(nameof(NewAssignmentPage), typeof(NewAssignmentPage));
            Routing.RegisterRoute(nameof(NewSubmissionPage), typeof(NewSubmissionPage));
            Routing.RegisterRoute(nameof(AssignmentListPage), typeof(AssignmentListPage));
            Routing.RegisterRoute(nameof(SubmissionListPage), typeof(SubmissionListPage));
        }
    }
}