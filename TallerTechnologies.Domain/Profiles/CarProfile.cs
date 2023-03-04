using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO = TallerTechnologies.Infrastructure.Models;
using ViewModels = TallerTechnologies.Domain.Models;

namespace TallerTechnologies.Domain.AutoMapper.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<DTO.Car, ViewModels.CarViewModel>();
            CreateMap<ViewModels.CarViewModel, DTO.Car>();
        }
    }
}
