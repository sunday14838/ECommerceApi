using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Services
{
    public class AuthService
    {
        private readonly AppDbContext context;
        private readonly PasswordService passwordService;
        private readonly JwtService jwtService;

        public AuthService(AppDbContext context, PasswordService passwordService, JwtService jwtService)
        {
            this.context = context;
            this.passwordService = passwordService;
            this.jwtService = jwtService;
        }

        public string Register(RegisterDto dto)
        {
            if (context.Users.Any(x => x.Email == dto.Email))
                throw new Exception("User Already Exists");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = passwordService.HashPassword(dto.Password),
               
            };

            context.Add(user);
            context.SaveChanges();

            return "User Registered Successfully";
        }

        public string Login(LoginDto dto)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !passwordService.VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return jwtService.GenerateToken(user);
        }
    }
}
