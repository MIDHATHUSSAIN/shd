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
                <div><asp:Button runat="server" ID="CMR" Text="Add Record" OnClick="AddRecord"/></div>
            </div>
            <br /><br />
           <div class="new">
                <telerik:RadGrid ID="gridView" runat="server"  AutoGenerateColumns="false"  >
                     <MasterTableView  >
                         <Columns>
                        <telerik:GridBoundColumn DataField="SystemNumbers" HeaderText="SystemNumbers" HeaderStyle-BackColor="Yellow" Visible="" ItemStyle-BackColor="#66ccff" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WTGName" HeaderText="ProductName"  ItemStyle-BackColor="#66ccff"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DateFrom" HeaderText="ProductPrice"  ItemStyle-BackColor="#66ccff" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DateTo" HeaderText="ProductQunatity"  ItemStyle-BackColor="#66ccff" ></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Edit" ItemStyle-BackColor="#66ccff">
                            <ItemTemplate ><asp:Button runat="server" Text="Edit" ForeColor="#ff6699" Width="70px" BackColor="#ccccff" cssClass="if" CommandArgument='<%#Eval("ProductId")%>' ID="EditButton" OnClick="EditButton_Click"/></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Delete" ItemStyle-BackColor="#66ccff">
                            <ItemTemplate><asp:Button runat="server" Text="Delete" BackColor="Red" Width="70px"  cssClass="if" CommandArgument='<%#Eval("ProductId")%>' ID="DeleteButton" OnClientClick="abc()" OnClick="DeleteButton_Click" /></ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </form>
</body>
</html>
