using System;
using System.Xml.Linq;

namespace ClassREST.Model
{
    public class StudentRepositoryList : IStudentRepository
    {
        private List<Student> _student;
        private int _nextId;

        public StudentRepositoryList() 
        {
            _nextId = 1;
            _student = new List<Student>() {
                new Student() { Id = _nextId++, Name = "Phillip", BirthYear =1963 },
                new Student() { Id = _nextId++, Name = "Nichlas", BirthYear =1967 },
                new Student() { Id = _nextId++, Name = "Christian", BirthYear =1972 }
            };
        }

        public IEnumerable<Student> Get(string? nameFilter,
            int? minBirthYear,
            int? maxBirthYear,
            int? amount)
        {
            List<Student> query = new List<Student>(_student);

            if (nameFilter != null)
            {
                query = query.FindAll(student =>
                student.Name.Contains(nameFilter,
                StringComparison.InvariantCultureIgnoreCase
                ));
            }

            if (minBirthYear != null)
            {
                query = query.FindAll(student =>
                student.BirthYear >= minBirthYear);
            }

            if (maxBirthYear != null)
            {
                query = query.FindAll(student =>
                student.BirthYear <= maxBirthYear);
            }

            if (amount != null)
            {
                query = query.Take(amount.Value).ToList();
            }

            return query;
        }

        public Student? GetById(int id)
        {
            return _student.Find(student => student.Id == id);
        }

        public Student Add(Student newStudent)
        {
            newStudent.Validate();
            newStudent.Id = _nextId++;
            _student.Add(newStudent);
            return newStudent;
        }

        public Student? Delete(int Id)
        {
            Student? toBeDeleted = GetById(Id);
            if (toBeDeleted != null)
            {
                _student.Remove(toBeDeleted);
            }
            return toBeDeleted;
        }

        public Student? Update(int Id, Student updates)
        {
            updates.Validate();
            Student? toBeUpdated = GetById(Id);
            if (toBeUpdated != null)
            {
                toBeUpdated.Name = updates.Name;
                toBeUpdated.BirthYear = updates.BirthYear;
            }
            return toBeUpdated;
        }
    }
}
