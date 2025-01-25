using AutoMapper;
using PreventorTest.Application.Students;
using PreventorTest.Application.Students.Domain;

namespace PreventorTest.Infrastructure
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
