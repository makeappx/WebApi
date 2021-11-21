using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("TestAPI")]
    public class TestApiController : ControllerBase
    {
        static TestDict testDict = new();

        [HttpGet("GetAllKeys")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        public IEnumerable<string> GetAllItems() => testDict.GetAllKeys();

        [HttpPost("AddItem")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public string AddItem(string key, string value) => testDict.Add(key, value);

        [HttpDelete("DeleteValue")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public string DeleteValue(string key) => testDict.DeleteValue(key);

        [HttpGet("GetValue")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public string GetValue(string key) => testDict.GetValue(key);
    }
}
