<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Drone_Create.aspx.cs" Inherits="Yubay_Drone_team.Drone_Create" %>

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
    <%--用Div作為輸出欄位,span為標題--%>

   

    <div class="FormArea">
        <div class="titleAreaMargin" style="text-align: center;">
            <asp:Label ID="CreateDrone" CssClass="title" runat="server" Text="新增無人機"></asp:Label>
        </div>
            <div style=" text-align:center" >    
            <asp:Label style="color: red;"  ID ="Label2" runat="server" Text="*為必填"></asp:Label>
        </div>
        <div style="border-bottom: 1px solid black; margin-bottom: 30px;"></div>
        <div class="d-flex justify-content-between inputmarin">
            <p style="color: red; display: inline">*</p>
            <label for="ContentPlaceHolder1_Text_Number">編號</label>
            <asp:TextBox ID="Text_Number" CssClass="inputsize" runat="server" MaxLength="25"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <p style="color: red; display: inline">*</p>
            <label for="ContentPlaceHolder1_Text_Manufacturer">製造商</label>
            <asp:TextBox ID="Text_Manufacturer" CssClass="inputsize" runat="server" MaxLength="25"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <p style="color: red; display: inline">*</p>
            <label for="ContentPlaceHolder1_Text_Weight">最大起飛重量(kg)</label>
            <asp:TextBox ID="Text_Weight" TextMode="Number" CssClass="inputsize" runat="server" min="1"
                max="99999" MaxLength="5"
                  onKeyDown="if(this.value.length==5 && event.keyCode!=8) return false;"
                onkeyup="if(event.keyCode !=37 && event.keyCode != 39)value=value.replace(/\D/g,'')"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_DropDownList_Status">使用狀態</label>
            <asp:DropDownList ID="DropDownList_Status" CssClass="inputsize" runat="server">
                <asp:ListItem Text="啟用" Value="啟用"></asp:ListItem>
                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                <asp:ListItem Text="故障" Value="故障"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_Text_Deactive">停用原因</label>
            <asp:TextBox ID="Text_Deactive" CssClass="inputsize" runat="server" MaxLength="20"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between inputmarin">
            <label for="ContentPlaceHolder1_DropDownList_Operator">負責人員</label>
            <asp:DropDownList ID="DropDownList_Operator" CssClass="inputsize" runat="server">
            </asp:DropDownList>
        </div>
        <div class="d-flex justify-content-center buttonArea">
            <asp:Button ID="Btn_Create" CssClass="Btn_Create btn "  runat="server" Text="建立" OnClick="Btn_Create_Click" />
           <a href="Drone_Detail.aspx" onclick="return confirm('是否取消新增/修改畫面，並轉跳至無人機管理畫面');" 
              type="button" class="btn">取消</a> 
        </div>
        <asp:Label ID="Label1" runat="server" Text="新增成功!" CssClass="errMsg" Visible="false"></asp:Label>    
        <%--<asp:Literal ID="Literal1" runat="server" Text="新增成功!"  Visible="false"></asp:Literal>--%>
    </div>

</asp:Content>
