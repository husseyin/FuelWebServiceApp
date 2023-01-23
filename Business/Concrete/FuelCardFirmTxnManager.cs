using Business.Abstract;
using Core.Utilities.Results.DataResult;
using Core.Utilities.Results.OperationResult;
using DataAccess.Abstract;
using Entities.Concrete;
using FuelCardService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FuelCardFirmTxnManager : IFuelCardFirmTxnService
    {
        ServiceSoapClient _client = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);

        IFuelCardFirmTxnDal _fuelCardFirmTxnDal;
        public FuelCardFirmTxnManager(IFuelCardFirmTxnDal fuelCardFirmTxnDal)
        {
            _fuelCardFirmTxnDal = fuelCardFirmTxnDal;
        }

        public IResult AddFuelCardFirmTxns(string startDate, string endDate)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<FuelCardFirmTxnModel>> GetFuelCardFirmTxns(string startDate, string endDate)
        {
            FirmTxnDetail1Request request = new FirmTxnDetail1Request
            {
                Body = new FirmTxnDetail1RequestBody
                {
                    input = new FirmTxnDetail1In
                    {
                        CardNo = "",
                        CustomerName = "",
                        EndDate = endDate,
                        FirmNo = 0,
                        MerchantNo = 0,
                        Password = "",
                        StartDate = startDate,
                        UserName = "",
                        UserSubCode = 0
                    }
                }
            };

            var result = _client.FirmTxnDetail1(request).Body.FirmTxnDetail1Result;

            return null;
        }
    }
}
