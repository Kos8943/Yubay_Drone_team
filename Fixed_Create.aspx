<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Fixed_Create.aspx.cs" Inherits="Yubay_Drone_team.Fixed_Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .FormArea {
            width: 50%;
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

        .dropdownListWidth {
            width: 180px;
        }


        /* xl - Extra large devices (large desktops, 1200px and up) */
        @media (min-width: 1282px) {

            .FormArea {
                padding: 0 2%;
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
        <div class="container-fluid">
            <div class="titleAreaMargin" style="text-align: center;">
                <asp:Label ID="FixedTittle" CssClass="title" runat="server" Text="新增無人機維修紀錄"></asp:Label>
            </div>
            <div style="border-bottom: 1px solid black; margin-bottom: 30px;"></div>

            <div>
                <label>無人機編號</label>
                <asp:DropDownList ID="DropDownList_Drone" runat="server"></asp:DropDownList>
            </div>
            <div>
                <label>更換部件</label>
                <asp:TextBox ID="Text_FixChange" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>故障日期</label>
                <asp:TextBox ID="Text_StopDate" runat="server" type="date"></asp:TextBox>
            </div>
            <div>
                <label>送修時間</label>
                <asp:TextBox ID="Text_SendDate" runat="server" type="date"></asp:TextBox>
            </div>
            <div>
                <label>維修廠商</label>
                <asp:TextBox ID="Text_FixVendor" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>故障原因</label>
                <asp:TextBox ID="Text_StopReason" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>備註</label>
                <asp:TextBox ID="Text_Remarks" runat="server" type="textarea"></asp:TextBox>
            </div>
        </div>
        <div>
            <asp:Button ID="Btn_Create" runat="server" Text="建立" OnClick="Btn_Create_Click" />
            <asp:Button ID="Btm_Cancel" runat="server" Text="取消" OnClick="Btm_Cancel_Click" />
        </div>
        <asp:Label ID="ltMsg" runat="server" Text="新增成功" Visible="false"></asp:Label>
    </div>
</asp:Content>
