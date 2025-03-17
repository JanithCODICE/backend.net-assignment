using BackendAssignment.DataAccessLayer.DBContexts;
using BackendAssignment.DataAccessLayer.Repositories.Class;
using BackendAssignment.Model.Dtos;
using BackendASsignment.Model.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.DataAccessLayer.Repositories.ClassRepository
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ClassDto> Add(ClassCreateRequest classEntity)
        {
            Entities.Class newClass = new Entities.Class
            {
                ClassName = classEntity.ClassName,
                Grade = classEntity.Grade,
                TeacherInCharge = classEntity.TeacherInCharge
            };

            await _context.Classes.AddAsync(newClass);
            await _context.SaveChangesAsync();

            return new ClassDto
            {
                Id = newClass.Id,
                ClassName = newClass.ClassName,
                Grade = newClass.Grade,
                TeacherInCharge = newClass.TeacherInCharge
            };
        }

        public async Task<ClassDto?> Delete(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null) return null;

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();

            return new ClassDto
            {
                Id = classEntity.Id,
                ClassName = classEntity.ClassName,
                Grade = classEntity.Grade,
                TeacherInCharge = classEntity.TeacherInCharge
            };
        }

        public async Task<PaginatedResultDto<ClassDto>> GetAllClasses(int page, int pageSize)
        {
            var query = _context.Classes.AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(c => c.ClassName)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    ClassName = c.ClassName,
                    Grade = c.Grade,
                    TeacherInCharge = c.TeacherInCharge
                })
                .ToListAsync();

            return new PaginatedResultDto<ClassDto> { Items = items, TotalCount = totalCount, Page = page, PageSize = pageSize };
        }

        public async Task<ClassDto?> GetById(int id)
        {
            return await _context.Classes
               .Where(c => c.Id == id)
               .Select(c => new ClassDto
               {
                   Id = c.Id,
                   ClassName = c.ClassName,
                   Grade = c.Grade,
                   TeacherInCharge = c.TeacherInCharge
               })
               .FirstOrDefaultAsync();
        }

        public async Task<PaginatedResultDto<StudentDto>> GetStudentsByClassId(int classId, int page, int pageSize)
        {
            var query = _context.Students
                 .Where(s => s.ClassId == classId);

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

        public async Task<ClassDto?> Update(ClassDto classEntity)
        {
            var existingEntity = await _context.Classes.FindAsync(classEntity.Id.Value);
            if (existingEntity == null) {
                return null;
            };

            existingEntity.ClassName = classEntity.ClassName;
            existingEntity.Grade = classEntity.Grade;
            existingEntity.TeacherInCharge = classEntity.TeacherInCharge;

            _context.Classes.Update(existingEntity);
            await _context.SaveChangesAsync();

            return new ClassDto
            {
                Id = existingEntity.Id,
                ClassName = existingEntity.ClassName,
                Grade = existingEntity.Grade,
                TeacherInCharge = existingEntity.TeacherInCharge
            };
        }
    }
}
