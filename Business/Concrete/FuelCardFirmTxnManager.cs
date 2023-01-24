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
        private FuelCardSettings _fuelCardSettings;
        IFuelCardFirmTxnDal _fuelCardFirmTxnDal;
        public FuelCardFirmTxnManager(IConfiguration configuration, IFuelCardFirmTxnDal fuelCardFirmTxnDal)
        {
            Configuration = configuration;
            _fuelCardSettings = Configuration.GetSection("FuelCardSettings").Get<FuelCardSettings>();
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
            DateTime _startDate = DateTime.Parse(startDate);
            DateTime _endDate = DateTime.Parse(endDate);

            FirmTxnDetail1Request txnRequest = new FirmTxnDetail1Request
            {
                Body = new FirmTxnDetail1RequestBody
                {
                    input = new FirmTxnDetail1In
                    {
                        CardNo = "",
                        CustomerName = "",
                        EndDate = _endDate.ToString(),
                        FirmNo = _fuelCardSettings.FirmNo,
                        MerchantNo = _fuelCardSettings.MerchantNo,
                        Password = _fuelCardSettings.Password,
                        StartDate = _startDate.ToString(),
                        UserName = _fuelCardSettings.UserName,
                        UserSubCode = _fuelCardSettings.UserSubCode
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
                    Volume = txnDetail.Volume,
                    UnitPrice = txnDetail.Amount / txnDetail.Volume
                });
            }

            return new SuccessDataResult<List<FuelCardFirmTxnModel>>(fuelTxns, SuccessMessage.FuelCardFirmTxnListed);
        }
    }
}
