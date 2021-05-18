<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Battery.aspx.cs" Inherits="Yubay_Drone_team.Battery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

       #BtnCreate{
        width:50px;
      }

    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TableListArea">
        <div class="ToolArea d-flex bd-highlight mb-2">
            <asp:Button ID="BtnCreate" class="mr-auto bd-highlight btn btn-info" runat="server" Text="＋新增" OnClick="Add_Click" />
            <%--<button class="mr-auto bd-highlight">
                <img  src="Imgs/plus1.svg" style="width:18px; height:18px;"/>新增
            </button>--%>
            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="./Imgs/plus.svg"/>--%>
            <asp:DropDownList ID="DropDownListSearch" runat="server" CssClass="ToolMagin">
                <asp:ListItem Value="Drone_ID" Text="電池編號"></asp:ListItem>
                <asp:ListItem Value="Status" Text="使用狀態"></asp:ListItem>
                <asp:ListItem Value="StopReason" Text="停用原因"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="textKeyWord" runat="server" CssClass="ToolMagin"></asp:TextBox>
            <%--<asp:Button ID="BtnSearch" runat="server" Text="查詢" CssClass="ToolMagin"/>--%>

            <button runat="server" onserverclick="btnSearch_Click" class="btn btn-light ToolMagin" style="border: 1px solid gray">
                <img src="Imgs/search.svg" style="width: 20px; height: 15px;" />
                <span>查詢</span>
            </button>
            <%-- <asp:Button ID="btnSearch" runat="server" Text="查詢" CssClass="ToolMagin" OnClick="btnSearch_Click" class="btn btn-light ToolMagin" style="border: 1px solid gray" />--%>
        </div>
        <div class="container-fluid">
            <table class="table table-hover" style="font-size: 18px;">
                <thead>
                    <tr class="table-primary tdHeight row">
                        <th class="col-2">電池編號</th>
                        <th class="col-2">電池編號</th>
                        <th class="col-8">停用原因</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repInvoice" runat="server" OnItemCommand="repInvoice_ItemCommand">
                        <ItemTemplate>
                            <tr class="row">
                                <td class="col-2"><%# Eval("Battery_ID") %></td>
                                <td class="col-2"><%# Eval("status") %></td>
                                <td class="col-6"><%# Eval("stopReason") %>kg</td>
                                <%--<td>
                                <button runat="server" class="btn btn-outline-secondary btn-sm" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>'>修改</button>
                            </td>--%>
                                <td>
                                    <asp:Button CssClass="btn btn-outline-secondary btn-sm" runat="server" Text="修改" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>' /></td>
                                <td>
                                    <asp:Button CssClass="btn btn-outline-danger btn-sm" runat="server" Text="刪除" CommandName="DeleItem" CommandArgument='<%# Eval("Sid") %>' OnClientClick="javascript:return confirm('確定刪除?')" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
            </table>
        </div>

    </div>

</asp:Content>
