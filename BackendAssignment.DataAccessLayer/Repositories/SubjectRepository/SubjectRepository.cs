using BackendAssignment.DataAccessLayer.DBContexts;
using BackendAssignment.DataAccessLayer.Entities;
using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.DataAccessLayer.Repositories.SubjectRepository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SubjectDto> Add(SubjectCreateRequest subject)
        {
            Subject newSubject = new Subject
            {
                Name = subject.Name,
                Code = subject.Code,
                Teacher = subject.Teacher
            };

            await _context.Subjects.AddAsync(newSubject);
            await _context.SaveChangesAsync();

            return new SubjectDto
            {
                Id = newSubject.Id,
                Name = newSubject.Name,
                Code = newSubject.Code,
                Teacher = newSubject.Teacher
            };
        }

        public async Task<SubjectDto?> Delete(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return null;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                Teacher = subject.Teacher
            };
        }

        public async Task<PaginatedResultDto<SubjectDto>> GetAll(int page, int pageSize)
        {
            var query = _context.Subjects.AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.Name)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Teacher = s.Teacher
                })
                .ToListAsync();

            return new PaginatedResultDto<SubjectDto> { Items = items, TotalCount = totalCount, Page = page, PageSize = pageSize };
        }

        public async Task<SubjectDto?> GetById(int id)
        {
            return await _context.Subjects
                .Where(s => s.Id == id)
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Teacher = s.Teacher
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PaginatedResultDto<StudentDto>> GetStudentsBySubjectId(int subjectId, int page, int pageSize)
        {
            var query = _context.StudentSubjects
                .Where(ss => ss.SubjectId == subjectId)
                .Select(ss => ss.Student); // Fetching students from the many-to-many table

            var totalCount = await query.CountAsync();
            var students = await query
                .OrderBy(s => s.Name) // Sorting by Name
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

            return new PaginatedResultDto<StudentDto>
            {
                Items = students,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<SubjectDto?> Update(SubjectDto subject)
        {
            var subjectfound = await _context.Subjects.FindAsync(subject.Id);
            if (subjectfound == null) return null;

            subjectfound.Name = subject.Name;
            subjectfound.Code = subject.Code;
            subjectfound.Teacher = subject.Teacher;

            _context.Subjects.Update(subjectfound);
            await _context.SaveChangesAsync();

            return new SubjectDto
            {
                Id = subjectfound.Id,
                Name = subjectfound.Name,
                Code = subjectfound.Code,
                Teacher = subjectfound.Teacher
            };
        }
    }
}
