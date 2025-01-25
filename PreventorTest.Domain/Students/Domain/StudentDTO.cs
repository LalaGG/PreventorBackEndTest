using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreventorTest.Application.Students.Domain
{
    public class StudentDTO
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int? DocumentType { get; set; }
        public string? Passport { get; set; }
        public string? Email { get; set; }
    }
}
