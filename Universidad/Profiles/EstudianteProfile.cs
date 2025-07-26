using AutoMapper;
namespace Universidad.Profiles
{
    public class EstudianteProfile : Profile
    {
        public EstudianteProfile() { 
            // Acá se realiza el mapeado entre DTO's y modelos
            CreateMap<Models.Estudiante, DTO.EstudianteReadDTO>(); // --->
            CreateMap<DTO.EstudianteCreateDTO, Models.Estudiante>();
            CreateMap<DTO.EstudianteUpdateDTO, Models.Estudiante>();
            CreateMap<Models.Estudiante, DTO.EstudianteUpdateDTO>();
        }
    }
}
