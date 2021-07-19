using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NRI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class GITController : ControllerBase
    {
        private readonly ILogger<GITController> _logger;

        public GITController(ILogger<GITController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUserRepository")]
        public IActionResult GetUserRepositaryAsync([FromQuery] QueryParameters parameters)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var client = new RestClient("https://api.github.com/users/" + parameters.user + "/repos");
                    var request = new RestRequest(Method.GET);

                    request.AddHeader("Content-Type", "application/vnd.github.v3+json");
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Authorization", "Basic " + parameters.token);

                    IRestResponse response = client.Execute(request);
                    int StatusCode = (int)response.StatusCode;
                    if (StatusCode == 200)
                    {
                        List<RepoList> data = JsonConvert.DeserializeObject<List<RepoList>>(response.Content);
                        if (data == null)
                        {
                            return NotFound();
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Repositories_Name");
                        for (int i = 0; i < data.Count; i++)
                        {
                            var repo = data[i];
                            sb.Append(repo.name + ',');
                            sb.Append("\r\n");

                        }
                        return File(Encoding.UTF8.GetBytes
                        (sb.ToString()), "text/csv", "Reports.csv");
                    }
                    else
                    {
                        return Ok(response.StatusDescription);
                    }

                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }


            return BadRequest();
        }


    }
}
