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
    using System.Collections.Generic;
    
    public partial class DBTradeOpen
    {
        public long TradeOpenId { get; set; }
        public Nullable<long> CustomerId { get; set; }
        public Nullable<System.DateTime> TradeDate { get; set; }
        public Nullable<System.DateTime> BusinessDate { get; set; }
        public string BuySell { get; set; }
        public string OpenClose { get; set; }
        public string InstrumentHead { get; set; }
        public string Instrument { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<int> SumVolume { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> CalPrice { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public string TradeOpenIdRef { get; set; }
        public string TradeCloseIdRef { get; set; }
        public string TradeOpenStatusId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<long> CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateBy { get; set; }
        public Nullable<double> MinDay { get; set; }
        public Nullable<double> InterestCalPercent { get; set; }
        public Nullable<decimal> InterestCalSum { get; set; }
        public string TradeOpenTypeId { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public Nullable<double> SumDay { get; set; }
        public string EditStatus { get; set; }
    }
}