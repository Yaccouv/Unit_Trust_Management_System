using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Unit_Trust_Backend.Data;
using Unit_Trust_Backend.DTOs;
using Unit_Trust_Backend.Investor.Interfaces;
using Unit_Trust_Backend.Investor.Models;

namespace Unit_Trust_Backend.Services
{
    public class UserService : IUserService
    {

        private readonly MyDatabase _database;
        private readonly IInvestorService _investorService;

        public UserService(MyDatabase database, IInvestorService investorService)
        {
            _database = database;
            _investorService = investorService;
        }

        public async Task<bool> RegisterUserAsync(UserSignupDTO signupDTO)
        {
            if (_database.Users.Any(u => u.Email == signupDTO.Email))
                return false;

            var user = new User
            {
                Email = signupDTO.Email,
                PasswordHash = HashPassword(signupDTO.Password),
                Firstname = signupDTO.Firstname,
                Lastname = signupDTO.Lastname
            };

            _database.Users.Add(user);
            await _database.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("_", "").ToLower();
            }
        }


        public async Task<bool> LoginUserAsync(UserSignInDTO signInDTO)
        {
            var user = await _database.Users
                .FirstOrDefaultAsync(u => u.Email == signInDTO.Email);
            if (user == null || !VerifyPassword(signInDTO.Password, user.PasswordHash))
            {
                return false;
            }
            var users = _database.Users.FirstOrDefault(u => u.Email == signInDTO.Email);
            if (users != null)
            {
                var userID= users.UserId;
                await _investorService.GetUserId(userID);
            }
            return true;
        }

        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Hash the entered password
            string enteredPasswordHash = HashPassword(enteredPassword);

            // Compare the hashed entered password with the stored password hash
            return enteredPasswordHash == storedPasswordHash;
        }

    }
}
