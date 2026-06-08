using Microsoft.AspNetCore.Mvc;
using apbd10.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors();

var students = new List<StudentDto>
{
    new StudentDto { Id = 1, IndexNumber = "s12345", FirstName = "John", LastName = "Doe", Email = "john@example.com", Semester = 3 }
};
var courses = new List<CourseDto>
{
    new CourseDto { Id = 1, Name = "Blazor Web Development", Ects = 5 },
    new CourseDto { Id = 2, Name = "Databases", Ects = 4 }
};
var studentCourses = new List<StudentCourseDto>();

app.MapGet("/api/students", () => Results.Ok(students));

app.MapGet("/api/students/{id:int}", (int id) => {
    var student = students.FirstOrDefault(s => s.Id == id);
    return student is not null ? Results.Ok(student) : Results.NotFound();
});

app.MapPost("/api/students", (StudentDto student) => {
    student.Id = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
    students.Add(student);
    return Results.Created($"/api/students/{student.Id}", student);
});

app.MapGet("/api/courses", () => Results.Ok(courses));

app.MapPost("/api/students/{id:int}/courses", (int id, int courseId) => {
    studentCourses.Add(new StudentCourseDto { StudentId = id, CourseId = courseId, AssignedAt = DateTime.UtcNow });
    return Results.Ok();
});

app.Run();