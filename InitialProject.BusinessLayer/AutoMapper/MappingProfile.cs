using Kawkaba.Core.DTO.AuthViewModel.FilesModel;
using Kawkaba.Core.DTO.AuthViewModel.RegisterModel;
using Kawkaba.Core.DTO.AuthViewModel.RoleModel;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kawkaba.Core.Entity.RequestEmployee;
using Kawkaba.Core.DTO.AuthViewModel.RequestEmployeeModel;
using Kawkaba.Core.DTO.AuthViewModel.PostsModel;
using Kawkaba.Core.Entity.Posts;

namespace Kawkaba.BusinessLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //--------------------------------------------------------------------------------------------------------
            // Mapping for RoleDTO <-> ApplicationRole
            CreateMap<RoleDTO, ApplicationRole>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.ArName, opt => opt.MapFrom(src => src.RoleAr))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.RoleDescription))
                .ReverseMap();

            //--------------------------------------------------------------------------------------------------------
            // Mapping for Paths <-> PathsModel
            CreateMap<Paths, PathsModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

            //--------------------------------------------------------------------------------------------------------
            // Mapping for ApplicationUser <-> RegisterAdmin
            CreateMap<ApplicationUser, RegisterAdmin>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            //--------------------------------------------------------------------------------------------------------
            // Mapping for ApplicationUser <-> RegisterSupportDeveloper
            CreateMap<ApplicationUser, RegisterSupportDeveloper>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            //--------------------------------------------------------------------------------------------------------
            // Mapping for ApplicationUser <-> RegisterCustomer
            CreateMap<ApplicationUser, RegisterCompany>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            //--------------------------------------------------------------------------------------------------------
            // Mapping for ApplicationUser <-> RegisterCustomer
            CreateMap<ApplicationUser, RegisterEmployee>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            //--------------------------------------------------------------------------------------------------------
            // Mapping for ApplicationUser <-> AuthDTO
            CreateMap<ApplicationUser, AuthDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore()) // Manually handle file uploads
                .ForMember(dest => dest.ProfileImageId, opt => opt.MapFrom(src => src.ProfileId))
                .ReverseMap();
            //--------------------------------------------------------------------------------------------------------
            // Mapping for RequestEmployee <-> RequestEmployeeDTO
            CreateMap<RequestEmployee, RequestEmployeeDTO>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            //------------------------------------------------------------------------------------------------------------
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.FileUrls, opt => opt.Ignore())
                .ForMember(dest => dest.Files, opt => opt.Ignore());

            // Map PostDTO to Post entity (for creating new posts)
            CreateMap<PostDTO, Post>()
                .ForMember(dest => dest.Files, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());


        }
    }
}
