using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PreventorTest.Application.Students;
using PreventorTest.Application.Students.Domain;
using PreventorTest.Application.Students.Interfaces;
using PreventorTest.Infrastructure.EntityFramework.Contexts;

namespace PreventorTest.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly PreventorDBContext _context;
        private readonly Mapper _mapper;

        public StudentRepository(PreventorDBContext context)
        {
            _context = context;
            _mapper = MapperConfig.InitializeAutomapper();
        }

        public async Task<List<Student>> Get()
        {
            return await _context.Student.AsNoTracking().ToListAsync();
        }

        public async Task<Student?> GetById(int studentId)
        {
            return await _context.Student.FirstOrDefaultAsync(x => x.StudentId == studentId);
        }

        public async Task<Student> Add(StudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<Student?> Update(StudentDTO studentDTO, int studentId)
        {
            var student = await _context.Student.AsNoTracking().FirstOrDefaultAsync(x => x.StudentId.Equals(studentId));
            if(student != null)
            {
                _context.Entry(student).CurrentValues.SetValues(studentDTO);
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return student;
            }

            return null;
        }

        public async Task<bool> DeleteById(int studentId)
        {
            var student = await _context.Student.AsNoTracking().FirstOrDefaultAsync(x => x.StudentId.Equals(studentId));
            if (student != null)
            {
                _context.Remove(student);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
