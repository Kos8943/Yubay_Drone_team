<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Customer_Detail.aspx.cs" Inherits="Yubay_Drone_team.Customer_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>

    #BtnCreate{
        width:50px;
    }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="TableListArea">
        <div class="ToolArea d-flex bd-highlifht mb-2">
            <asp:Button ID="BtnCreate" runat="server" class="mr-auto bd-highlight btn btn-info" Text="+新增" OnClick="BtnCreate_Click" />
            <div>
            </div>
            <asp:DropDownList ID="DropDownListSearch" runat="server" CssClass="ToolMagin">
                <asp:ListItem Value="Name" Text="姓名" ></asp:ListItem>
                <asp:ListItem Value="Address" Text="地址" ></asp:ListItem>
                <asp:ListItem Value="Phone" Text="電話" ></asp:ListItem>
                <asp:ListItem Value="Crop" Text="農作物" ></asp:ListItem>
                <asp:ListItem Value="Area" Text="土地面積" ></asp:ListItem>
                 <asp:ListItem Value="Farm_Address" Text="地號" ></asp:ListItem>
             </asp:DropDownList>
            <asp:TextBox ID="textKeyWord" runat="server" CssClass="ToolMagin"></asp:TextBox>
          
            <button runat="server" onserverclick="btnSearch_Click" class="btn btn-light ToolMagin" style="border:1px solid gray">
                <img src="Imgs/search.svg" style="width:20px; height:15px;"/>
                <span>查詢</span>
            </button>
        </div>
        <table class="table table-hover" style="font-size:18px">
            <thead>
                <tr class="tdHeight">
                    <th scope="col">姓名</th>
                    <th scope="col">地址</th>
                    <th scope="col">電話</th>
                    <th scope="col">農作物</th>
                    <th scope="col">土地面積</th>
                    <th scope="col">地號</th>
                    <th scope="col"></th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
                   <asp:Repeater ID="repInvoice" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Address") %></td>
                            <td><%# Eval("Phone") %></td>
                            <td><%# Eval("Crop") %></td>
                            <td><%# Eval("Area") %></td>
                            <td><%# Eval("Farm_Address") %></td>

                            <%--<td>
                                <button runat="server" class="btn btn-outline-secondary btn-sm" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>'>修改</button>
                            </td>--%>
                            <td><asp:Button CssClass="btn btn-outline-secondary btn-sm" runat="server" Text="修改" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>' /></td>
                            <td><asp:Button CssClass="btn btn-outline-danger btn-sm BtnDel" runat="server" Text="刪除" CommandName="DeleteItem" CommandArgument='<%# Eval("Sid") + "," +Eval("Name") %>' OnClientClick="javascript:return confirm('確定刪除?')"/></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
