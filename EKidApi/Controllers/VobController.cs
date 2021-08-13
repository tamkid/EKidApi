using EKidApi.RequestData.Vob;
using EKidApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EKidApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VobController : ControllerBase
    {
        private readonly IVobService _service;

        public VobController(IVobService service) 
        {
            int a = 6;
            _service = service;
        }

        // GET: api/<VobController>
        [HttpGet]
        public async Task<IActionResult> Get(int _limit = 10, int _page = 0, string _search = "")
        {
            var response = await _service.GetAll(_limit, _page, _search);
            return Ok(response);
        }

        // GET api/<VobController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Guid idGuid;
            bool isParse = Guid.TryParse(id, out idGuid);
            if (!isParse)
            {
                ModelState.AddModelError("Error", "Id field is not valid");
                return BadRequest(ModelState);
            }

            var response = await _service.GetById(idGuid);
            return Ok(response);
        }

        // POST api/<VobController>
        [HttpPost("add-new")]
        public async Task<IActionResult> AddNew([FromBody] Request_Vob_Add request)
        {
            var response = await _service.AddNew(request);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Request_Vob_Update request)
        {
            if (request.Id == Guid.Empty)
            {
                ModelState.AddModelError("Error", "Id field is not valid");
                return BadRequest(ModelState);
            }
            var response = await _service.Update(request);
            return Ok(response);
        }

        // DELETE api/<VobController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Guid idGuid;
            bool isParse = Guid.TryParse(id, out idGuid);
            if (!isParse)
            {
                ModelState.AddModelError("Error", "Id field is not valid");
                return BadRequest(ModelState);
            }

            var response = await _service.Delete(idGuid);
            return Ok(response);
        }
    }
}
