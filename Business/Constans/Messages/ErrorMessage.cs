﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constans.Messages
{
    public static class ErrorMessage
    {
        // OTOBIL LOGIN
        public static string OtobilLoginError = "Token alma hatası!";

        // OTOBIL SALE
        public static string OtobilSaleListError = "Satış listeleme hatası!";
        public static string OtobilSalesAddErorr = "Otobil satış ekleme hatası!";

        // FUELCARD FIRMTXN
        public static string FuelCardFirmTxnListError = "Yakıt kart detay listeleme hatası!";
        public static string FuelCardFirmTxnAddErorr = "Yakıt kart detay ekleme hatası!";

        // VEHICLEROG SALETRANS
        public static string VehicleRogSaleTransListError = "Taşıt tanıma işlem listeleme hatası!";
        public static string VehicleRogSaleTransAddErorr = "Taşıt tanıma işlem ekleme hatası!";
    }
}
