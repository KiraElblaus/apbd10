using Microsoft.AspNetCore.Mvc;
using apbd10.Models;

[ApiController]
[Route("api/[controller]")] 
public class CoursesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCourses()
    {
        return Ok(DataStore.Courses);
    }
}