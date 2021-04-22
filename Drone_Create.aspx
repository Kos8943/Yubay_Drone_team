﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Drone_Create.aspx.cs" Inherits="Yubay_Drone_team.Drone_Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Number{
             width:50px;
            text-align:left;
        }
         .Manufacturer{
             width:50px;
            text-align:left;
        }
          .Weight{
             width:50px;
            text-align:left;
        }
          .Deactive{
             width:50px;
            text-align:left;
        }
            .Person{
             width:50px;
            text-align:left;
        }


            .Btn_Create{

                margin-right:80px;
                 margin-left:40px;

               


            }

          

    </style>
    

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <%--用Div作為輸出欄位,span為標題--%>
    <div style="border:4px #000 solid;">
       <div style="text-align:center;"><h3>新增無人機</h3>
    <div>
        <div>
            <span>編號</span>
            <asp:TextBox ID="Text_Number" runat="server"></asp:TextBox>
        </div>
        <div>
            <span>製造商</span>
            <asp:TextBox ID="Text_Manufacturer" runat="server"></asp:TextBox>
        </div>
    <div>
            <span>最大起飛重量</span>
            <asp:TextBox ID="Text_Weight" runat="server"></asp:TextBox>
    </div>
    <div>
            <span>使用狀態</span>
    
    
        <asp:DropDownList ID="DropDownList_Status" runat="server">
            <asp:ListItem Text ="啟用" Value="啟用"></asp:ListItem>
            <asp:ListItem Text ="停用" Value="停用"></asp:ListItem>
            <asp:ListItem Text ="故障" Value="故障"></asp:ListItem>


            
        </asp:DropDownList>

    </div>
    <div>
        <span>停用原因</span>
        <asp:TextBox ID="Text_Deactive" runat="server"></asp:TextBox>
    </div>
    <div>
        <span>負責人員</span>
        <asp:DropDownList ID="DropDownList_Operator" runat="server">
            <asp:ListItem Text ="Stella" Value="Stella"></asp:ListItem>
            <asp:ListItem Text ="Tom" Value="Tom"></asp:ListItem>
            <asp:ListItem Text ="Sandy" Value="Sandy"></asp:ListItem>

        </asp:DropDownList>

        
    </div>
    </div>
</div>

    <%--新增/修改按鈕--%>
        
         
    <asp:Button ID="Btn_Create" CssClass="Btn_Create" runat="server" Text="建立" OnClick="Btn_Create_Click" />

     <asp:Button ID="Btn_Cancel" runat="server" Text="取消" OnClick="Btn_Cancel_Click" /><br/><br />

   <asp:Literal ID="Literal1" runat="server" Text="新增成功!" Visible="false"></asp:Literal>
</div>
   

</asp:Content>
