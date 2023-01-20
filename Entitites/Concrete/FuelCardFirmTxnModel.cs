using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class FuelCardFirmTxnModel : IEntity
    {
        public int Id { get; set; }
        public int TrxID { get; set; }
        public DateTime Date { get; set; }
        public string MerchantName { get; set; }
        public string CityName { get; set; }
        public string CardNo { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountBayii { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Volume { get; set; }
        public int PumpNo { get; set; }
        public string ProductName { get; set; }
        public string PlateNo { get; set; }

    }
}
