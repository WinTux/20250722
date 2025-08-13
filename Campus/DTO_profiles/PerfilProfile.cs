using AutoMapper;
using Campus.DTO;
using Campus.Models;

namespace Campus.DTO_profiles
{
    public class PerfilProfile : Profile
    {
        public PerfilProfile() {
            CreateMap<Estudiante, EstudianteReadDTO>();
            CreateMap<EstudiantePublisherDTO, Estudiante>()
                .ForMember(
                    dest => dest.fci,
                    opt => opt.MapFrom(src => src.id)
                );
                
        }
    }
}
