﻿using Business.Abstract;
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
    public class FuelCardFirmTxnController : ControllerBase
    {
        IFuelCardFirmTxnService _fuelCardFirmTxnService;

        public FuelCardFirmTxnController(IFuelCardFirmTxnService fuelCardFirmTxnService)
        {
            _fuelCardFirmTxnService = fuelCardFirmTxnService;
        }

        [HttpGet("getfuelcardfirmtxns")]
        public IActionResult GetFuelCardFirmTxns(string startDate, string endDate)
        {
            var result = _fuelCardFirmTxnService.GetFuelCardFirmTxns(startDate, endDate);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("addfuelcardfirmtxns")]
        public IActionResult AddFuelCardFirmTxns(string startDate, string endDate)
        {
            var result = _fuelCardFirmTxnService.AddFuelCardFirmTxns(startDate, endDate);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
