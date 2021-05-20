<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Fixed.aspx.cs" Inherits="Yubay_Drone_team.Fixed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TableListArea">
        <div class="ToolArea d-flex bd-highlight mb-2">
            <asp:Button ID="BtnCreate" class="mr-auto bd-highlight btn btn-info" runat="server" Text="＋新增" OnClick="Add_Click"/>
            <%--<button class="mr-auto bd-highlight">
                <img  src="Imgs/plus1.svg" style="width:18px; height:18px;"/>新增
            </button>--%>
            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="./Imgs/plus.svg"/>--%>
            <asp:DropDownList ID="DropDownListSearch" runat="server" CssClass="ToolMagin">
                <asp:ListItem Value="Drone_ID" Text="無人機編號"></asp:ListItem>
                <asp:ListItem Value="FixChange" Text="更換零件"></asp:ListItem>
                <asp:ListItem Value="StopDate" Text="停用時間"></asp:ListItem>
                <asp:ListItem Value="SendDate" Text="送修時間"></asp:ListItem>
                <asp:ListItem Value="FixVendor" Text="維修廠商"></asp:ListItem>
                <asp:ListItem Value="StopReason" Text="故障原因"></asp:ListItem>
                <asp:ListItem Value="Remarks" Text="備註"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="textKeyWord" runat="server" CssClass="ToolMagin"></asp:TextBox>
            <%--<asp:Button ID="BtnSearch" runat="server" Text="查詢" CssClass="ToolMagin"/>--%>

            <button runat="server" onserverclick="btnSearch_Click" class="btn btn-light ToolMagin" style="border:1px solid gray">
                <img src="Imgs/search.svg" style="width:20px; height:15px;"/>
                <span>查詢</span>
            </button>
           <%-- <asp:Button ID="btnSearch" runat="server" Text="查詢" CssClass="ToolMagin" OnClick="btnSearch_Click" class="btn btn-light ToolMagin" style="border: 1px solid gray" />--%>
        </div>
        <table class="table table-hover" style="font-size: 18px;">
            <thead>
                <tr class="tdHeight">
                    <th scope="col">無人機編號</th>
                    <th scope="col">更換零件</th>
                    <th scope="col">停用時間</th>
                    <th scope="col">送修時間 </th>
                    <th scope="col">維修廠商</th>
                    <th scope="col">故障原因</th>
                    <th scope="col">備註</th>
                    <th scope="col"></th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repInvoice" runat="server" OnItemCommand="repInvoice_ItemCommand1">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Drone_ID") %></td>
                            <td><%# Eval("FixChange") %></td>
                            <td><%#Convert.ToDateTime(Eval("StopDate")).ToShortDateString() %></td>
                            <td><%#Convert.ToDateTime(Eval("SendDate")).ToShortDateString() %></td>
                            <td><%# Eval("FixVendor") %></td>
                            <td><%# Eval("StopReason") %></td>
                            <td><%# Eval("Remarks") %></td>
                            <%--<td>
                                <button runat="server" class="btn btn-outline-secondary btn-sm" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>'>修改</button>
                            </td>--%>
                            <td><asp:Button CssClass="btn btn-outline-secondary btn-sm" runat="server" Text="修改" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>' /></td>
                            <td><asp:Button CssClass="btn btn-outline-danger btn-sm BtnDel" runat="server" Text="刪除" CommandName="DeleItem" CommandArgument='<%# Eval("Sid") %>' OnClientClick="javascript:return confirm('確定刪除?')"/></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </tbody>
        </table>


    </div>
</asp:Content>
