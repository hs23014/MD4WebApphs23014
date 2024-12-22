using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MD4WebApphs23014.Data;
using MD4WebApphs23014.Models;

namespace MD4WebApphs23014.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolDbContext _context;

        public CoursesController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var schoolDbContext = _context.Courses.Include(c => c.Teacher);
            return View(await schoolDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            //1

            //aizvieto:
            var teachers = _context.Teachers.ToList();
            if (!teachers.Any())
            {
                Console.WriteLine("No teachers found in database.");
                ModelState.AddModelError("", "No teachers found. Please add teachers first.");
                return View();
            }

            foreach (var teacher in teachers)
            {
                Console.WriteLine($"TeacherId: {teacher.TeacherId}, Name: {teacher.Name}, Surname: {teacher.Surname}");
            }

            ViewBag.Teachers = new SelectList(teachers, "TeacherId", "Surname");
            Console.WriteLine($"Teachers count: {teachers.Count}");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Name,TeacherId")] Course course)
        {
            Console.WriteLine($"Course: {course.Name}, TeacherId: {course.TeacherId}"); // Kursa un skolotāja ID pārbaude
            if (course.TeacherId == 0)
            {
                ModelState.AddModelError("TeacherId", "Please select a valid teacher.");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid. Debugging errors:");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }


            Console.WriteLine($"ModelState is valid: {ModelState.IsValid}"); // Izdrukā ModelState statusu

            if (ModelState.IsValid)
            {
                //2

                try
                {
                    _context.Add(course);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Course saved successfully.");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving course: {ex.Message}");
                }
            }
            //3


            //4

            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", course.TeacherId);
            ViewBag.Teachers = new SelectList(_context.Teachers, "TeacherId", "Surname", course.TeacherId);
            Console.WriteLine($"Course: {course.Name}, TeacherId: {course.TeacherId}");
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", course.TeacherId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Name,TeacherId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", course.TeacherId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}


//1
//ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
//ViewBag.Teachers = new SelectList(_context.Teachers, "TeacherId", "Surname"); // Saista TeacherId ar Teacher Name
//return View();


//2
/*_context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));*/

//3
/*else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }*/


//4
/*Console.WriteLine($"ModelState is valid: {ModelState.IsValid}");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }*/



/*if (!ModelState.IsValid)
{
    //ViewBag.Teachers = new SelectList(_context.Teachers, "TeacherId", "Surname", course.TeacherId);
    //return View(course);
    Console.WriteLine($"ModelState is valid: {ModelState.IsValid}");
    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
    }
}*/


//5
