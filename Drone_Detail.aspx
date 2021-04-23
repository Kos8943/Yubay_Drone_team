﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Drone_Detail.aspx.cs" Inherits="Yubay_Drone_team.Drone_Detail" %>

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

        td{
            padding:10px!important;
        }

        .tdHeight{
            padding:10px;
        }

        /*#BtnCreate{
            width:50px;
        }*/
    </style>
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
                <asp:ListItem Value="Manufacturer" Text="製造商"></asp:ListItem>
                <asp:ListItem Value="WeightLoad" Text="最大起飛重量"></asp:ListItem>
                <asp:ListItem Value="Status" Text="使用狀態"></asp:ListItem>
                <asp:ListItem Value="StopReason" Text="停用原因"></asp:ListItem>
                <asp:ListItem Value="operator" Text="負責人員"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="textKeyWord" runat="server" CssClass="ToolMagin"></asp:TextBox>
            <%--<asp:Button ID="BtnSearch" runat="server" Text="查詢" CssClass="ToolMagin"/>--%>
            <asp:Button ID="btnSearch" runat="server" Text="查詢" CssClass="ToolMagin" OnClick="btnSearch_Click" class="btn btn-light ToolMagin" style="border: 1px solid gray" />
        </div>
        <table class="table table-hover" style="font-size: 18px;">
            <thead>
                <tr class="table-primary tdHeight">
                    <th scope="col">無人機編號</th>
                    <th scope="col">製造商</th>
                    <th scope="col">最大起飛重量</th>
                    <th scope="col">使用狀態 </th>
                    <th scope="col">停用原因</th>
                    <th scope="col">負責人員</th>
                    <th scope="col"></th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repInvoice" runat="server" OnItemCommand="repInvoice_ItemCommand1">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Drone_ID") %></td>
                            <td><%# Eval("Manufacturer") %></td>
                            <td><%# Eval("WeightLoad") %>kg</td>
                            <td><%# Eval("Status") %></td>
                            <td><%# Eval("StopReason") %></td>
                            <td><%# Eval("Operator") %></td>
                            <td><asp:Button CssClass="btn btn-outline-warning btn-sm" runat="server" Text="修改" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>' /></td>
                            <td><asp:Button CssClass="btn btn-outline-danger btn-sm" runat="server" Text="刪除" CommandName="DeleItem" CommandArgument='<%# Eval("Sid") %>' OnClientClick="javascript:return confirm('確定刪除?')"/></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </tbody>
        </table>


    </div>
</asp:Content>
