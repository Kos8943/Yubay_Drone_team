<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="User_Account.aspx.cs" Inherits="Yubay_Drone_team.User_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ToolArea {
            height: 35px;
        }

        .TableListArea {
            width: 95%;
            background-color: white;
            border-radius: 10px;
            padding: 2% 3%;
        }

        .ToolMagin {
            margin-left: 5px;
        }

        .HiddenSid{
            /*visibility:hidden;*/
        }
        /*#BtnCreate{
            width:50px;
        }*/
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
                <asp:ListItem Value="Account" Text="帳號"></asp:ListItem>
                <asp:ListItem Value="UserName" Text="名稱"></asp:ListItem>
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
                        <th class="col-2">帳號</th>
                        <th class="col-2">名稱</th>
                        <th class="col-8">刪除資料權限</th>
                        <%--<th class="col-1 HiddenSid" id="HiddenSid" runat="server"></th>--%>
                        <%--<th class="col-1"></th>--%>
                    </tr>
                </thead>
                <tbody runat="server" id="TablaBody">
                    <asp:Repeater ID="repInvoice" runat="server" OnItemCommand="repInvoice_ItemCommand1">
                        <ItemTemplate>
                            <tr class="row">
                                <td class="col-2"><%# Eval("Account") %></td>
                                <td class="col-2"><%# Eval("UserName") %></td>
                                <td class="col-6"><%# ((int)Eval("AccountLevel") == 2) ? "有" : "-" %> </td>
                                <%--<td>
                                <button runat="server" class="btn btn-outline-secondary btn-sm" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>'>修改</button>
                            </td>--%>
                                <td class="col-1">
                                    <asp:Button CssClass="btn btn-outline-secondary btn-sm" runat="server" Text="修改" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>'/>
                                </td>


                                <td class="col-1 DelCol">
                                    <asp:Button runat="server" Text="刪除" CssClass="btn btn-outline-danger btn-sm BtnDel" CommandName="DeleItem" CommandArgument='<%# Eval("Sid")+","+ Eval("Account") %>' OnClientClick="javascript:return confirm('確定刪除?')" />
                                </td>

                               <%-- CommandArgument=' <%#Eval("id")+","+Eval("name") %>' ><%#Eval("name") %>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
            </table>
        </div>

    </div>

</asp:Content>
