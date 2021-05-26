<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Customer_Create.aspx.cs" Inherits="Yubay_Drone_team.CustomerCreate" %>

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
            <asp:Label ID="CreateCustomer" CssClass="title" runat="server" Text="新增客戶資料"></asp:Label>
        </div>
    <div style="text-align:center">
        <asp:Label style="color:red;" ID="Label2" runat="server" Text="*為必填"></asp:Label>
    </div>
        <div style="border-bottom:1px solid black; margin-bottom:30px;"></div>  
        <div class="d-flex justify-content-between inputmarin">
             <p style="color:red; display: inline">*</p>
            <label for="ContentPlaceHolder1_Text_Number">姓名</label>
            <asp:TextBox ID="Text_Name" CssClass="inputsize" runat="server"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <p style="color:red; display:inline">*</p>          
            <label for="ContentPlaceHolder1_Text_Manufacturer">地址</label>
            <asp:TextBox ID="Text_Address" CssClass="inputsize" runat="server"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
             <p style="color:red; display:inline">*</p>
            <label for="ContentPlaceHolder1_Text_Weight">電話</label>      
            <asp:TextBox ID="Text_Phone" CssClass="inputsize" runat="server" MaxLength="10" 
                onkeyup="value=value.replace(/[^\d]/g,'')"
                onKeyDown="if(this.value.length==10 && event.keyCode!=8) return false;"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_DropDownList_Status">農作物</label>
            <asp:TextBox ID="Text_Crop" runat="server"></asp:TextBox>

        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_Deactive">土地面積</label>
            <asp:TextBox ID="Text_Area" CssClass="inputsize" runat="server"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_DropDownList_Operator">地號</label>
            <asp:TextBox ID="Text_Farm_Address" runat="server"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-center buttonArea">
            <asp:Button ID="Btn_Create" CssClass="Btn_Create btn"  runat="server" Text="建立" OnClick="Btn_Create_Click" />
             <a href="Customer_Detail.aspx" onclick="return confirm('是否取消新增/修改畫面，並轉跳至無人機管理畫面');" 
              type="button" class="btn">取消</a> 
        </div>
        <asp:Label ID="Label1" runat="server" Text="新增成功!" CssClass="errMsg" Visible="false"></asp:Label>
    </div>
</asp:Content>
