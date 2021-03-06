<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Fixed_Create.aspx.cs" Inherits="Yubay_Drone_team.Fixed_Create" %>

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

        .dropdownListWidth {
            width: 180px;
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
                margin-bottom: 7px;
            }
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="FormArea">
        <div class="titleAreaMargin" style="text-align: center;">
            <asp:Label ID="FixedTittle" CssClass="title" runat="server" Text="新增無人機維修紀錄"></asp:Label>
        </div>
        <div style="text-align: center">
            <asp:Label Style="color: red;" ID="Label2" runat="server" Text="*為必填"></asp:Label>
        </div>
        <div style="border-bottom: 1px solid black; margin-bottom: 30px;"></div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_DropDownList_Drone">無人機編號</label>
            <p style="color: red; display: inline">*</p>
            <asp:DropDownList ID="DropDownList_Drone" class="dropdownListWidth" runat="server"></asp:DropDownList>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_FixChange">更換部件</label>
            <p style="color: red; display: inline">*</p>
            <asp:TextBox ID="Text_FixChange" CssClass="inputsize" runat="server" MaxLength="50"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_StopDate">故障日期</label>
            <p style="color: red; display: inline">*</p>
            <asp:TextBox ID="Text_StopDate" CssClass="inputsize" runat="server" type="date"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_SendDate">送修時間</label>
            <asp:TextBox ID="Text_SendDate" CssClass="inputsize" runat="server" type="date"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_FixVendor">維修廠商</label>
            <p style="color: red; display: inline">*</p>
            <asp:TextBox ID="Text_FixVendor" CssClass="inputsize" runat="server" MaxLength="50"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_StopReason">故障原因</label>
            <p style="color: red; display: inline">*</p>
            <asp:TextBox ID="Text_StopReason" CssClass="inputsize" runat="server" MaxLength="255"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_Remarks">備註</label>
            <asp:TextBox ID="Text_Remarks" CssClass="inputsize" runat="server" type="textarea"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-center buttonArea">
            <asp:Button ID="Btn_Create" runat="server" Text="建立" CssClass="Btn_Create" OnClick="Btn_Create_Click" />
            <asp:Button ID="Btm_Cancel" runat="server" Text="取消" OnClick="Btm_Cancel_Click" />
        </div>
        <asp:Label ID="ltMsg" runat="server" Text="新增成功" CssClass="d-flex justify-content-center errMsg" Visible="false"></asp:Label>
    </div>
</asp:Content>
