<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Destination.aspx.cs" Inherits="Yubay_Drone_team.Destination" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        td{
            font-size: 14px;
        }

        th{
            font-size: 14px;
        }

        .containerPaddingChange{
            padding: 0;
        }

        .ToolArea {
            height: 35px;
        }

        .TableListArea {
            width: 95%;
            background-color: white;
            border-radius: 10px;
            padding: 2% 3%;
        }

        .ToolMagin {
            margin-left: 5px;
        }

        .HiddenSid{
            /*visibility:hidden;*/
        }
        /*#BtnCreate{
            width:50px;
        }*/
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TableListArea">

        <div class="ToolArea d-flex bd-highlight mb-2">
            <asp:Button ID="BtnCreate" class="mr-auto bd-highlight btn btn-info" runat="server" Text="＋新增" OnClick="Add_Click" />
            <%--<button class="mr-auto bd-highlight">
                <img  src="Imgs/plus1.svg" style="width:18px; height:18px;"/>新增
            </button>--%>
            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="./Imgs/plus.svg"/>--%>
            <asp:DropDownList ID="DropDownListSearch" runat="server" CssClass="ToolMagin">
                <asp:ListItem Value="Date" Text="出勤日期"></asp:ListItem>
                <asp:ListItem Value="Staff" Text="出勤人員"></asp:ListItem>
                <asp:ListItem Value="Drone_ID" Text="使用無人機"></asp:ListItem>
                <asp:ListItem Value="Battery_Count" Text="攜帶電池數量"></asp:ListItem>
                <asp:ListItem Value="Customer_Name" Text="客戶姓名"></asp:ListItem>
                <asp:ListItem Value="Customer_Phone" Text="電話"></asp:ListItem>
                <asp:ListItem Value="Customer_Address" Text="地址"></asp:ListItem>
                <asp:ListItem Value="Pesticide" Text="使用農藥"></asp:ListItem>
                <asp:ListItem Value="Pesticide_Date" Text="農藥有效日期"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="textKeyWord" runat="server" CssClass="ToolMagin"></asp:TextBox>
            <%--<asp:Button ID="BtnSearch" runat="server" Text="查詢" CssClass="ToolMagin"/>--%>

            <button runat="server" onserverclick="btnSearch_Click" class="btn btn-light ToolMagin" style="border: 1px solid gray">
                <img src="Imgs/search.svg" style="width: 20px; height: 15px;" />
                <span>查詢</span>
            </button>
            <%-- <asp:Button ID="btnSearch" runat="server" Text="查詢" CssClass="ToolMagin" OnClick="btnSearch_Click" class="btn btn-light ToolMagin" style="border: 1px solid gray" />--%>
        </div>
        <div class="container-fluid containerPaddingChange">
            <table class="table table-hover" style="font-size: 18px;">
                <thead>
                    <tr class="tdHeight">
                        <th scope="col">出勤日期</th>
                        <th scope="col">出勤人員</th>
                        <th scope="col">使用無人機</th>
                        <th scope="col">攜帶電池數量</th>
                        <th scope="col">客戶姓名</th>
                        <th scope="col">電話</th>
                        <th scope="col">地址</th>
                        <th scope="col">使用農藥</th>
                        <th scope="col">農藥有效日期</th>
                        <th scope="col">備註</th>
                        <th scope="col"></th>
                        <th scope="col"></th>
                        <%--<th class="col-1 HiddenSid" id="HiddenSid" runat="server"></th>--%>
                        <%--<th class="col-1"></th>--%>
                    </tr>
                </thead>
                <tbody runat="server" id="TablaBody">
                    <asp:Repeater ID="repInvoice" runat="server" OnItemCommand="repInvoice_ItemCommand1">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Date","{0: yyyy-MM-dd}") %></td>
                                <td><%# Eval("Staff") %></td>
                                <td><%# Eval("Drone_ID") %></td>
                                <td><%# Eval("Battery_Count") %></td>
                                <td><%# Eval("Customer_Name") %></td>
                                <td><%# Eval("Customer_Phone") %></td>
                                <td><%# Eval("Customer_Address") %></td>
                                <td><%# (!string.IsNullOrWhiteSpace(Eval("Pesticide").ToString())) ? Eval("Pesticide") : "-" %></td>
                                <td><%# (!string.IsNullOrWhiteSpace(Eval("Pesticide_Date").ToString())) ? Eval("Pesticide_Date") : "-" %></td>
                                <td><%# Eval("Remarks") %></td>
                                
                                <td>
                                    <asp:Button CssClass="btn btn-outline-secondary btn-sm" runat="server" Text="修改" CommandName="UpDateItem" CommandArgument='<%# Eval("Sid") %>'/>
                                </td>


                                <td>
                                    <asp:Button runat="server" Text="刪除" CssClass="btn btn-outline-danger btn-sm BtnDel" CommandName="DeleItem" CommandArgument='<%# Eval("Sid") %>' OnClientClick="javascript:return confirm('確定刪除?')" />
                                </td>

                               <%-- CommandArgument=' <%#Eval("id")+","+Eval("name") %>' ><%#Eval("name") %>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
            </table>
        </div>

    </div>
</asp:Content>
