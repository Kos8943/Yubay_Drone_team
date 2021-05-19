<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Battery_Create.aspx.cs" Inherits="Yubay_Drone_team.Battery_Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
     .FormArea {
            width: 40%;
            border: 1px #111 solid;
            padding: 0 5%;
            background-color: rgb(254,254,254);
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

        .checkBoxStyle {
            margin-left: 35px;
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
                margin-bottom: 35px;
            }
        }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="FormArea">
            <div class="titleAreaMargin" style="text-align: center;">
                <asp:Label ID="BatteryTittle" CssClass="title" runat="server" Text="新增電池管理"></asp:Label>
            </div>

            <div style="border-bottom: 1px solid black; margin-bottom: 30px;"></div>

            <div class="d-flex justify-content-between inputmarin">
                <label for="ContentPlaceHolder1_Text_Battery_ID">電池編號</label>
                <asp:TextBox ID="Text_Battery_ID" CssClass="inputsize"  runat="server"></asp:TextBox>
            </div>
            <div class="d-flex justify-content-between inputmarin">
                <label for="ContentPlaceHolder1_Text_Status">使用狀況</label>
                <asp:TextBox ID="Text_Status" CssClass="inputsize"  runat="server"></asp:TextBox>
            </div>
            <div class="d-flex justify-content-between inputmarin">
                <label for="ContentPlaceHolder1_Text_StopReason">故障原因</label>
                <asp:TextBox ID="Text_StopReason" CssClass="inputsize"  runat="server"></asp:TextBox>
            </div>
        <div class="d-flex justify-content-center buttonArea">
            <asp:Button ID="Btn_Create" CssClass="Btn_Create" runat="server" Text="建立" OnClick="Btn_Create_Click" />
            <asp:Button ID="Btm_Cancel" runat="server" Text="取消" OnClick="Btm_Cancel_Click" />
        </div>
        <asp:Label ID="ltMsg" runat="server" Text="新增成功" CssClass="errMsg"  Visible="false"></asp:Label>
    </div>
</asp:Content>
