using BackendAssignment.DataAccess.Repositories.StudentRepository;
using BackendAssignment.DataAccessLayer.DBContexts;
using BackendAssignment.DataAccessLayer.Entities;
using BackendAssignment.Model.Dtos;
using BackendAssignment.Model.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.DataAccessLayer.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<StudentDto> Add(StudentCreateRequest student)
        {
            Student newStudent = new Student
            {
                Name = student.Name,
                Age = student.Age,
                Address = student.Address,
                ClassId = student.ClassId,
                StudentSubjects = new List<StudentSubject>()
            };

            if (student.SubjectIds != null && student.SubjectIds.Count > 0)
            {
                foreach (var subjectId in student.SubjectIds)
                {
                    newStudent.StudentSubjects.Add(new StudentSubject
                    {
                        SubjectId = subjectId
                    });
                }
            }

            await _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();

            return new StudentDto
            {
                Id = newStudent.Id,
                Name = newStudent.Name,
                Age = newStudent.Age,
                Address = newStudent.Address,
                ClassId = newStudent.ClassId,
                SubjectIds = newStudent.StudentSubjects.Select(ss => ss.SubjectId).ToList()
            };
        }

        public async Task<StudentDto?> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return null;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Address = student.Address,
                ClassId = student.ClassId
            };
        }

        public async Task<PaginatedResultDto<StudentDto>> GetAll(int page, int pageSize)
        {
            var query = _context.Students.AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.Name)  // Default sorting by Name
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    Address = s.Address,
                    ClassId = s.ClassId
                })
                .ToListAsync();

            return new PaginatedResultDto<StudentDto> { Items = items, TotalCount = totalCount, Page = page, PageSize = pageSize };
        }

        public async Task<PaginatedResultDto<StudentDto>> GetByClassId(int classId, int page, int pageSize)
        {
            var query = _context.Students.Where(s => s.ClassId == classId);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.Name)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    Address = s.Address,
                    ClassId = s.ClassId
                })
                .ToListAsync();

            return new PaginatedResultDto<StudentDto> { Items = items, TotalCount = totalCount, Page = page, PageSize = pageSize };
        }

        public async Task<StudentDto?> GetById(int id)
        {
            return await _context.Students
               .Where(s => s.Id == id)
               .Select(s => new StudentDto
               {
                   Id = s.Id,
                   Name = s.Name,
                   Age = s.Age,
                   Address = s.Address,
                   ClassId = s.ClassId
               })
               .FirstOrDefaultAsync();
        }

        public async Task<PaginatedResultDto<StudentDto>> GetBySubjectId(int subjectId, int page, int pageSize)
        {
            var query = _context.Students
                .Include(s => s.StudentSubjects)
                .Where(s => s.StudentSubjects.Any(y => y.Id == subjectId));

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.Name)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    Address = s.Address,
                    ClassId = s.ClassId
                })
                .ToListAsync();

            return new PaginatedResultDto<StudentDto> { Items = items, TotalCount = totalCount, Page = page, PageSize = pageSize };
        }

        public async Task<StudentDto?> Update(StudentDto student)
        {
            var studentFound = await _context.Students.FindAsync(student.Id);
            if (studentFound == null) return null;

            studentFound.Name = student.Name;
            studentFound.Age = student.Age;
            studentFound.Address = student.Address;
            studentFound.ClassId = student.ClassId;

            _context.Students.Update(studentFound);
            await _context.SaveChangesAsync();

            return new StudentDto
            {
                Id = studentFound.Id,
                Name = studentFound.Name,
                Age = studentFound.Age,
                Address = studentFound.Address,
                ClassId = studentFound.ClassId
            };
        }
    }
}
