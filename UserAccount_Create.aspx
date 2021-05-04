<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserAccount_Create.aspx.cs" Inherits="Yubay_Drone_team.UserAccount_Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .FormArea {
            width: 40%;
            border: 1px #111 solid;
            padding: 0 5%;
            background-color:rgb(254,254,254);
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

        .checkBoxStyle{
            margin-left:35px;
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
    <div class="FormArea">
        <div class="titleAreaMargin" style="text-align: center;">
            <asp:Label ID="CreateDrone" CssClass="title" runat="server" Text="新增使用者"></asp:Label>
        </div>

        <div style="border-bottom:1px solid black; margin-bottom:30px;"></div>

        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_Account">帳號</label>
            <asp:TextBox ID="Text_Account" CssClass="inputsize" runat="server"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_Password">密碼</label>
            <asp:TextBox ID="Text_Password" CssClass="inputsize" runat="server"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_CheckPassword">確認密碼</label>
            <asp:TextBox ID="Text_CheckPassword" CssClass="inputsize" runat="server"></asp:TextBox>
        </div>
        
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_UserName">使用者名稱</label>
            <asp:TextBox ID="Text_UserName" CssClass="inputsize" runat="server"></asp:TextBox>
        </div>

        <div class="d-flex justify-content-start inputmarin">
            <label for="ContentPlaceHolder1_Text_DeleteData_Authority">資料刪除權限</label>
            <asp:CheckBox ID="DeleteData_Authority" runat="server" CssClass="checkBoxStyle"/>
        </div>
        
        <div class="d-flex justify-content-center buttonArea">
            <asp:Button ID="Btn_Create" CssClass="Btn_Create" runat="server" Text="建立" OnClick="Btn_Create_Click" />
            <asp:Button ID="Btn_Cancel" runat="server" Text="取消" OnClick="Btn_Cancel_Click" />
        </div>
        <asp:Label ID="Label1" runat="server" Text="新增成功!" CssClass="errMsg" Visible="false"></asp:Label>
        <%--<asp:Literal ID="Literal1" runat="server" Text="新增成功!"  Visible="false"></asp:Literal>--%>
    </div>
</asp:Content>
