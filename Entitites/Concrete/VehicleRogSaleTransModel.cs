using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class VehicleRogSaleTransModel
    {
        public int Id { get; set; }
        public string SalesType { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string CardNo { get; set; }
        public string PlateCd { get; set; }
        public string VIuType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string RetailOutletCode { get; set; }
        public string RetailOutletName { get; set; }
        public string RtlOtltProvince { get; set; }
        public string FuelName { get; set; }
        public string GroupCode { get; set; }
        public decimal VehicleKm { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal SalesTotalAmount { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerReference { get; set; }
        public decimal ShellReference { get; set; }
    }
}
