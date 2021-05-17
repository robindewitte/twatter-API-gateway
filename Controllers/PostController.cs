using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using twatter_API_gateway.DTO;
using twatter_API_gateway.Helper;

namespace twatter_API_gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/post")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PostController : Controller
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("postmessage")]
        public async Task<ActionResult<string>> PostMessage(PostDTO postDTO)
        {
            IFlurlResponse response = await $"https://localhost:5005/api/post/postmessage".PostJsonAsync(postDTO);
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("getusermessages/{username}")]
        public async Task<ActionResult<List<PostDTO>>> GetUserMessages(string username)
        {
            IFlurlResponse response = await $"{Constants.PostApiUrl}/api/post/getusermessages/{username}".GetJsonAsync();
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
                List<PostDTO> answer = await response.GetJsonAsync<List<PostDTO>>();
                return answer;
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("getmessages/{searchterm}")]
        public async Task<ActionResult<List<PostDTO>>> GetMessages(string searchterm)
        {
            IFlurlResponse response = await $"{Constants.PostApiUrl}/api/post/getmessages/{searchterm}".GetJsonAsync();
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
                List<PostDTO> answer = await response.GetJsonAsync<List<PostDTO>>();
                return answer;
            }
        }
    }
}
