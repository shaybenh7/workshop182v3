<%@ Page Title="Stores Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllStores.aspx.cs" Inherits="WebServices.Views.Pages.AllStores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="bg0 m-t-23 p-b-140" style="margin-left: auto; margin-right: auto; margin-top: 45px; max-width: 100%;">
        <div class="container">
            <div id="allStoresComponent" class="row isotope-grid" style="position: relative;">
            </div>
        </div>
    </div>
     <script type="text/javascript">
        $(document).ready(function () {
            $("#AllStoresMenuButton").addClass("active-menu")
        });
    </script>
    <script type="text/javascript">
        
        $(document).ready(function () {
            var mainDiv = document.getElementById('allStoresComponent');
            jQuery.ajax({
                type: "GET",
                url: baseUrl+"/api/store/getAllStores",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var i;
                    for (i = 0; i < response.length; i++) {
                        store = response[i];
                        var storeId = store["storeId"];
                            var storeName = store["name"];
                            var ownerName = store["storeCreator"]["userName"];
                        var string = "";
                        string += "<div class=\"col-sm-6 col-md-4 col-lg-3 p-b-35 isotope-item women\" >";
                        string += "<div class=\"block2\">";
                        string += "<div class=\"block2-pic hov-img0\">";
                        string += "<a href=\""+baseUrl+"/viewStore?storeId="+storeId+"\" class=\"block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1\" >View Store</a>";
                        string += "</div>";
                        string += "<div class=\"block2-txt flex-w flex-t p-t-14\" style=\"width: 270px;padding: 17px;border-color: black;border-width: 1px;border-style: groove;margin-left:20px; margin-bottom: 20px;\">";
                        string += "<div class=\"block2-txt-child1 flex-col-l \">";
                        string += "<a href=\""+baseUrl+"/viewStore?storeId="+storeId+"\" class=\"stext-104 cl4 hov-cl1 trans-04 js-name-b2 p-b-6\">";
                        string += "<div id=\"storeName" + i + "\">Store Name: " + storeName + "</div>"; // add sale name here to saleName1
                            string += "</a>";
                        string += "<span class=\"stext-105 cl3\">";
                        string += "<div id=\"ownerName" + i + "\">Store Creator: " + ownerName + "</div>"; // add sale name here to storeName
                            string += "</span>";
                        string += "</div>";
                        string += "</div>";
                        string += "</div>";
                        string += "</div>";
                        mainDiv.innerHTML += string;
                    }
                },
                error: function (response) {
                    console.log(response);
                    window.location.href = baseUrl+"/error";
                }
            });
        });
        
        
    </script>
    
</asp:Content>



