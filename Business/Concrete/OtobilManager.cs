using Business.Abstract;
using Business.Constans.Messages;
using Core.Utilities.Busines;
using Core.Utilities.Results.DataResult;
using Core.Utilities.Results.OperationResult;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using OtobilService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Business.Concrete
{
    public class OtobilManager : IOtobilService
    {
        ReportsSoapClient _client = new ReportsSoapClient(ReportsSoapClient.EndpointConfiguration.ReportsSoap);
        public IConfiguration Configuration { get; }
        private OtobilLoginOptions _otobilLoginOptions;
        private OtobilFleetOptions _otobilFleetOptions;
        IOtobilSaleDal _otobilSaleDal;
        public OtobilManager(IConfiguration configuration, IOtobilSaleDal otobilSaleDal)
        {
            Configuration = configuration;
            _otobilLoginOptions = Configuration.GetSection("OtobilLoginOptions").Get<OtobilLoginOptions>();
            _otobilFleetOptions = Configuration.GetSection("OtobilFleetOptions").Get<OtobilFleetOptions>();
            _otobilSaleDal = otobilSaleDal;
        }

        public IResult AddOtobilSales(string fromDate, string toDate)
        {
            var getOtobilSales = GetOtobilSales(fromDate, toDate);

            if (!getOtobilSales.Success)
                return new ErrorResult(getOtobilSales.Message);
            
            foreach (var sale in getOtobilSales.Data)
            {
                var helper = TryCatchHelper.RunTheMethod(() => _otobilSaleDal.Add(sale));
                if (!helper.Success)
                    return new ErrorResult(ErrorMessage.OtobilSalesAddErorr);
            }

            return new SuccessResult(SuccessMessage.OtobilSalesAdded);
        }

        public IDataResult<List<OtobilSaleModel>> GetOtobilSales(string fromDate, string toDate)
        {
            DateTime _fromDate = DateTime.Parse(fromDate);
            DateTime _toDate = DateTime.Parse(toDate);
            var _otobilLogin = OtobilLogin();

            if (!_otobilLogin.Success)
                return new ErrorDataResult<List<OtobilSaleModel>>(_otobilLogin.Message);

            SalesRequest salesRequest = new SalesRequest
            {
                fleetCode = _otobilFleetOptions.FleetCode,
                fromDate = _fromDate,
                toDate = _toDate,
                tokenKey = _otobilLogin.Data
            };

            var salesResult = _client.Sales(salesRequest).SalesResult.Nodes[1];

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(salesResult.CreateReader());

            XmlNodeList items = xmlDocument.GetElementsByTagName("Table");
            if (items == null)
                return new ErrorDataResult<List<OtobilSaleModel>>(ErrorMessage.OtobilSaleListError);

            List<OtobilSaleModel> otobilSales = new List<OtobilSaleModel>();
            foreach (XmlNode item in items)
            {
                otobilSales.Add(new OtobilSaleModel
                {
                    CityID = int.Parse(item["CityID"].InnerText),
                    CityName = item["CityName"].InnerText,
                    ECRReceiptNr = int.Parse(item["ECRReceiptNr"].InnerText),
                    FleetID = int.Parse(item["FleetID"].InnerText),
                    FleetName = item["FleetName"].InnerText,
                    GroupID = int.Parse(item["GroupID"].InnerText),
                    GroupName = item["GroupName"].InnerText,
                    InvoicePeriodNr = item["InvoicePeriodNr"].InnerText,
                    LicensePlateNr = item["LicensePlateNr"].InnerText,
                    Odometer = int.Parse(item["Odometer"].InnerText),
                    ProcessTime = DateTime.Parse(item["ProcessTime"].InnerText),
                    ProductID = int.Parse(item["ProductID"].InnerText),
                    ProductName = item["ProductName"].InnerText,
                    SaleEnd = DateTime.Parse(item["SaleEnd"].InnerText),
                    StationID = int.Parse(item["StationID"].InnerText),
                    StationName = item["StationName"].InnerText,
                    Total = decimal.Parse(item["Total"].InnerText.Replace('.', ',')),
                    UnitPrice = decimal.Parse(item["UnitPrice"].InnerText.Replace('.', ',')),
                    Volume = decimal.Parse(item["Volume"].InnerText.Replace('.', ',')),
                });
            }

            return new SuccessDataResult<List<OtobilSaleModel>>(otobilSales, SuccessMessage.OtobilSalesListed);
        }

        public IDataResult<string> OtobilLogin()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                userName = _otobilLoginOptions.UserName,
                password = _otobilLoginOptions.Password
            };

            string _tokenKey = _client.Login(loginRequest).LoginResult;

            if (_tokenKey == "Hata : Kullanıcı bulunamadı." || _tokenKey == "")
                return new ErrorDataResult<string>(ErrorMessage.OtobilLoginError);

            return new SuccessDataResult<string>(_tokenKey, SuccessMessage.OtobilLoggedIn);
        }
    }
}
