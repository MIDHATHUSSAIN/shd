<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
     <telerik:RadScriptManager runat="server" ID="scrptMgr" ></telerik:RadScriptManager>
        <div>
            <h1>Curtailments Monitor Record </h1><br />
            <div class="main">
                <div>
                     <telerik:RadComboBox RenderMode="Lightweight" ID="WTG" runat="server" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" Width="200px">
                            <Items>
                                <telerik:RadComboBoxItem Text="Turbine A" Value="T1" />
                                <telerik:RadComboBoxItem Text="Turbine B" Value="T2" />
                                <telerik:RadComboBoxItem Text="Turbine C" Value="T3" />
                            </Items>
                        </telerik:RadComboBox>
                </div>
                <div>
                     <asp:Label runat ="server" >Start Date/Time : </asp:Label>
                    <telerik:RadDateTimePicker ID="From" runat="server" AutoPostBack="true" Width="250" Height="35" DateInput-DateFormat="yyyy-MM-dd HH:mm"></telerik:RadDateTimePicker>
                </div>
                <div>
                     <asp:Label runat ="server" >End Date/Time : </asp:Label>
                    <telerik:RadDateTimePicker ID="To" runat="server" AutoPostBack="true" Width="250" Height="35" DateInput-DateFormat="yyyy-MM-dd HH:mm"></telerik:RadDateTimePicker>
                </div>
                <div>
                    <asp:Label runat ="server" >Set Point : </asp:Label>
                    <telerik:RadNumericTextBox ID="SetPoit" runat="server" MinValue="1" MaxValue="50"  DecimalDigits="2" Width="200" ></telerik:RadNumericTextBox>
                        

                </div>

                <div>
                   <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />

                </div>
            </div>
            <br /><br />
           <div class="new">
                <asp:Table runat="server" ID="pink">

                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>
