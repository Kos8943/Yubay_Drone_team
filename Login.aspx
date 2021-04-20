<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Yubay_Drone_team.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="JQuery/jquery-3.6.0.min.js"></script>
    <style>
        html, body, #form1 {
            margin: 0;
            padding: 0;
            height: 100%;
            background-color: rgb(17,120,216);
            font-family: 'Segoe UI',Arial,'Microsoft Jhenghei',sans-serif;
        }

        .LoginArea {
            width: 320px;
            height: 236px;
            margin: auto;
            padding-top: 16px;
            background-color: rgba(89,167,225,.6);
            border-radius: 5px;
        }

        .tittle {
            margin: auto;
            text-align: center;
            font-size: 30px;
            font-weight: bolder;
            color: rgb(233,233,233);
        }

        .inputAreaStyle {
            padding-top: 16px;
            padding-bottom: 16px;
        }

        .UserImg {
            width: 28px;
            height: 28px;
            margin-right: 8px;
        }

        .InputSize {
            width: 246px;
            height: 28px;
            border: 0px;
            background-color: rgba(255,255,255,0);
            font-family: 'Segoe UI',Arial,'Microsoft Jhenghei',sans-serif;
        }

            .InputSize:focus {
                outline: none;
            }

        .Line {
            width: 100%;
            border-bottom: 1px solid rgb(250,250,250);
            margin-bottom: 13px;
            opacity: .3;
        }

        .BtnLoginArea{
            text-align:center;
        }

        .btnLogin {
            width: 278px;
            height: 38px;
            font-family: 'Segoe UI',Arial,'Microsoft Jhenghei',sans-serif;
            background-color: rgb(5,139,233);
            border: 1px solid rgba(0, 0, 0,.1);
            border-radius: 5px;
            font-size: 20px;
            color: rgba(253,253,253,.8);
            font-weight: bold;
            outline: none;
        }

        .errorMsg {
            margin: 0;
            margin-top: 5px;
            color: white;
            font-size: 18px;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="">
        <div style="width: 100%;margin-top:247px;">
            <div class="tittle">YUBAY</div>
            <div class="LoginArea">
                <div class="d-flex justify-content-center inputAreaStyle">
                    <img src="Imgs/iconfinder_User_5431752.svg" class="UserImg" />
                    <asp:TextBox ID="TextAccount" runat="server" CssClass="InputSize"></asp:TextBox>
                </div>
                <div class="Line"></div>

                <div class="d-flex justify-content-center inputAreaStyle">
                    <img src="Imgs/iconfinder_Safety01_928417.svg" class="UserImg" />
                    <asp:TextBox ID="TextPassword" runat="server" CssClass="InputSize"></asp:TextBox>
                </div>
                <div class="Line"></div>
                <div class="BtnLoginArea">
                    <asp:Button ID="btnLogin" runat="server" Text="登入" CssClass="btnLogin" />
                    <asp:Label ID="ltErrorMsg" class="errorMsg" runat="server" Text="" Visible="false"></asp:Label>
                </div>

            </div>
        </div>

    </form>
</body>
</html>
