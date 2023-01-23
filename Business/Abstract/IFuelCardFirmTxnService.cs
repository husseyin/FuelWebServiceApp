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
    public interface IFuelCardFirmTxnService
    {
        IDataResult<List<FuelCardFirmTxnModel>> GetFuelCardFirmTxns(string startDate, string endDate);
        IResult AddFuelCardFirmTxns(string startDate, string endDate);
    }
}
