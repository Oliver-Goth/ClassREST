namespace ClassREST.Model
{
    public interface IStudentRepository
    {
        Student Add(Student newstudent);
        Student? Update(int Id, Student updates);
        Student? Delete(int Id);
        Student? GetById(int Id);
        IEnumerable<Student> Get(string? namefilter,
            int? minBirthYear,
            int? maxBirthYear,
            int? amount);

    }
}
