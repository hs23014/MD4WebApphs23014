using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public class DataCollection
    {
        //SARAKSTI:
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Submission> Submissions { get; set; }

        //KONSTRUKTORI:
        public DataCollection() {
            Teachers = new List<Teacher>();
            Students = new List<Student>();
            Courses = new List<Course>();
            Assignments = new List<Assignment>();
            Submissions = new List<Submission>();
        }

        //METODES:
        public void AddTeacher(Teacher teacher)
        {
            Teachers.Add(teacher);
        }
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
        public void AddAssignment(Assignment assignment)
        {
            Assignments.Add(assignment);
        }
        public void AddSubmission(Submission submission) {
            Submissions.Add(submission); 
        }

        public override string ToString()
        {
            string result = "Data Collection:\n";
            result += "\nTeachers:\n";
            foreach (var t in Teachers)
            {
                result += t.ToString() + "\n";
            }

            result += "\n\nStudents:\n";
            foreach(var st in Students)
            {
                result += st.ToString() + "\n";
            }

            result += "\n\nCourses:\n";
            foreach (var c in Courses)
            {
                result += c.ToString() + "\n";
            }

            result += "\n\nAssignment\n";
            foreach (var a in Assignments)
            {
                result += a.ToString() + "\n";
            }

            result += "\n\nSubmissions:\n";
            foreach (var sb in Submissions)
            {
                result += sb.ToString() + "\n";
            }

            return result;
        }
    }
}
