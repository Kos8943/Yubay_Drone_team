<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="User_Account.aspx.cs" Inherits="Yubay_Drone_team.User_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ToolArea{
            height:35px;
        }

        .TableListArea {
            width: 95%;
            background-color: white;
            border-radius: 10px;
            padding:2% 3%;
        }

        .ToolMagin{
            margin-left:5px;
        }

        /*#BtnCreate{
            width:50px;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="TableListArea">
        <div class="ToolArea d-flex bd-highlight mb-3">
            <asp:Button ID="BtnCreate" class="mr-auto bd-highlight btn btn-info" runat="server" Text="＋新增" />
            <%--<button class="mr-auto bd-highlight">
                <img  src="Imgs/plus1.svg" style="width:18px; height:18px;"/>新增
            </button>--%>
            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="./Imgs/plus.svg"/>--%>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ToolMagin">
                <asp:ListItem Value="Date" Text="出勤日期"></asp:ListItem>
                <asp:ListItem Value="Staff" Text="出勤人員"></asp:ListItem>
                <asp:ListItem Value="Drone_ID" Text="使用無人機"></asp:ListItem>
                <asp:ListItem Value="Battery_Count" Text="攜帶電池數量"></asp:ListItem>
                <asp:ListItem Value="Cumstomer_Name" Text="客戶名稱"></asp:ListItem>
                <asp:ListItem Value="Tel" Text="客戶電話"></asp:ListItem>
                <asp:ListItem Value="Address" Text="客戶地址"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="TextSearch" runat="server" CssClass="ToolMagin"></asp:TextBox>
            <%--<asp:Button ID="BtnSearch" runat="server" Text="查詢" CssClass="ToolMagin"/>--%>
            <button class="btn btn-light ToolMagin" style="border:1px solid gray">
                <img src="Imgs/search.svg" style="width:20px; height:15px;"/>
                <span>查詢</span>
            </button>
        </div>
        <table class="table table-hover" style="font-size:18px;">
            <thead>
                <tr class="table-primary">
                    <th scope="col">First</th>
                    <th scope="col">Last</th>
                    <th scope="col">Handle</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>

                    <td>Jacob</td>
                    <td>Thornton</td>
                    <td>@fat</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>

            </tbody>
        </table>


    </div>

    <asp:Repeater ID="TableRepeater" runat="server">
    </asp:Repeater>
</asp:Content>
