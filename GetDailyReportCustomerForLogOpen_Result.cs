//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PGMKTStock
{
    using System;
    
    public partial class GetDailyReportCustomerForLogOpen_Result
    {
        public long LogOpenOfCusId { get; set; }
        public Nullable<long> CustomerId { get; set; }
        public Nullable<System.DateTime> TradeDate { get; set; }
        public string Instrument { get; set; }
        public Nullable<decimal> MKTValue { get; set; }
        public Nullable<int> LVolume { get; set; }
        public Nullable<decimal> LPrice { get; set; }
        public Nullable<decimal> LAvgPrice { get; set; }
        public Nullable<decimal> LInterestCalSum { get; set; }
        public Nullable<decimal> LMarketPrice { get; set; }
        public Nullable<decimal> LUnrealized { get; set; }
        public Nullable<int> SVolume { get; set; }
        public Nullable<decimal> SPrice { get; set; }
        public Nullable<decimal> SAvgPrice { get; set; }
        public Nullable<decimal> SInterestCalSum { get; set; }
        public Nullable<decimal> SMarketPrice { get; set; }
        public Nullable<decimal> SUnrealized { get; set; }
        public Nullable<double> MinDay { get; set; }
        public Nullable<double> InterestCalPercent { get; set; }
    }
}