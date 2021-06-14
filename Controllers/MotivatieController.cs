using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using motivatieservice.DTO;
using twatter_API_gateway.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twatter_API_gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotivatieController : Controller
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("motivatie")]
        public async Task<ActionResult<string>> Motivatie(MotivatieDTO motivatieDTO)
        {
            IFlurlResponse response = await $"{Constants.MotivatieApiUrl}/api/motivatie/motivatie".PostJsonAsync(motivatieDTO);
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

    }
}
