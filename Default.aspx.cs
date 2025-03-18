using System;

// If working with SQL Server
using System.Linq; // Required for LINQ

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using Telerik.Windows.Documents.Flow.Model.StructuredDocumentTags;
using Telerik.Web.Design;

public partial class Default : System.Web.UI.Page 
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


            //WTGBinding();


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
    protected void btnAdd_Click(object sender, EventArgs e)
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

            foreach (DataRow row in DateTimeTable.AsEnumerable())
            {
                string wtgNamee = row.Field<string>("WTGName");
                DateTime dateFrom = row.Field<DateTime>("DateFrom");
                DateTime dateTo = row.Field<DateTime>("DateTo");
                decimal setpoint = row.Field<decimal>("SetPoint");
                int SystemNumber = row.Field<int>("SetPoint");
                Console.WriteLine($"WTG: {wtgName}, Start: {dateFrom}, End: {dateTo}");

                TableRow ParentRow = new TableRow();

                int rowIndex = DateTimeTable.Rows.IndexOf(row) ; 
                ParentRow.ID = "Row_" + rowIndex;
                TableCell ParentCell1 = new TableCell();

                ParentCell1.Text = wtgNamee;
                TableCell ParentCell2 = new TableCell();

                ParentCell2.Text = dateFrom.ToString();

                TableCell ParentCell3 = new TableCell();

                ParentCell3.Text = dateTo.ToString();

                TableCell ParentCell4 = new TableCell();

                ParentCell4.Text = setpoint.ToString();

                TableCell ParentCell5 = new TableCell();

                Button removeBtn = new Button();
                removeBtn.Text = "Remove";
                removeBtn.Click += RemoveRow;
                ParentCell5.Controls.Add( removeBtn );

                TableCell ParentCell6 = new TableCell();

                Button SubSetPoint = new Button();
                SubSetPoint.Text = "Sub Set Point";
                SubSetPoint.Click += AddSubSetPoint;
                ParentCell6.Controls.Add(SubSetPoint);

                ParentRow.Cells.Add(ParentCell1);
                ParentRow.Cells.Add(ParentCell2);
                ParentRow.Cells.Add(ParentCell3);
                ParentRow.Cells.Add(ParentCell4);
                ParentRow.Cells.Add(ParentCell5);
                ParentRow.Cells.Add(ParentCell6);

                pink.Rows.Add(ParentRow);

            }

        }
    }

    private void RemoveRow(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AddSubSetPoint(object sender, EventArgs e)
    {
        RadComboBox wtgComboBox = new RadComboBox();
        wtgComboBox.ID = "WTGG";
        wtgComboBox.Width = Unit.Pixel(200);
        wtgComboBox.RenderMode = RenderMode.Lightweight;
        wtgComboBox.CheckBoxes = true;
        wtgComboBox.EnableCheckAllItemsCheckBox = true;
        myPlaceholder.Controls.Add(wtgComboBox);

        RadDateTimePicker FromDateforSub = new RadDateTimePicker();
        FromDateforSub.ID = "SubSetPointDate";
        FromDateforSub.AutoPostBack = true;
        FromDateforSub.DateInput.DisplayDateFormat = "yyyy-MM-dd HH:mm";
        FromDateforSub.Height = 35;
        FromDateforSub.Width = 250;

        RadNumericTextBox SebSetPoint = new RadNumericTextBox();

        SebSetPoint.ID = "SebSetPoint";
        SebSetPoint.DecimalDigits = 2;
        SebSetPoint.Width = Unit.Pixel(100);
        SebSetPoint.MinValue = 0;
        SebSetPoint.MaxValue = 100;

        TableRow ParentRowInput = new TableRow();
        TableCell ParentCellInput1 = new TableCell();

        ParentCellInput1.Controls.Add(wtgComboBox);


        TableCell ParentCellInput2 = new TableCell();

        ParentCellInput2.Controls.Add(FromDateforSub);

        TableCell ParentCellInput3 = new TableCell();

        ParentCellInput3.Controls.Add(SebSetPoint);

        TableCell ParentCellInput4 = new TableCell();

        Button AddSubSet = new Button();
        AddSubSet.Text = "Add";
        AddSubSet.Click += AddSub;
        ParentCellInput4.Controls.Add(AddSubSet);



    }

    private void AddSub(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    protected void btnRemove_Command(object sender, CommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (rowIndex >= 0 && rowIndex < DateTimeTable.Rows.Count)
        {
            DateTimeTable.Rows.RemoveAt(rowIndex);
           
        }
       
    }

    protected void EditButton_Click(object sender, EventArgs e)
    {
        ShowAlert("heoolo");
    }
}
