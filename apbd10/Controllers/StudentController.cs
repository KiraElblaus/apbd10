using Microsoft.AspNetCore.Mvc;
using apbd10.Models;
using apbd10.DTOs;

[ApiController]
[Route("api/[controller]")] 
public class StudentsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetStudents()
    {
        return Ok(DataStore.Students);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetStudentDetails(int id)
    {
        var student = DataStore.Students.FirstOrDefault(s => s.Id == id);
        if (student == null) 
            return NotFound();

        var assignedCourses = DataStore.StudentCourses
            .Where(sc => sc.StudentId == id)
            .Join(DataStore.Courses, sc => sc.CourseId, c => c.Id, (sc, c) => c)
            .ToList();

        return Ok(new { Student = student, Courses = assignedCourses });
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] StudentDto newStudent)
    {
        newStudent.Id = DataStore.Students.Count > 0 ? DataStore.Students.Max(s => s.Id) + 1 : 1;
        DataStore.Students.Add(newStudent);
        
        return CreatedAtAction(nameof(GetStudentDetails), new { id = newStudent.Id }, newStudent);
    }

    [HttpPost("{id:int}/courses")]
    public IActionResult AssignCourse(int id, [FromBody] int courseId)
    {
        if (!DataStore.Students.Any(s => s.Id == id) || !DataStore.Courses.Any(c => c.Id == courseId)) 
            return BadRequest();

        DataStore.StudentCourses.Add(new StudentCourseDto 
        { 
            StudentId = id, 
            CourseId = courseId, 
            AssignedAt = DateTime.UtcNow 
        });

        return Ok();
    }
}