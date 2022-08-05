﻿using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "Numsey#2022").Result;

            if (result.Succeeded) _userManager.AddToRoleAsync(user, "User").Wait();
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "Numsey#2022").Result;

            if (result.Succeeded) _userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole role = new()
            {
                Name = "User",
                NormalizedName = "USER"
            };

            var roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var roleResult = _roleManager.CreateAsync(role).Result;
        }
    }
}