using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD1_hs23014;
using Microsoft.EntityFrameworkCore;

namespace md2hs23014
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
                
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    string connectionString = File.ReadAllText(@"C:\Temp\ConnS.txt");

                    optionsBuilder.UseSqlite(connectionString);
                }

                catch (Exception ex)
                {
                    throw new Exception($"Error reading connection string: {ex.Message}");
                }
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(s => s.StudentId);

            modelBuilder.Entity<Teacher>().HasKey(t => t.TeacherId);

            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
            
            modelBuilder.Entity<Assignment>().HasKey(a => a.AssignmentId);
            
            modelBuilder.Entity<Submission>().HasKey(su => su.SubmissionId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentId);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student)
                .WithMany(st => st.Submissions)
                .HasForeignKey(s => s.StudentId);
        }
    }
}