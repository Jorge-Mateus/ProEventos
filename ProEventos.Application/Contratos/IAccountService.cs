using Microsoft.AspNetCore.Identity;
using ProEventos.Application.DTOs;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> checkUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto>UpdateAccount(UserUpdateDto userUpdateDto);
    }
}
