using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Contratos;
using ProEventos.Application.DTOs;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);

                return Ok(user);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuário. Erro: {ex.Message}");

            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.Username)) return BadRequest("Usuário já existe.");

                var user = await _accountService.CreateAccountAsync(userDto);

                if (user != null)
                    return Ok(user);


                return BadRequest("Usuário não criado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar o usuário. Erro: {ex.Message}");

            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLoginDto.Username);
                if (user == null) return Unauthorized("Usuário ou Senha está errado");

                var result = await _accountService.checkUserPasswordAsync(user, userLoginDto.Password);
                if (!result.Succeeded) return Unauthorized();

                return Ok(new
                {
                    userName = user.UserName,
                    PrimeiroNome = user.PrimeiroNome,
                    token = await _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar login do usuário. Erro: {ex.Message}");

            }
        }

        [HttpPut("UpdateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userDto)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Usuário inválido.");

                var userRetorno = await _accountService.UpdateAccount(userDto);

                if (userRetorno == null)
                    return NoContent();

                return Ok(userRetorno);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar usuário. Erro: {ex.Message}");

            }
        }
    }
}
