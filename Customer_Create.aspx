<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Customer_Create.aspx.cs" Inherits="Yubay_Drone_team.CustomerCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
          .FormArea {
            width: 40%;
            border: 2px #000 solid;
            padding: 0 5%;
        }

        .inputsize {
            width: 180px;
            height: 30px;
        }

        .inputmarin {
            margin-bottom: 7px;
        }

        .titleAreaMargin {
            margin: 15px 0;
        }

        .title {
            font-size: 25px;
        }

        .Number {
            width: 50px;
            text-align: left;
        }

        .Manufacturer {
            width: 50px;
            text-align: left;
        }

        .Weight {
            width: 50px;
            text-align: left;
        }

        .Deactive {
            width: 50px;
            text-align: left;
        }

        .Person {
            width: 50px;
            text-align: left;
        }

        .buttonArea {
            margin-top: 40px;
        }

        .Btn_Create {
            margin-right: 120px;
            /*margin-left: 40px;*/
        }

        .errMsg {
            color: red;
        }


        /* xl - Extra large devices (large desktops, 1200px and up) */
        @media (min-width: 1282px) {

            .FormArea {
                padding: 0 10%;
            }

            .title {
                font-size: 30px;
            }

            .titleAreaMargin {
            margin: 30px 0;
        }

            .inputmarin {
            margin-bottom: 20px;
        }
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
            <asp:ListItem Value="Farm_Address" Text="地號"></asp:ListItem>

        </asp:DropDownList>

    </div>

</asp:Content>
