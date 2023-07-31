using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Application.Mapping
{
    public class ModelToDTOMapping : Profile
    {
        public ModelToDTOMapping()
        {
            CreateMap<Person, PersonDTO>();
        }
    }
}