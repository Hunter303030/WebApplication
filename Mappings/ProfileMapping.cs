﻿using AutoMapper;
using WebApplication.Dto.Profile;
using WebApplication.Models;

namespace WebApplication.Mappings
{
    public class ProfileMapping : AutoMapper.Profile
    {
        public ProfileMapping()
        {
            CreateMap<Models.Profile, ProfileAuthDto>();
            CreateMap<Models.Profile, ProfileEditDto>();
            CreateMap<ProfileEditPasswordDto, Models.Profile>();

            CreateMap<ProfileAuthDto, Models.Profile>();
            CreateMap<ProfileEditDto, Models.Profile>();
            CreateMap<ProfileEditPasswordDto, Models.Profile>();
        }
    }
}