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

    private DataTable DateTimeTable
    {
        get
        {
            if (ViewState["DateTimeTable"] == null)
            {
                DataTable DT = new DataTable();
                DT.Columns.Add("UserID", typeof(int));
                DT.Columns.Add("DateFrom", typeof(DateTime));
                DT.Columns.Add("DateTo", typeof(DateTime));
                DT.Columns.Add("WTGName", typeof(string));
                DT.Columns.Add("SetPoint", typeof(decimal));
                DT.Columns.Add("SystemNumbers", typeof(string));

                ViewState["DateTimeTable"] = DT;
            }

            return (DataTable)ViewState["DateTimeTable"];
        }
        set
        {
            ViewState["DateTimeTable"] = value;
        }
    }

    public DataTable SubDateTimeTable
    {
        get
        {
            if (Session["SubDataTable"] == null)
            {
                DataTable SDT = new DataTable();
                SDT.Columns.Add("SubDateFrom", typeof(DateTime));
                SDT.Columns.Add("SubDateTo", typeof(DateTime));
                SDT.Columns.Add("SubWTGName", typeof(string));
                SDT.Columns.Add("SubSetPoint", typeof(decimal));
                SDT.Columns.Add("SystemNumbers", typeof(string));
                Session["SubDataTable"] = SDT;

            }

            return (DataTable)Session["SubDataTable"];

        }
        set
        {
            Session["SubDataTable"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {


            WTGBinding();


        }
    }

    protected void WTGBinding()
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

    protected bool DataValidation(out DateTime DateFrom, out DateTime DateTo)
    {
        DateFrom = DateTime.MinValue;
        DateTo = DateTime.MinValue;

        if (string.IsNullOrEmpty(From.SelectedDate.ToString()) || string.IsNullOrEmpty(To.SelectedDate.ToString()))
        {
            ShowAlert("Kindly fill in both date fields.");
            return false;
        }

        DateFrom = From.SelectedDate.Value; /*|| SubFrom.SelectedDate.Value ;*/
        DateTo = To.SelectedDate.Value; /* || SubTo.SelectedDate.Value;*/

        if (DateFrom > DateTo)
        {
            ShowAlert("Start date/time must be earlier than end date/time.");
            return false;
        }

        return true;
    }

    private void ShowAlert(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
    }
    protected void AddRecord(object sender, EventArgs e)
    {

        DateTime DateFrom;
        DateTime DateTo;
        decimal SetPoint = (decimal)SetPoit.Value;

        if (!DataValidation(out DateFrom, out DateTo))
        {
            return;
        }

        var collection = WTG.CheckedItems;

        if (collection.Count == 0)
        {
            ShowAlert("Please select at least one item from the WTG list.");
            return;
        }

        bool exist = false;
        for (int i = 0; i < DateTimeTable.Columns.Count; i++)
        {

            if (DateTimeTable.Columns[i].ColumnName == "WTGName")
            {
                exist = true;
            }
        }
        if (exist == false)
        {
            DateTimeTable.Columns.Add("WTGName", typeof(string));
        }


        foreach (var item in collection)
        {
            string wtgName = item.Text;
            string wtgId = item.Value;

            bool exists = DateTimeTable.AsEnumerable().Any(row =>
                row.Field<string>("SystemNumbers") == wtgId &&
                row.Field<DateTime>("DateFrom") == DateFrom &&
                row.Field<DateTime>("DateTo") == DateTo
            );

            if (exists)
            {
                //ShowAlert("The WTG " + wtgName + "with the specified date range already exists in the table.");
                ShowAlert("Some WTGs with the specified date range already exists in the table.");
                continue;
            }

            DataRow newRow = DateTimeTable.NewRow();
            newRow["UserID"] = 123; /*Session["ID"];*/
            newRow["SystemNumbers"] = wtgId;
            newRow["WTGName"] = wtgName;
            newRow["DateFrom"] = DateFrom;
            newRow["DateTo"] = DateTo;
            newRow["SetPoint"] = SetPoint;
            DateTimeTable.Rows.Add(newRow);

            gridView.DataSource = DateTimeTable;
            gridView.DataBind();

        }
    }


    protected void btnRemove_Command(object sender, CommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (rowIndex >= 0 && rowIndex < DateTimeTable.Rows.Count)
        {
            DateTimeTable.Rows.RemoveAt(rowIndex);
            gridView.DataSource = DateTimeTable;
            gridView.DataBind();
        }
        //DateTimeTable.Rows.RemoveAt(rowIndex);
        //BindGrid();
    }

    protected void AddSubSetPoint(object sender, CommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            // Create a new table row
            TableRow inputRow = new TableRow();

            // Create table cells with input fields
            TableCell nameCell = new TableCell();
            nameCell.Controls.Add(new LiteralControl("<input type='text' placeholder='Enter Name' class='form-control' />"));

            TableCell ageCell = new TableCell();
            ageCell.Controls.Add(new LiteralControl("<input type='number' placeholder='Enter Age' class='form-control' />"));

            TableCell genderCell = new TableCell();
            genderCell.Controls.Add(new LiteralControl("<select class='form-control'><option>Male</option><option>Female</option></select>"));

            TableCell actionCell = new TableCell();
            actionCell.Controls.Add(new LiteralControl("<button class='btn btn-primary' onclick='saveRowData(this)'>Save</button>"));

            // Add cells to the row
            inputRow.Cells.Add(nameCell);
            inputRow.Cells.Add(ageCell);
            inputRow.Cells.Add(genderCell);
            inputRow.Cells.Add(actionCell);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }
}
