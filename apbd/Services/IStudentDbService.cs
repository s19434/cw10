using System.Collections.Generic;
using cw10.Models;

namespace cw10.Services
{
    public interface IStudentDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student GetStudent(string index);
        public Student AddStudent(Student student);
        public Student ModifyStudent(Student updated);
        public Student DeleteStudent(string index);
    }
}