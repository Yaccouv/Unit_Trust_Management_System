using Unit_Trust_Backend.DTOs;

namespace Unit_Trust_Backend.Investor.Interfaces
{
    public interface IUserService
    {

        Task<bool> RegisterUserAsync(UserSignupDTO signupDTO);
        Task<bool> LoginUserAsync(UserSignInDTO signInDTO);
    }
}
