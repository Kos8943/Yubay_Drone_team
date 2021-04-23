<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Customer_Create.aspx.cs" Inherits="Yubay_Drone_team.CustomerCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ToolArea{
            height:35px;
        }
        

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="ToolArea d-flex bd-highlight mb-2">
        <asp:Button ID="BtnCustomer" class="mr-auto bd-highlight btn btn-info" runat="server" Text="+新增" OnClick="BtnCustomer_Click" />
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="Name" Text="姓名"></asp:ListItem>
            <asp:ListItem Value="Address" Text="地址"></asp:ListItem>
            <asp:ListItem Value="Phone" Text="電話"></asp:ListItem>
             <asp:ListItem Value="Crop" Text="農作物"></asp:ListItem>
             <asp:ListItem Value="Area" Text="土地面積"></asp:ListItem>
              <asp:ListItem Value="Farm_" Text="地號"></asp:ListItem>

        </asp:DropDownList>

    </div>

</asp:Content>
