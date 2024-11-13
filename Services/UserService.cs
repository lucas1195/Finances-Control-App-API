using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories.Interfaces;
using Finances_Control_App_API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finances_Control_App_API.Services
{
    public class UserService(IRepository<User> userRepository, IConfiguration config) : IUserService 
    {
        private readonly IRepository<User> _userRepository = userRepository;

        private readonly IConfiguration _config = config;

        public async Task<LoginResponseDTO> LogginUser(LoginRequestModel loginRequestModel)
        {
            var getUser = FindUserByEmail(loginRequestModel.UserEmail);
            if (getUser.Result == null)
                return new LoginResponseDTO(false, "User Not Found");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginRequestModel.Password, getUser.Result.Password);
            if (checkPassword)

                return new LoginResponseDTO(true, "Login Successfully", GeneretateJWT(getUser.Result));
            else
                return new LoginResponseDTO(false, "Invalid Password");
        }

        public async Task UpdatePassword(User user, string newPassword)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.Password = hashedPassword;

            await _userRepository.UpdateAsync(user);
        }

        // Token creation
        private string GeneretateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.UserEmail!),
                new Claim(ClaimTypes.UserData, user.UserName!)
};

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> FindUserByEmail(string userEmail)
        {
            return  _userRepository.Table.FirstOrDefault(x => x.UserEmail == userEmail);
        }
        

    }
}
