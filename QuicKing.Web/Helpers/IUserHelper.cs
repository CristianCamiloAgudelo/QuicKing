﻿using Microsoft.AspNetCore.Identity;
using QuicKing.Web.Data.Entities;
using QuicKing.Web.Models;
using System;
using System.Threading.Tasks;

namespace QuicKing.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserAsync(string email);
        
        Task<UserEntity> GetUserAsync(Guid userId);

        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);


        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path);

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);


    }
}
