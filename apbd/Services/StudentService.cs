using System;
using System.Collections.Generic;
using System.Linq;
using cw10.Models;

namespace cw10.Services
{
    public class StudentService : IStudentDbService
    {
        private SampleDbContext _dbcontext;

        public StudentService(SampleDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Student> GetStudents() => _dbcontext.Student.ToList();

        public Student GetStudent(string index) => _dbcontext.Student.First(s => s.IndexNumber == index);

        public Student AddStudent(Student student)
        {
            var result = _dbcontext.Student.Add(student).Entity;
            _dbcontext.SaveChanges();
            return result;
        }

        public Student ModifyStudent(Student updated)
        {
            var result = _dbcontext.Update(updated).Entity;
            _dbcontext.SaveChangesAsync();
            return result;
        }

        public Student DeleteStudent(string index)
        {
            var student = new Student { IndexNumber = index };
            _dbcontext.Attach(student);
            var response = _dbcontext.Remove(student).Entity;
            _dbcontext.SaveChanges();
            return response;
        }
    }
}