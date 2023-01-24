using Core.Utilities.Results.DataResult;
using Core.Utilities.Results.OperationResult;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVehicleRogSaleTransService
    {
        IDataResult<List<VehicleRogSaleTransModel>> GetVehicleRogSaleTrans(string startDate, string endDate);
        IResult AddVehicleRogSaleTrans(string startDate, string endDate);
    }
}
