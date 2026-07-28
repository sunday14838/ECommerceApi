using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ECommerceApi.Services
{
#pragma warning disable CS1591
    public class AuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly PasswordService passwordService;
        private readonly JwtService jwtService;
        private readonly ILogger<AuthService> logger;

        public AuthService(IAuthRepository authRepository, PasswordService passwordService,
            JwtService jwtService,
            ILogger<AuthService> logger)
        {
            this.authRepository = authRepository;
            this.passwordService = passwordService;
            this.jwtService = jwtService;
            this.logger = logger;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            if (await authRepository.Exists(dto.Email))
                throw new Exception("User Already Exists");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = passwordService.HashPassword(dto.Password),
               
            };

           await authRepository.Add(user);
           await authRepository.SaveChangesAsync();
            logger.LogInformation("New user registered: {Email}", dto.Email);

            return "User Registered Successfully";
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await authRepository.GetByEmail(dto.Email);

            if (user == null)
            {
                logger.LogWarning("Invalid login attempt for {Email}", dto.Email);

                passwordService.VerifyPassword("", "");
                throw new UnauthorizedAccessException("Invalid Email");
            }

            if (!passwordService.VerifyPassword(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid Password");
            }

            logger.LogInformation("User {Email} logged in successfully", dto.Email);

            return jwtService.GenerateToken(user);
        }
    }
}
