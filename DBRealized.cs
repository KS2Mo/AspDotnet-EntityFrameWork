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
    
    public partial class DBRealized
    {
        public long RealizedId { get; set; }
        public Nullable<long> CustomerId { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PriceClose { get; set; }
        public Nullable<decimal> PriceAvgCosts { get; set; }
        public Nullable<decimal> PriceCosts { get; set; }
        public Nullable<decimal> Interest { get; set; }
        public Nullable<int> Voloum { get; set; }
        public Nullable<double> MinDay { get; set; }
        public Nullable<double> SumDay { get; set; }
        public Nullable<double> InterestCalPercent { get; set; }
        public string TradeOpenId { get; set; }
        public string TradeCloseId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
