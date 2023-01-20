using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OtobilSaleModel : IEntity
    {
        public int Id { get; set; }
        public DateTime SaleEnd { get; set; }
        public DateTime ProcessTime { get; set; }
        public int StationID { get; set; }
        public string StationName { get; set; }
        public int FleetID { get; set; }
        public string FleetName { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string LicensePlateNr { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Total { get; set; }
        public decimal Volume { get; set; }
        public decimal UnitPrice { get; set; }
        public int Odometer { get; set; }
        public int ECRReceiptNr { get; set; }
        public string InvoicePeriodNr { get; set; }
        public string CityName { get; set; }
        public int CityID { get; set; }
    }
}
