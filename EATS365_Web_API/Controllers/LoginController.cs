using DataAccess.Repository;
using EATS365_Library.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Threading.Tasks;
using System;
using EATS365_Library.EATS365_Exception;

namespace EATS365_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public LoginController() => _accountRepository = new AccountRepository();

        [HttpPost]
        public async Task<ActionResult<APIResponseDTO>> Login(LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.AccountEmail) || string.IsNullOrEmpty(loginDTO.AccountPassword))
            {
                return BadRequest(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "Email or Password can not be empty!",
                    StatusCode = 400,
                    Data = null
                });
            }

            try
            {
                var accountDTO = await _accountRepository.LoginAsync(loginDTO.AccountEmail, loginDTO.AccountPassword);

                var token = _accountRepository.GenerateToken(accountDTO);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    UserMessage = "Login successful!",
                    StatusCode = 200,
                    Data = token
                });
            } catch (AccountNotActivatedException ex)
            {
                return NotFound(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = ex.Message,
                    InternalMessage = ex.ToString(),
                    StatusCode = 404,
                    Data = null
                });
            }
            catch (AccountNotFoundException ex)
            {
                return NotFound(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = ex.Message,
                    InternalMessage = ex.ToString(),
                    StatusCode = 404,
                    Data = null
                });
            }
            catch (InvalidCredentialsException ex)
            {
                return BadRequest(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = ex.Message,
                    InternalMessage = ex.ToString(),
                    StatusCode = 400,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "An error occurred during processing. Please try again later!",
                    InternalMessage = ex.ToString(),
                    StatusCode = 500,
                    Data = null
                });
            }
        }
    }

}
