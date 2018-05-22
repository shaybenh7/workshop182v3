<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebServices.Views.Pages.login" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="bg0 p-t-75 p-b-32" style="margin-left: auto; margin-right: auto; max-width: 100%;">

        <div class="container" style="max-width: 100%;">
            <div class="row centerElem" style="max-width: 100%;">

                <div class="col-sm-6 col-lg-3 p-b-50 centerElem" style="max-width: 100%;">
                    <h4 class="stext-301 cl0 p-b-30">Newsletter
                    </h4>

                    <div class="wrap-input1 w-full p-b-4">
                        <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="username" id="username" placeholder="Anatoly">
                        <div class="focus-input1 trans-04"></div>
                    </div>

                    <div class="wrap-input1 w-full p-b-4">
                        <input class="input1 bg-none plh1 stext-107 cl7" type="password" name="password" id="password" placeholder="123456">
                        <div class="focus-input1 trans-04"></div>
                    </div>
                    <small id="loginAlert" class="form-text text-muted text-Alert"></small>
                    <div class="p-t-18">
                        <input type="button" class="flex-c-m stext-101 cl0 size-103 bg1 bor1 hov-btn2 p-lr-15 trans-04" name="btnLogin" id="btnLogin" value="login" />

                    </div>
                </div>
            </div>


        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#btnLogin").click(function () {

                username = $("#username").val();
                pass = $("#password").val();

                jQuery.ajax({
                    type: "GET",
                    url: baseUrl+"/api/user/login?username=" + username + "&password=" + pass,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response);
                        //console.log(response[1]);
                        if (response == "user successfuly logged in") {
                            //document.cookie = "HashCode=" + response[1]; //saves the hash code as a cookie
                            window.location.href = baseUrl+"/";
                        }
                        else {
                            $("#loginAlert").html('Failure - ' + response);
                        }
                    },
                    error: function (response) {
                        console.log(response);
                        window.location.href = baseUrl+"/error";
                    }
                });
            });
        });

    </script>

</asp:Content>


