using Business.Abstract;
using Business.Constans.Messages;
using Core.Utilities.Busines;
using Core.Utilities.Results.DataResult;
using Core.Utilities.Results.OperationResult;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRecogService;

namespace Business.Concrete
{
    public class VehicleRogSaleTransManager : IVehicleRogSaleTransService
    {
        TTSWebServicesSoapClient client = new TTSWebServicesSoapClient(TTSWebServicesSoapClient.EndpointConfiguration.TTSWebServicesSoap);
        IVehicleRogSaleTransDal _vehicleRogSaleTransDal;
        public VehicleRogSaleTransManager(IVehicleRogSaleTransDal vehicleRogSaleTransDal)
        {
            _vehicleRogSaleTransDal = vehicleRogSaleTransDal;
        }

        public IResult AddVehicleRogSaleTrans(string startDate, string endDate)
        {
            var getVehicleRogSaleTrans = GetVehicleRogSaleTrans(startDate, endDate);

            if (!getVehicleRogSaleTrans.Success)
                return new ErrorResult(getVehicleRogSaleTrans.Message);

            foreach (var tran in getVehicleRogSaleTrans.Data)
            {
                var helper = TryCatchHelper.RunTheMethod(() => _vehicleRogSaleTransDal.Add(tran));
                if (!helper.Success)
                    return new ErrorResult(ErrorMessage.VehicleRogSaleTransAddErorr);
            }

            return new SuccessResult(SuccessMessage.VehicleRogSaleTransAdded);
        }

        public IDataResult<List<VehicleRogSaleTransModel>> GetVehicleRogSaleTrans(string startDate, string endDate)
        {
            DateTime _startDate = DateTime.Parse(startDate);
            DateTime _endDate = DateTime.Parse(endDate);

            GetCustomerSalesTransactionRequest requestSale = new GetCustomerSalesTransactionRequest()
            {
                branch_code = "",
                customer_reference = "",
                cust_code = "",
                department_code = "",
                invoice_number = "",
                password = "",
                plate_code = "",
                report_end_dt = _endDate,
                report_start_dt = _startDate,
                user_id = ""
            };

            var result = client.GetCustomerSalesTransaction(requestSale).GetCustomerSalesTransactionResult;
            if (!result.PROCESSRESULT.Success)
                return new ErrorDataResult<List<VehicleRogSaleTransModel>>(ErrorMessage.VehicleRogSaleTransListError);

            List<VehicleRogSaleTransModel> saleTrans = new List<VehicleRogSaleTransModel>();
            foreach (var sale in result.LISTOFSALESTRANSACTION)
            {
                saleTrans.Add(new VehicleRogSaleTransModel
                {
                    CardNo = sale.Card_no,
                    CustomerCode = sale.Customer_code,
                    CustomerName = sale.Customer_name,
                    CustomerReference = sale.Customer_Reference,
                    DepartmentCode = sale.Department_code,
                    DepartmentName = sale.Department_name,
                    FuelName = sale.Fuel_name,
                    GroupCode = sale.Group_code,
                    InvoiceNo = sale.Invoice_no,
                    PlateCd = sale.Plate_cd,
                    RetailOutletCode = sale.Retail_outlet_code,
                    RetailOutletName = sale.Retail_outlet_name,
                    RtlOtltProvince = sale.Rtl_otlt_province,
                    SalesTotalAmount = sale.Sales_total_amount,
                    SalesType = sale.Sales_type,
                    ShellReference = sale.Shell_Reference,
                    TransactionDate = sale.Transaction_date,
                    UnitPrice = sale.Unit_price,
                    VehicleKm = sale.Vehicle_km,
                    VIuType = sale.Viu_type,
                    Volume = sale.Volume
                });
            }

            return new SuccessDataResult<List<VehicleRogSaleTransModel>>(saleTrans, SuccessMessage.VehicleRogSaleTransListed);
        }
    }
}
