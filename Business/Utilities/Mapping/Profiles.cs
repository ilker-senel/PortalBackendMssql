﻿using Business.Models.Request.Create;
using Business.Models.Request.Delete;
using Business.Models.Request.Functional;
using Business.Models.Request.Update;
using Business.Models.Response;
using Infrastructure.Data.Entities;

namespace Business.Utilities.Mapping
{
    public class Profiles : AutoMapper.Profile
    {
        public Profiles()
        {
            //Users
            CreateMap<RegisterDto, User>();
            //CreateMap<UserUpdateDto, User>();
            //CreateMap<ChangePasswordDto, User>();
            CreateMap<User, UserProfileDto>();

            //Roles
            CreateMap<Role, InfoRolDto>();
            CreateMap<CreateRolDto, Role>();
            CreateMap<UpdateRolDto, Role>();

            //Categories
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, InfoCategoryDto>();
            CreateMap<DeleteDto, Category>();//soft delete işlemi için yaptım

        }
    }
}
