using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtobilController : ControllerBase
    {
        IOtobilService _otobilService;
        public OtobilController(IOtobilService otobilService)
        {
            _otobilService = otobilService;
        }

        [HttpGet("otobillogin")]
        public IActionResult OtobilLogin()
        {
            var result = _otobilService.OtobilLogin();
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getotobilsales")]
        public IActionResult GetOtobilSales(string fromDate, string toDate)
        {
            var result = _otobilService.GetOtobilSales(fromDate, toDate);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("addotobilsales")]
        public IActionResult AddOtobilSales(string fromDate, string toDate)
        {
            var result = _otobilService.AddOtobilSales(fromDate, toDate);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
