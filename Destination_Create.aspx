<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Destination_Create.aspx.cs" Inherits="Yubay_Drone_team.Destination_Create" %>

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

        .dropdownListWidth{
            width:180px;
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
                        <asp:Label ID="UserAccountTittle" CssClass="title" runat="server" Text="新增無人機出勤紀錄"></asp:Label>
                    </div>
             <div style="border-bottom: 1px solid black; margin-bottom: 30px;"></div>
            <div class="row">
                <div class="col-6">
                   

                    <%--<div style="border-bottom: 1px solid black; margin-bottom: 30px;"></div>--%>


                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Date">出勤日期</label>
                        <asp:TextBox ID="Text_Date" CssClass="inputsize" runat="server" type="date"></asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Staff">出勤人員</label>
                        <asp:TextBox ID="Text_Staff" CssClass="inputsize" runat="server"></asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_DropDownList_Drone">使用無人機</label>
                        <asp:DropDownList runat="server" id="DropDownList_Drone" class="dropdownListWidth"></asp:DropDownList>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Battery">攜帶電池數量</label>
                        <asp:TextBox ID="Text_Battery" CssClass="inputsize" runat="server"></asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Remarks">備註</label>
                        <asp:TextBox ID="Text_Remarks" CssClass="inputsize" runat="server" type="textarea"></asp:TextBox>
                    </div>
                </div>

                <div class="col-6">
                    <%--<div style="border-bottom: 1px solid black; margin-bottom: 30px;"></div>--%>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_DropDownList_Customer_Name">客戶姓名</label>
                        <asp:DropDownList runat="server" id="DropDownList_Customer_Name" class="dropdownListWidth ddlCustomerName"></asp:DropDownList>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Phone">客戶電話</label>
                        <asp:TextBox ID="Text_Phone" CssClass="inputsize costomerPhone" runat="server"></asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Address">客戶地址</label>
                        <asp:TextBox ID="Text_Address" CssClass="inputsize costomerAddress" runat="server"></asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Pesticide">使用農藥</label>
                        <asp:TextBox ID="Text_Pesticide" CssClass="inputsize" runat="server"></asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-between inputmarin">
                        <label for="ContentPlaceHolder1_Text_Pesticide_Date">農藥有效日期</label>
                        <asp:TextBox ID="Text_Pesticide_Date" CssClass="inputsize" runat="server" type="date"></asp:TextBox>
                    </div>

                    
                </div>

            </div>
        </div>

        <div class="d-flex justify-content-center buttonArea">
            <asp:Button ID="Btn_Create" CssClass="Btn_Create" runat="server" Text="建立" OnClick="Btn_Create_Click" />
            <asp:Button ID="Btn_Cancel" runat="server" Text="取消" OnClick="Btn_Cancel_Click" />
        </div>
        <asp:Label ID="ltMsg" runat="server" Text="新增成功!" CssClass="d-flex justify-content-center errMsg" Visible="false"></asp:Label>
        <%--<asp:Literal ID="Literal1" runat="server" Text="新增成功!"  Visible="false"></asp:Literal>--%>
    </div>


    <script>
        $(".ddlCustomerName").change(function () {
            var Sid = $(this).val();
            $.ajax({
                method: "POST",
                url: "API/GetCustomerDetail.ashx",
                type: "JSON",
                data: { "Sid": Sid }
            }).done(function (responseData) {
                console.log(responseData);
                console.log($(".costomerPhone"))
                $(".costomerPhone").val(responseData["Phone"]);
                $(".costomerAddress").val(responseData["Address"]);
            });
        });
    </script>
</asp:Content>
