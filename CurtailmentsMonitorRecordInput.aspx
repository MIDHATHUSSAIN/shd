<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurtailmentsMonitorRecordInput.aspx.cs" Inherits="Curtailments_CurtailmentsMonitorRecordInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
          <telerik:RadScriptManager runat="server" ID="scrptMgr" ></telerik:RadScriptManager>
        <div>
            <h1>Curtailments Monitor Record </h1><br />
            <div class="main">
                <div>
                    <asp:Label runat ="server" >WTG : </asp:Label>
                    <telerik:RadComboBox RenderMode="Lightweight"  ID="WTG" runat="server" AutoPostBack="true" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" Width="200" > </telerik:RadComboBox>
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
                <div><asp:Button runat="server" ID="CMR" Text="Add Record" OnClick="CMR_Click"/></div>
            </div>
            <asp:Literal runat="server" ID="test"></asp:Literal>
              <asp:Literal runat="server" ID="Literal1"></asp:Literal>
             <asp:Literal runat="server" ID="Literal2"></asp:Literal>
        </div>
    </form>
</body>
</html>
