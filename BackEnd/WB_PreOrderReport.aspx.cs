using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PGMKTStock.BackEnd
{
    public partial class WB_PreOrderReport : System.Web.UI.Page
    {
        ParameterFields paramFields = new ParameterFields();
        ParameterField paramField1 = new ParameterField();
        ParameterField paramField2 = new ParameterField();
        ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();
        ParameterDiscreteValue paramDiscreteValue2 = new ParameterDiscreteValue();
        ReportDocument reportDocument = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    DBStockMarketEntities db = new DBStockMarketEntities();
                    List<DBCustomer> tbServiceStatus = db.DBCustomers.ToList();
                    ddlCustomer.DataSource = tbServiceStatus;
                    ddlCustomer.DataTextField = "CustomerName";
                    ddlCustomer.DataValueField = "CustomerId";
                    ddlCustomer.DataBind();
                    ddlCustomer.Items.Insert(0, new ListItem("--เลือกลูกค้า--", "0"));
                    Session.Remove("CrFuit");
                }
                this.CrystalReportViewer1.ReportSource = Session["CrFuit"];
            }
        }

        public void Clear()
        {
            if (!IsPostBack)
            {
                Session.Remove("CrFuit");
            }
            Session.Remove("CrFuit");
            this.CrystalReportViewer1.ReportSource = Session["CrFuit"];
        }

        protected void btnTrade_Click(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedValue != "0" && txtTradeDate.Text != "")
            {
                DataUpdate(txtTradeDate.Text, ddlCustomer.SelectedValue);
            }
            else
            {
                lblErrorInPopup.Text = "กรูณากรอกข้อมูลให้ครบ";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "ShowDialog();", true);
            }
        }

        public void DataUpdate(string DateStart, string Customer)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Clear();

                paramField1.Name = "@TradingDateStart";
                paramDiscreteValue1.Value = DateStart;
                paramField1.CurrentValues.Add(paramDiscreteValue1);
                paramFields.Add(paramField1);

                paramField2.Name = "@CustomerId";
                paramDiscreteValue2.Value = Customer;
                paramField2.CurrentValues.Add(paramDiscreteValue2);
                paramFields.Add(paramField2);

                CrystalReportViewer1.ParameterFieldInfo = paramFields;
                string path = Server.MapPath("CRDailyReportCustomerForheader.rpt");
                reportDocument.Load(Server.MapPath("CRDailyReportCustomerForheader.rpt"));
                reportDocument.SetDatabaseLogon("sa", "p@ssw0rd!", ".", "DBStockMarket");

                CrystalReportViewer1.ReportSource = reportDocument;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
            }
        }

    }
}