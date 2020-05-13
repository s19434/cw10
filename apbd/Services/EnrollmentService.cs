using System;
using System.Collections.Generic;
using System.Linq;
using cw10.DTOs.Requests;
using cw10.DTOs.Responses;
using cw10.Models;

namespace cw10.Services
{
    public class EnrollmentService : IEnrollmentDbService
    {
        private readonly SampleDbContext _dbcontext;

        public EnrollmentService(SampleDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public bool IsExist(string name) => _dbcontext.Studies.Count(n => n.Name == name) > 0;

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest enrollment)
        {
            var id = _dbcontext.Studies.First(s => s.Name == enrollment.Studies).IdStudy;
            var recentEnrollment = _dbcontext.Enrollment
                .Where(predicate: e => e.Semester == 1 && e.IdStudy == id).OrderByDescending(keySelector: e => e.IdEnrollment).FirstOrDefault();


            if (recentEnrollment == null)
            {
                recentEnrollment = _dbcontext.Enrollment.Add(entity: new Enrollment
                {
                    IdStudy = id,
                    StartDate = DateTime.Now,
                    Semester = 1,
                    Student = new HashSet<Student>()
                }).Entity;
            }

            _ = _dbcontext.Student.Add(entity: new Student
            {
                IndexNumber = enrollment.IndexNumber,
                FirstName = enrollment.FirstName,
                LastName = enrollment.LastName,
                BirthDate = DateTime.Parse(enrollment.BirthDate),
                IdEnrollment = recentEnrollment.IdEnrollment,
            });

            _ = _dbcontext.SaveChanges();

            return new EnrollStudentResponse
            {
                IdStudy = recentEnrollment.IdStudy,
                IdEnrollment = recentEnrollment.IdEnrollment,
                Semester = recentEnrollment.Semester,
                StartDate = recentEnrollment.StartDate,
            };
        }

        public bool EnrollmentExists(string studies, int semester) => _dbcontext.Enrollment.Count(e =>
                                                                                    e.IdStudy == _dbcontext.Studies.First(s => s.Name == studies).IdStudy &&
                                                                                    e.Semester == semester) > 0;

        public Enrollment Promote(PromoteStudentRequest promotion)
        {
            var id = _dbcontext.Studies.FirstOrDefault(predicate: s => s.Name == promotion.Studies).IdStudy;

            var currentEnrollment = _dbcontext.Enrollment
                .FirstOrDefault(e =>
                    e.IdStudy == id &&
                    e.Semester == promotion.Semester
                );

            var nextEnrollment = _dbcontext.Enrollment
                .FirstOrDefault(e =>
                    e.IdStudy == id &&
                    e.Semester == promotion.Semester + 1
                );


            if (nextEnrollment == null)
            {
                nextEnrollment = new Enrollment
                {
                    IdEnrollment = _dbcontext.Enrollment.Max(selector: e => e.IdEnrollment) + 1,
                    Semester = promotion.Semester + 1,
                    IdStudy = id,
                };
                _dbcontext.Enrollment.Add(nextEnrollment);
                _dbcontext.SaveChanges();
            }


            var enrolledStudents = _dbcontext.Student.Where(s =>
                s.IdEnrollment == currentEnrollment.IdEnrollment
            ).ToList();

            enrolledStudents.ForEach(s => { s.IdEnrollment = nextEnrollment.IdEnrollment; });

            _dbcontext.SaveChanges();
            return nextEnrollment;
        }
    }
}