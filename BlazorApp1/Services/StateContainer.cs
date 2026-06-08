namespace BlazorApp1.Services;
using apbd10.DTOs;

public class StateContainer
{
    public List<StudentDto> ObservedStudents { get; private set; } = new();

    public event Action? OnChange;

    public void AddObservedStudent(StudentDto student)
    {
        if (!ObservedStudents.Any(s => s.Id == student.Id))
        {
            ObservedStudents.Add(student);
            NotifyStateChanged();
        }
    }

    public void RemoveObservedStudent(int id)
    {
        var student = ObservedStudents.FirstOrDefault(s => s.Id == id);
        if (student != null)
        {
            ObservedStudents.Remove(student);
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}