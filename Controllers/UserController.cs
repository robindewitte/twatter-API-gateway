using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Flurl.Http;
using System.Threading.Tasks;
using twatter_API_gateway.Helper;
using twatter_userservice.DTO;

namespace twatter_API_gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            IFlurlResponse response = await $"https://localhost:5003/api/user/login".PostJsonAsync(loginDTO);

            if (response.StatusCode >= 500)
            {
                return StatusCode(500);
            }
            else if (response.StatusCode >= 400)
            {
                return StatusCode(400);
            }
            else
            {
                string answer = await response.GetStringAsync();
                return answer;
            }
        }



        //bekijk hoe ik de value wil doorgeven voor deze methode

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("register")]
        public async Task<ActionResult<RegisterResponseDTO>> Register(RegisterDTO registerDTO)
        {
            IFlurlResponse response = await $"https://localhost:5003/api/user/register".PostJsonAsync(registerDTO);
            if (response.StatusCode >= 500)
            {
                return StatusCode(500);
            }
            else if (response.StatusCode >= 400)
            {
                return StatusCode(400);
            }
            else
            {
                RegisterResponseDTO returnValue = await response.GetJsonAsync<RegisterResponseDTO>();
                return returnValue;
            }
        }
    }
}
