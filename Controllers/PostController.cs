using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            Microsoft.Extensions.Primitives.StringValues token;
            bool getmeToken = Request.Headers.TryGetValue("Authorization", out token);
            if (getmeToken)
            {
                IFlurlResponse response = await $"{Constants.PostApiUrl}/api/post/postmessage".WithHeader("Authorization", token).PostJsonAsync(postDTO);
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
            else
            {
                return StatusCode(500);
            }
         
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("getusermessages/{username}")]
        public async Task<ActionResult<ICollection<PostDTO>>> GetUserMessages(string username)
        {
            Microsoft.Extensions.Primitives.StringValues token;
            bool getmeToken = Request.Headers.TryGetValue("Authorization", out token);
            if (getmeToken)
            {
                IFlurlResponse response = await $"{Constants.PostApiUrl}/api/post/getusermessages/{username}".WithHeader("Authorization", token).GetAsync();
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
                    ICollection<PostDTO> answer = await response.GetJsonAsync<ICollection<PostDTO>>();
                    return Ok(answer);
                }
            }
            else
            {
                return StatusCode(500);
            }
          
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("getmessages/{searchterm}")]
        public async Task<ActionResult<ICollection<PostDTO>>> GetMessages(string searchterm)
        {
            Microsoft.Extensions.Primitives.StringValues token;
            bool getmeToken = Request.Headers.TryGetValue("Authorization", out token);
            if (getmeToken)
            {
                IFlurlResponse response = await $"{Constants.PostApiUrl}/api/post/getmessages/{searchterm}".WithHeader("Authorization", token).GetAsync();
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
                    ICollection<PostDTO> answer = await response.GetJsonAsync<ICollection<PostDTO>>();
                    return Ok(answer);
                }
            }
            else
            {
                return StatusCode(500);
            }
           
        }
    }
}
