using DataAccess.Repository;
using EATS365_Library.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EATS365_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IAccountRepository _accountRepository;

        public LoginController() => _accountRepository = new AccountRepository();

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var accountDTO = _accountRepository.Login(loginDTO.AccountEmail, loginDTO.AccountPassword);

                if (accountDTO == null)
                {
                    return Ok(new APIResponseDTO
                    {
                        Success = false,
                        Message = "Email hoặc password không đúng! Vui lòng đăng nhập lại!"
                    });
                }

                // Create token


                return Ok(new APIResponseDTO
                {
                    Success = true,
                    Message = "Đăng nhập thành công!",
                    Data = _accountRepository.GenerageToken(accountDTO)
                });

            } catch
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = "Email hoặc password không đúng! Vui lòng đăng nhập lại!"
                });
            }
        }
    }
}
