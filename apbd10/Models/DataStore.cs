namespace apbd10.Models;
using apbd10.DTOs;

public static class DataStore
{
    public static List<StudentDto> Students { get; set; } = new()
    {
        new() { Id = 1, IndexNumber = "s12345", FirstName = "John", LastName = "Doe", Email = "john@test.com", Semester = 3 },
        new() { Id = 2, IndexNumber = "s67890", FirstName = "Anna", LastName = "Smith", Email = "anna@test.com", Semester = 5 }
    };

    public static List<CourseDto> Courses { get; set; } = new()
    {
        new() { Id = 1, Name = "C# Programming", Ects = 5 },
        new() { Id = 2, Name = "Web Applications", Ects = 6 }
    };

    public static List<StudentCourseDto> StudentCourses { get; set; } = new();
}