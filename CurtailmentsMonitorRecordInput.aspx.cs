using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Curtailments_CurtailmentsMonitorRecordInput : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["APMSH"].ConnectionString;

            SqlDataAdapter DA = new SqlDataAdapter();
            DA.SelectCommand = new SqlCommand();
            DA.SelectCommand.Connection = con;

            DA.SelectCommand.CommandType = CommandType.StoredProcedure;

            DA.SelectCommand.CommandText = "SP_GetWTGsList";

            DataTable dT = new DataTable();
            con.Open();
            DA.Fill(dT);
            DA.Dispose();
            con.Close();
            con.Dispose();


            WTG.DataTextField = "NAME";
            WTG.DataValueField = "ID";

            WTG.DataSource = dT;
            WTG.DataBind();

        }


    }

    protected void CMR_Click(object sender, EventArgs e)
    {
        var datetimeFrom = From.DbSelectedDate.ToString();
        //test.Text = dateTime;
        var dateTimeTo = To.DbSelectedDate.ToString();
       

        if(datetimeFrom > dateTimeTo)
        {
            Literal1.Text = "helllo";
        }

           
                var collection = WTG.CheckedItems;
        if (collection.Count == 0)
        {
            Literal2.Text = "no Item Selected";
        }
       
        Literal2.Text = collection.ToString();

    }
}