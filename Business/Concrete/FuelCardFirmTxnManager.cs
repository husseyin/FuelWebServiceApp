using Business.Abstract;
using Business.Constans.Messages;
using Core.Utilities.Busines;
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
            var getFuelCardFirmTxns = GetFuelCardFirmTxns(startDate, endDate);

            if (!getFuelCardFirmTxns.Success)
                return new ErrorResult(getFuelCardFirmTxns.Message);

            foreach (var txn in getFuelCardFirmTxns.Data)
            {
                var helper = TryCatchHelper.RunTheMethod(() => _fuelCardFirmTxnDal.Add(txn));
                if (!helper.Success)
                    return new ErrorResult(ErrorMessage.OtobilSalesAddErorr);
            }

            return new SuccessResult(SuccessMessage.FuelCardFirmTxnAdded);
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
            if (result.ResultCode != 0)
                return new ErrorDataResult<List<FuelCardFirmTxnModel>>(ErrorMessage.FuelCardFirmTxnListError);

            List<FuelCardFirmTxnModel> fuelTxns = new List<FuelCardFirmTxnModel>();
            foreach (var txnDetail in result.TxnDetails)
            {
                fuelTxns.Add(new FuelCardFirmTxnModel
                {
                    Amount = txnDetail.Amount,
                    CardNo = txnDetail.CardNo,
                    CityName = txnDetail.CityName,
                    CustomerName = txnDetail.CustomerName,
                    Date = DateTime.Parse(txnDetail.Date),
                    DiscountBayii = txnDetail.DiscountBayii,
                    DiscountTotal = txnDetail.DiscountTotal,
                    MerchantName = txnDetail.MerchantName,
                    PlateNo = txnDetail.PlateNo,
                    ProductName = txnDetail.ProductName,
                    PumpNo = int.Parse(txnDetail.PumpNo),
                    TrxID = txnDetail.TrxID,
                    Volume = txnDetail.Volume
                });
            }

            return new SuccessDataResult<List<FuelCardFirmTxnModel>>(fuelTxns, SuccessMessage.FuelCardFirmTxnListed);
        }
    }
}
