﻿using AutoMapper;
using WebApplication.Dto.Profile;

namespace WebApplication.Service.Interfase
{
    public interface IProfileService
    {
        public Task<Models.Profile> Select(ProfileAuthDto profileAuthDto);
        public Task<bool> Create(Models.Profile profile);
        public Task<ProfileEditDto> GetProfile(Guid Id);
        public Task<bool> Edit(ProfileEditDto profileEditDto, Guid profile_Id);
        public Task<bool> EditPassword(ProfileEditPasswordDto profileEditDto, Guid profile_Id);
    }
}