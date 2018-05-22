<%@ Page Title="Register Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="WebServices.Views.Pages.register" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="bg0 p-t-75 p-b-32" style="margin-left: auto; margin-right: auto; max-width: 100%;">

        <div class="container" style="max-width: 100%;">
            <div class="row centerElem" style="max-width: 100%;">

                <div class="col-sm-6 col-lg-3 p-b-50 centerElem" style="max-width: 100%;">
                    <h4 class="stext-301 cl0 p-b-30">Newsletter
                    </h4>

                    <div class="wrap-input1 w-full p-b-4">
                        <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="username" id="username" placeholder="Enter username">
                        <div class="focus-input1 trans-04"></div>
                    </div>

                    <div class="wrap-input1 w-full p-b-4">
                        <input class="input1 bg-none plh1 stext-107 cl7" type="password" name="password1" id="password1" placeholder="Enter your password">
                        <div class="focus-input1 trans-04"></div>
                    </div>

                    <div class="wrap-input1 w-full p-b-4">
                        <input class="input1 bg-none plh1 stext-107 cl7" type="password" name="password2" id="password2" placeholder="Confirm password">
                        <div class="focus-input1 trans-04"></div>
                    </div>
                   <small id="registerAlert" class="form-text text-muted text-Alert"></small>
                    <div class="p-t-18">
                        <input type="button" class="flex-c-m stext-101 cl0 size-103 bg1 bor1 hov-btn2 p-lr-15 trans-04" name="btnRegister" id="btnRegister" value="Register" />
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">
            $(document).ready(function () {
	    $("#btnRegister").click(function(){
		
		    username=$("#username").val();
            pass = $("#password1").val();
            pass2 = $("#password2").val();
            if (pass != pass2) {
                $("#registerAlert").html('Failure - passwords does not match');
            }
            else {
                jQuery.ajax({
                    type: "GET",
                    url: baseUrl+"/api/user/register?username=" + username + "&password=" + pass,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == "user successfuly added") {
                            alert("User successfuly added");
                            window.location.href = baseUrl+"/";
                        }
                        else {
                            $("#registerAlert").html('Failure - ' + response);
                        }
                    },
                    error: function (response) {
                        window.location.href = baseUrl+"/error";
                    }
                });
            }
	    });
    });

</script>

</asp:Content>

