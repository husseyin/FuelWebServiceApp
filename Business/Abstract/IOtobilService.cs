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
    public interface IOtobilService
    {
        IDataResult<string> OtobilLogin();
        IDataResult<List<OtobilSaleModel>> GetOtobilSales(string fromDate, string toDate);
        IResult AddOtobilSales(string fromDate, string toDate);
    }
}
