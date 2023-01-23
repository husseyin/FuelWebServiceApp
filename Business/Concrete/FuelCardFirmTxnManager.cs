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
        public IConfiguration Configuration { get; }
        private FuelCardLoginOptions _fuelCardLoginOptions;
        private FuelCardFirmTxnOptions _fuelCardFirmTxnOptions;
        IFuelCardFirmTxnDal _fuelCardFirmTxnDal;
        public FuelCardFirmTxnManager(IConfiguration configuration, IFuelCardFirmTxnDal fuelCardFirmTxnDal)
        {
            Configuration = configuration;
            _fuelCardLoginOptions = Configuration.GetSection("FuelCardLoginOptions").Get<FuelCardLoginOptions>();
            _fuelCardFirmTxnOptions = Configuration.GetSection("FuelCardFirmTxnOptions").Get<FuelCardFirmTxnOptions>();
            _fuelCardFirmTxnDal = fuelCardFirmTxnDal;
        }

        public IResult AddFuelCardFirmTxns(string startDate, string endDate)
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Body = new LoginRequestBody
                {
                    input = new LoginIn
                    {
                        Password = _fuelCardLoginOptions.Password,
                        UserName = _fuelCardLoginOptions.UserName,
                        UserSubCode = _fuelCardFirmTxnOptions.UserSubCode
                    }
                }
            };

            var result = _client.Login(loginRequest);

            return null;
        }

        public IDataResult<List<FuelCardFirmTxnModel>> GetFuelCardFirmTxns(string startDate, string endDate)
        {
            FirmTxnDetail1Request txnRequest = new FirmTxnDetail1Request
            {
                Body = new FirmTxnDetail1RequestBody
                {
                    input = new FirmTxnDetail1In
                    {
                        CardNo = "",
                        CustomerName = "",
                        EndDate = endDate,
                        FirmNo = _fuelCardFirmTxnOptions.FirmNo,
                        MerchantNo = _fuelCardFirmTxnOptions.MerchantNo,
                        Password = _fuelCardLoginOptions.Password,
                        StartDate = startDate,
                        UserName = _fuelCardLoginOptions.UserName,
                        UserSubCode = _fuelCardFirmTxnOptions.UserSubCode
                    }
                }
            };

            var result = _client.FirmTxnDetail1(txnRequest).Body.FirmTxnDetail1Result;

            return null;
        }
    }
}
