namespace BlazorApp1.Services;
using System.Net.Http.Json;
using apbd10.DTOs;

public class StudentsApiClient
{
    private readonly HttpClient _http;

    public StudentsApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<StudentDto>?> GetStudentsAsync() => 
        await _http.GetFromJsonAsync<List<StudentDto>>("/api/students");

    public async Task<StudentDto?> GetStudentAsync(int id) => 
        await _http.GetFromJsonAsync<StudentDto>($"/api/students/{id}");

    public async Task<HttpResponseMessage> CreateStudentAsync(StudentDto student) => 
        await _http.PostAsJsonAsync("/api/students", student);
}