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
    public class VehicleRogController : ControllerBase
    {
        IVehicleRogSaleTransService _vehicleRogSaleTransService;

        public VehicleRogController(IVehicleRogSaleTransService vehicleRogSaleTransService)
        {
            _vehicleRogSaleTransService = vehicleRogSaleTransService;
        }

        [HttpGet("getvehiclerogsaletrans")]
        public IActionResult GetVehicleRogSaleTrans(string startDate, string endDate)
        {
            var result = _vehicleRogSaleTransService.GetVehicleRogSaleTrans(startDate, endDate);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("addvehiclerogsaletrans")]
        public IActionResult AddVehicleRogSaleTrans(string startDate, string endDate)
        {
            var result = _vehicleRogSaleTransService.AddVehicleRogSaleTrans(startDate, endDate);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
