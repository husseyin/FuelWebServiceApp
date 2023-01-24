using Business.Abstract;
using Core.Utilities.Results.DataResult;
using Core.Utilities.Results.OperationResult;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VehicleRogSaleTransManager : IVehicleRogSaleTransService
    {
        public IResult AddVehicleRogSaleTrans(string startDate, string endDate)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<VehicleRogSaleTransModel>> GetVehicleRogSaleTrans(string startDate, string endDate)
        {
            throw new NotImplementedException();
        }
    }
}
