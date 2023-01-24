using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constans.Messages
{
    public static class SuccessMessage
    {
        // OTOBIL LOGIN
        public static string OtobilLoggedIn = "Token alındı.";

        // OTOBIL SALE
        public static string OtobilSalesListed = "Otobil satışları listelendi.";
        public static string OtobilSalesAdded = "Otobil satışları eklendi.";

        // FUELCARD FIRMTXN
        public static string FuelCardFirmTxnListed = "Yakıt kart detayları listelendi.";
        public static string FuelCardFirmTxnAdded = "Yakıt kart detayları eklendi.";

        // VEHICLEROG SALETRANS
        public static string VehicleRogSaleTransListed = "Taşıt tanıma işlemleri listelendi.";
        public static string VehicleRogSaleTransAdded = "Taşıt tanıma işlemleri eklendi.";
    }
}
