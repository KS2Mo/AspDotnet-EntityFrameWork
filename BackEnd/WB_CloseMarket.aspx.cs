using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PGMKTStock.BackEnd
{
    public partial class WB_CloseMarket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            DBStockMarketEntities db = new DBStockMarketEntities();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime dtTrading = Convert.ToDateTime(txtTradeDate.Text);

            #region เพิ่มข้อมูล รายการที่ เปิดสัญญาอยู่
            List<DBTradeOpen> tbTO = db.DBTradeOpens.Where(t => t.BusinessDate == dtTrading && t.TradeOpenStatusId=="A" && t.OpenClose=="O").ToList();

            List<DBMarketPrice> tbMP = db.DBMarketPrices.Where(t => t.TradeDate == dtTrading).ToList();

            List<DBLogSummaryOfCustomer> tbSum =new List<DBLogSummaryOfCustomer>();
            DateTime dtDate = Convert.ToDateTime(txtTradeDate.Text);

            var GroupCustomerList = tbTO.GroupBy(u => u.CustomerId).Select(group => new { CustomerId = group.Key }).ToList();


            var GroupInstrumentList = tbTO.GroupBy(u => new { u.Instrument, u.InstrumentHead }).Select(group => new { Instrument = group.Key.Instrument,InstrumentHead=group.Key.InstrumentHead }).ToList();

            foreach (var tbCustomer in GroupCustomerList)
            {
                foreach (var tbTrade in GroupInstrumentList)
                {
                    var tbDataByCustomer = tbTO.Where(t => t.CustomerId == tbCustomer.CustomerId && t.Instrument == tbTrade.Instrument).ToList();
                    string Instrument = tbTrade.Instrument;
                    DBMarketPrice DMP = tbMP.Where(t => t.Instrument == Instrument).FirstOrDefault();

                    var InstrumentB = tbDataByCustomer.Where(t => t.BuySell == "B").ToList();
                    DBTradeOpen InstrumentBAvg = tbDataByCustomer.Where(t => t.BuySell == "B").OrderByDescending(t => t.TradeOpenId).FirstOrDefault();


                    DBLogSummaryOfCustomer LogSum = new DBLogSummaryOfCustomer();
                    decimal LPriceCostSum = 0, LInterestCalSum = 0, LUnrealized = 0;
                    int LVolume = 0;
                    if (InstrumentB.Count > 0)
                    {
                        foreach (var IB in InstrumentB)
                        {
                            LogSum.CustomerId = IB.CustomerId;
                            LogSum.MinDay = IB.MinDay;
                            LogSum.InterestCalPercent = IB.InterestCalPercent;

                            LVolume += IB.Volume.Value;
                            LPriceCostSum += IB.Volume.Value * IB.Price.Value;

                            TimeSpan SumDays = (dtDate - IB.TradeDate.Value);
                            double DaySum = 0;
                            if (SumDays.Days < IB.MinDay)
                                DaySum = IB.MinDay.Value;
                            else
                                DaySum = SumDays.Days;
                            //คำนวน UUnrealized
                            decimal PriceAVG = InstrumentBAvg.AvgPrice.Value * IB.Volume.Value;
                            decimal PriceMKTValue = DMP.MarketPrice.Value * IB.Volume.Value;
                            LUnrealized += PriceMKTValue - PriceAVG;

                            //คำนวน ดอกเบี้ย
                            decimal PriceCost = IB.Price.Value * IB.Volume.Value;
                            decimal CalPercen = Convert.ToDecimal(IB.InterestCalPercent.Value) / 100;
                            decimal CalDaySum = Convert.ToDecimal(DaySum) / 365;
                            LInterestCalSum += PriceCost * CalPercen * CalDaySum;
                        }
                        LogSum.TradeDate = dtDate;
                        LogSum.Instrument = tbTrade.Instrument;
                        LogSum.InstrumentHead = tbTrade.InstrumentHead;
                        LogSum.LMarketPrice = DMP.MarketPrice.Value;
                        LogSum.LUnrealized = LUnrealized;
                        LogSum.LVolume = LVolume;
                        LogSum.LInterestCalSum = LInterestCalSum;
                        LogSum.LPrice = LPriceCostSum / LogSum.LVolume;
                        LogSum.LAvgPrice = InstrumentBAvg.AvgPrice.Value;
                    }


                    var InstrumentS = tbDataByCustomer.Where(t => t.BuySell == "S").ToList();
                    if (InstrumentS.Count > 0)
                    {
                        DBTradeOpen InstrumentSAvg = tbDataByCustomer.Where(t => t.BuySell == "S").OrderByDescending(t => t.TradeOpenId).FirstOrDefault();
                        decimal SPriceCostSum = 0, SInterestCalSum = 0, SUnrealized = 0;
                        int SVolume = 0;
                        foreach (var IB in InstrumentS)
                        {
                            LogSum.CustomerId = IB.CustomerId;
                            LogSum.MinDay = IB.MinDay;
                            LogSum.InterestCalPercent = IB.InterestCalPercent;
                            SVolume += IB.Volume.Value;
                            SPriceCostSum += IB.Volume.Value * IB.Price.Value;

                            TimeSpan SumDays = (dtDate - IB.TradeDate.Value);
                            double DaySum = 0;
                            if (SumDays.Days < IB.MinDay)
                                DaySum = IB.MinDay.Value;
                            else
                                DaySum = SumDays.Days;

                            //คำนวน UUnrealized
                            decimal PriceAVG = InstrumentSAvg.AvgPrice.Value * IB.Volume.Value;
                            decimal PriceMKTValue = DMP.MarketPrice.Value * IB.Volume.Value;
                            SUnrealized += PriceMKTValue - PriceAVG;
                            //คำนวน ดอกเบี้ย
                            decimal PriceCost = IB.Price.Value * IB.Volume.Value;
                            decimal CalPercen = Convert.ToDecimal(IB.InterestCalPercent.Value) / 100;
                            decimal CalDaySum = Convert.ToDecimal(DaySum) / 365;
                            SInterestCalSum += PriceCost * CalPercen * CalDaySum;
                        }

                        LogSum.TradeDate = dtDate;
                        LogSum.Instrument = tbTrade.Instrument;
                        LogSum.InstrumentHead = tbTrade.InstrumentHead;
                        LogSum.MKTValue = DMP.MarketPrice.Value;
                        LogSum.SMarketPrice = DMP.MarketPrice.Value;
                        LogSum.SUnrealized = SUnrealized;
                        LogSum.SVolume = SVolume;
                        LogSum.SInterestCalSum = SInterestCalSum;
                        LogSum.SPrice = SPriceCostSum / SVolume;
                        LogSum.SAvgPrice = InstrumentSAvg.AvgPrice.Value;
                    }
                    db.DBLogSummaryOfCustomers.Add(LogSum);
                    db.SaveChanges();

                }
            }
            #endregion

            #region เพิ่ม Header Report 
            List<DBCustomer> tbCus = db.DBCustomers.ToList();
            foreach (var tbCustomer in tbCus)
            {
                DBLogPriceOfCustomer LogHeader = new DBLogPriceOfCustomer();
                LogHeader.LogPriceDate = Convert.ToDateTime(txtTradeDate.Text);
                decimal CashInAccount = 0, CreditLimit = 0, LineAvailable = 0, SumCostByOpen = 0, SumMKTPrice = 0, Unreallzed = 0, Realized = 0, ExcessEquity = 0, EquityBalance = 0, InitialMargin = 0, MaintenanceMargin = 0, CallMargin = 0;
                string StatusCustomer = "";

                //ตรวจสอบเงินฝากหลักประกันของลูกค้า
                List<DBDeposit> tbDep = db.DBDeposits.Where(t => t.CustomerId == tbCustomer.CustomerId).ToList();
                if (tbDep != null)
                {
                    foreach (var Dep in tbDep)
                    {
                        if (Dep.DepositTypeId == 1)
                            CashInAccount += Dep.DepositPrice.Value;
                        else if (Dep.DepositTypeId == 2)
                            CashInAccount -= Dep.DepositPrice.Value;
                    }
                }
                CreditLimit=CashInAccount*5;
                LogHeader.CustomerId = tbCustomer.CustomerId;
                LogHeader.CashInAccount = CashInAccount;
                LogHeader.CreditLimit = CreditLimit;
                //ตรวจสอบสำหรับการคำนวนหาข้อมูลโดยสรุปของลูกค้า
                List<DBTradeOpen> tbOTrades = db.DBTradeOpens.Where(t => t.CustomerId == tbCustomer.CustomerId && t.TradeOpenStatusId == "A").ToList();
                if (tbOTrades != null)
                {
                    foreach (var tbOTrade in tbOTrades)
                    {
                        //ผลรวมต้นทุนของรายการที่มีสถานะ Open
                        SumCostByOpen += tbOTrade.AvgPrice.Value * tbOTrade.Volume.Value;

                        //ผลรวม MKT Price ของรายการที่มีสถานะ Open
                        DBMarketPrice MP = db.DBMarketPrices.Where(t => t.Instrument == tbOTrade.Instrument).OrderByDescending(t => t.CreateDate).FirstOrDefault();
                        SumMKTPrice += MP == null ? 0 : MP.MarketPrice.Value * tbOTrade.Volume.Value;
                    }
                    //ประมาณกำไร/ขาดทุน โดยคำนวนจาก ผลรวมราคาตลาด ณ เวลาใดเวลาหนึ่ง-ผลรวมต้นทุน
                    if (SumMKTPrice > 0)
                        Unreallzed = SumMKTPrice - SumCostByOpen;
                }
                LogHeader.CostValue = SumCostByOpen;
                LogHeader.MKTValue = SumMKTPrice;
                LogHeader.Unrealized = Unreallzed;
                //กำไร/ขาดทุน โดยคำนวนจาก ผลรวมราคาตลาด ณ เวลาที่มีการ Close-ผลรวมต้นทุน
                List<DBRealized> tbRealized  = db.DBRealizeds.Where(t => t.CustomerId == tbCustomer.CustomerId).ToList();
                if (tbRealized != null)
                {
                    foreach (var SumRealized in tbRealized)
                    {
                        Realized += SumRealized.Price.Value;
                    }
                }
                LogHeader.Realized = Realized;

                //List<DBTradeClose> tbCTrades = db.DBTradeCloses.Where(t => t.CustomerId == tbCustomer.CustomerId).ToList();
                //if (tbCTrades != null)
                //{
                //    foreach (var tbCTrade in tbCTrades)
                //    {
                //        Realized += (tbCTrade.ClosePrice.Value * tbCTrade.CloseVolume.Value) - (tbCTrade.OpenAvgPrice.Value * tbCTrade.CloseVolume.Value);
                //    }
                //}
                InitialMargin = SumCostByOpen / 5;
                LogHeader.InitialMargin = InitialMargin;

                LineAvailable = ((CashInAccount + Realized) * 5) - InitialMargin;
                LogHeader.LineAvailable = LineAvailable;

                ExcessEquity = CashInAccount + Realized - InitialMargin;
                LogHeader.ExcessEquity = ExcessEquity;

                EquityBalance = CashInAccount + Unreallzed + Realized;
                LogHeader.EquityBalance = EquityBalance;

                MaintenanceMargin = InitialMargin - ((InitialMargin * 60) / 100);
                LogHeader.MaintenanceMargin = MaintenanceMargin;

                if (EquityBalance >= MaintenanceMargin)
                {
                    StatusCustomer = "No";
                }
                else if (EquityBalance < MaintenanceMargin)
                {
                    StatusCustomer = "Call";
                }
                LogHeader.CallMargin = StatusCustomer;

                db.DBLogPriceOfCustomers.Add(LogHeader);
                db.SaveChanges();
            }
            #endregion

            #region Start Order Daily 
            DateTime dtOpen = Convert.ToDateTime(txtTradeDate.Text);
            List<DBTradeOpen> tbOTradesNewDailyTotal = db.DBTradeOpens.Where(t => t.BusinessDate == dtOpen).ToList();
            foreach (var Order in tbOTradesNewDailyTotal)
            {
                if (Order.TradeDate == Order.BusinessDate)
                {
                    DBTradeOpen_Log TOLog = new DBTradeOpen_Log();
                    TOLog.AvgPrice = Order.AvgPrice;
                    TOLog.BuySell = Order.BuySell;
                    TOLog.CalPrice = Order.CalPrice;
                    TOLog.CloseDate = Order.CloseDate;
                    TOLog.CreateBy = Order.CreateBy;
                    TOLog.CreateDate = Order.CreateDate;
                    TOLog.CustomerId = Order.CustomerId;
                    TOLog.EditStatus = Order.EditStatus;
                    TOLog.Instrument = Order.Instrument;
                    TOLog.InterestCalPercent = Order.InterestCalPercent;
                    TOLog.InterestCalSum = Order.InterestCalSum;
                    TOLog.MinDay = Order.MinDay;
                    TOLog.OpenClose = Order.OpenClose;
                    TOLog.Price = Order.Price;
                    TOLog.SumDay = Order.SumDay;
                    TOLog.SumVolume = Order.SumVolume;
                    TOLog.TradeCloseIdRef = Order.TradeCloseIdRef;
                    TOLog.TradeDate = Order.TradeDate;
                    TOLog.TradeOpenId = Order.TradeOpenId;
                    TOLog.TradeOpenIdRef = Order.TradeOpenIdRef;
                    TOLog.TradeOpenStatusId = Order.TradeOpenStatusId;
                    TOLog.TradeOpenTypeId = Order.TradeOpenTypeId;
                    TOLog.UpdateBy = Order.UpdateBy;
                    TOLog.UpdateDate = Order.UpdateDate;
                    TOLog.Volume = Order.Volume;
                    TOLog.BusinessDate = Order.BusinessDate;
                    db.DBTradeOpen_Log.Add(TOLog);
                    db.SaveChanges();
                }
                
            }

            List<DBTradeOpen> tbOTradesNewDaily = db.DBTradeOpens.Where(t => t.TradeOpenStatusId == "A").ToList();
            foreach (var Order in tbOTradesNewDaily)
            {
                Order.BusinessDate = Convert.ToDateTime(txtTradeDate2.Text);
                db.SaveChanges();
            }
            List<DBTradeOpen> tbOTradesNewDailyReMove = db.DBTradeOpens.Where(t => t.TradeOpenStatusId != "A").ToList();
            foreach (var Order in tbOTradesNewDailyReMove)
            {
                db.DBTradeOpens.Remove(Order);
                db.SaveChanges();
            }
            #endregion
        }

        protected void btnCheckDate_Click(object sender, EventArgs e)
        {
            DateTime TradeDateTime = Convert.ToDateTime(txtTradeDate.Text);

            DBStockMarketEntities db = new DBStockMarketEntities();
            List<DBLogPriceOfCustomer> POC = db.DBLogPriceOfCustomers.Where(t => t.LogPriceDate == TradeDateTime).ToList();
            if (POC.Count == 0)
            {
                //เช็ควันที่กรอกข้อมูลตรงกับวันเสาร์-อาทิตย์หรือเปล่า
                if (TradeDateTime.DayOfWeek == DayOfWeek.Saturday || TradeDateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    txtTradeDate.Text = "";
                    txtTradeDate2.Text = "";
                    lblErrorInPopup.Text = "วันที่กรอกข้อมูลตรงกับวันเสาร์-อาทิตย์";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "ShowDialog();", true);
                }
                else
                {
                    DateTime SettelmentDateTime = TradeDateTime.AddDays(1);
                    if (SettelmentDateTime.DayOfWeek == DayOfWeek.Saturday)
                    {
                        SettelmentDateTime.AddDays(1);
                    }
                    else if (SettelmentDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        SettelmentDateTime.AddDays(2);
                    }
                    txtTradeDate2.Text = SettelmentDateTime.ToString("MM/dd/yyyy");
                }
            }
            else
            {
                lblErrorInPopup.Text = "มีรายการปิดตลาดวันที่นี้ไปแล้ว กรูณากรอกวันที่ใหม่";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "ShowDialog();", true);
            }
        }

        

    }
}