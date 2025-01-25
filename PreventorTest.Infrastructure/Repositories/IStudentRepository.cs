using PreventorTest.Application.Students.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreventorTest.Application.Students.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> Get();
        Task<Student?> GetById(int studentId);
        Task<Student> Add(StudentDTO student);
        Task<Student?> Update(StudentDTO student, int studentId);
        Task<bool> DeleteById(int studentId);
    }
}
