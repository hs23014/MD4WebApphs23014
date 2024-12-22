using MD4WebApphs23014.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MD4WebApphs23014.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();*/

//aizstājējs:
string connectionString = File.ReadAllText(@"C:\Temp\ConnS.txt");
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();*/

//aizstājējs:
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Pārbauda e-pasta apstiprinājumu
})
.AddEntityFrameworkStores<SchoolDbContext>();


builder.Services.AddControllersWithViews();

var app = builder.Build();





//jauns:
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<SchoolDbContext>();
    dbContext.Database.EnsureCreated(); // Automātiski izveido DB struktūru

    /*if (dbContext.Database.EnsureCreated())
    {
        // Tikai, ja datubāze bija tukša, izsauciet testa datu metodi
        CreateTestData(dbContext);
    }*/

    if (!dbContext.Students.Any() && !dbContext.Teachers.Any())
    {
        CreateTestData(dbContext);
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//jauns:
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

static void CreateTestData(SchoolDbContext context)
{
    try
    {
        if (!context.Teachers.Any() && !context.Students.Any() &&
            !context.Courses.Any() && !context.Assignments.Any() &&
            !context.Submissions.Any())
        {
            var teachers = new List<Teacher>
            {
                new Teacher { Name = "Anna", Surname = "Apsīte", Gender = "Woman", ContractDate = new DateTime(2022, 9, 1) },
                new Teacher { Name = "Elīza", Surname = "Egle", Gender = "Woman", ContractDate = new DateTime(2020, 8, 24) },
                new Teacher { Name = "Gustavs", Surname = "Grauds", Gender = "Man", ContractDate = new DateTime(2008, 7, 19) }
            };

            var students = new List<Student>
            {
                new Student { Name = "Bruno", Surname = "Bērzs", Gender = "Man", StudentIdNumber = "bb1001" },
                new Student { Name = "Diāna", Surname = "Druva", Gender = "Woman", StudentIdNumber = "dd1002" },
                new Student { Name = "Katrīna", Surname = "Kļava", Gender = "Woman", StudentIdNumber = "kk1003" }
            };

            var courses = new List<Course>
            {
                new Course { Name = "Matemātika", Teacher = teachers[0] },
                new Course { Name = "Ģeogrāfija", Teacher = teachers[1] },
                new Course { Name = "Vēsture", Teacher = teachers[2] }
            };

            var assignments = new List<Assignment>
            {
                new Assignment { Description = "Pabeigt 3. darba lapu", Deadline = new DateTime(2024, 10, 20), Course = courses[0] },
                new Assignment { Description = "Izveidot aprakstu par vienu no pasaules okeāniem", Deadline = new DateTime(2024, 10, 16), Course = courses[1] },
                new Assignment { Description = "Uzrakstīt eseju par Ziemassvētku kaujām", Deadline = new DateTime(2024, 11, 25), Course = courses[2] }
            };

            var submissions = new List<Submission>
            {
                new Submission { Assignment = assignments[0], Student = students[0], SubmissionTime = DateTime.Now, Score = 95 },
                new Submission { Assignment = assignments[1], Student = students[1], SubmissionTime = DateTime.Now, Score = 90 },
                new Submission { Assignment = assignments[2], Student = students[2], SubmissionTime = DateTime.Now, Score = 100 }
            };

            context.Teachers.AddRange(teachers);
            context.Students.AddRange(students);
            context.Courses.AddRange(courses);
            context.Assignments.AddRange(assignments);
            context.Submissions.AddRange(submissions);

            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to create test data: {ex.Message}");
    }
}