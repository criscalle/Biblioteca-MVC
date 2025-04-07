using AutoMapper;
using Biblioteca_MVC.Controllers.Features;

namespace Biblioteca_MVC.Models.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Persona, PersonaDTO>();
        CreateMap<MaterialAcademico, MaterialAcademicoDTO>();

    }
}
