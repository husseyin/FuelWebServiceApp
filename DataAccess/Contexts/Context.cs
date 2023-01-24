using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Contexts
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HUSEYIN\SQLEXPRESS;Database=FuelWebServiceApp;Trusted_Connection=true");
        }
        
        public DbSet<OtobilSaleModel> OtobilSales { get; set; }
        public DbSet<FuelCardFirmTxnModel> FuelCardFirmTxns { get; set; }
        public DbSet<VehicleRogSaleTransModel> VehicleRogSalesTrans { get; set; }

    }
}
