<%@ Page Title="View Store Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewStore.aspx.cs" Inherits="WebServices.Views.Pages.viewStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="sec-product-detail bg0 p-t-65 p-b-60">

        <div class="container">


                <div class="p-r-50 p-t-5 p-lr-0-lg">
                    <h4 id="store-name" class="mtext-105 cl2 js-name-detail p-b-14">Store Name:
                    </h4>
                    <span id="owners" class="mtext-106 cl2">Owners- 
                    </span>
                    <br />

                    <span id="managers" class="mtext-106 cl2">Managers- 
                    </span>
                    <br /><br /><br />

                    <div id="viewStore" class="row isotope-grid" style="position: relative;">
                </div>
                </div>
        </div>


    </section>


    <script type="text/javascript">
        function loadModal(saleId) {
            document.getElementById("modalContent").innerHTML = '<object type="text/html" data="'+baseUrl+"/viewInstantSale?saleId=' + saleId + ' ></object>';
        }
    </script>


    <script type="text/javascript">

        $(document).ready(function () {
            var storeId = <%=ViewData["storeId"]%>;
            var mainDiv = document.getElementById('viewStore');

            jQuery.ajax({
                type: "GET",
                url: baseUrl+"/api/store/viewSalesByStore?storeId=" + storeId,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    var i;

                    for (i = 0; i < response.length; i++) {

                        sale = response[i];
                        var pis = sale["ProductInStoreId"];
                        var saleId = sale["SaleId"];
                        typeOfSale = sale["TypeOfSale"]; //typeOfSale = sale[typeOfSale];
                        var string = "";
                        string += "<div class=\"col-sm-6 col-md-4 col-lg-3 p-b-35 isotope-item women\" style=\"width: 300px;padding: 17px;border-color: black;border-width: 1px;border-style: groove;margin-left:20px; margin-bottom: 20px;\"  >";
                        string += "<div class=\"block2\">";
                        string += "<div class=\"block2-pic hov-img0\">";
                        //string += "<img src=\"images/product-01New.jpg\" alt=\"IMG-PRODUCT\">";
                        string += "<a href=\""+baseUrl+"/viewInstantSale?saleId=" + saleId + "\" id=\"viewSaleFromPicture" + i + "\"";
                        string += "<img src=\"images/itamar.jpg\" alt=\"IMG-PRODUCT\">";
                        string += "</a>";
                        if (typeOfSale==1)
                            string += "<a href=\""+baseUrl+"/viewInstantSale?saleId="+saleId+"\" id=\"viewSaleFromInnerPicture"+i+"\" class=\"block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1\">Quick Buy</a>";
                        else
                            string += "<a href=\""+baseUrl+"/viewRaffleSale?saleId="+saleId+"\" id=\"viewSaleFromInnerPicture"+i+"\" class=\"block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1\">Quick Buy</a>";
                        string += "</div>";
                        string += "<div class=\"block2-txt flex-w flex-t p-t-14\">";
                        string += "<div class=\"block2-txt-child1 flex-col-l \">";
                        //string += "<a href=\"product-detail.html\" class=\"stext-104 cl4 hov-cl1 trans-04 js-name-b2 p-b-6\">";
                        string += "<b><div id=\"productName" + i + "\">Product Name: </div></b>"; // add sale name here to saleName1
                        //string += "</a>";
                        string += "<span class=\"stext-105 cl3\">";
                        string += "<div id=\"saleid" + i + "\">Sale id: " + saleId + " </div>"; // add sale id here to storeName
                         string += "<div id=\"ProductInStoreId" + i + "\">product in store id: "+pis+" </div>"; // add sale id here to storeName
                        string += "</span>";
                        string += "<span class=\"stext-105 cl3\">";
                        string += "<div id=\"salePrice" + i + "\">Sale price: </div>"; // add sale name here to storeName
                        string += "</span>";
                        string += "<span class=\"stext-105 cl3\">";
                        string += "<div id=\"storeName" + i + "\">Store Name: </div>"; // add sale name here to storeName
                        string += "</span>";
                        if(typeOfSale == 1)
                            string += "<span class=\"stext-105 cl3\">type of sale: instant sale</span>";
                        else
                            string += "<span class=\"stext-105 cl3\">type of sale: Raffle sale</span>";
                        string += "<span class=\"stext-105 cl3\" style=\"width: 100%;direction: rtl;\">";
                        string += "<div>";
                        if (typeOfSale == 1)
                        //    string += "<a href=\baseUrl+"/viewInstantSale?saleId="+saleId+"\" id=\"viewSale"+i+"\" class=\" stext-103  size-102 bg0 bor2 p-lr-15 trans-04 js-show-modal1\" style=\"display: contents;\">Buy</a>";
                            string += "<input type=\"button\" value=\"Buy\" id=\"viewSale"+i+"\" onclick=\"location.href=\'"+baseUrl+ "/viewInstantSale?saleId="+saleId+"\'\" class=\"flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer\" />";
                        else
                            string += "<input type=\"button\" value=\"Buy\" id=\"viewSale"+i+"\" onclick=\"location.href=\'" + baseUrl + "/viewRaffleSale?saleId="+saleId+"\'\" class=\"flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer\" />";
                            //string += "<a href=\baseUrl+"/viewRaffleSale?saleId="+saleId+"\" id=\"viewSale"+i+"\" class=\" stext-103  size-102 bg0 bor2 p-lr-15 trans-04 js-show-modal1\"  style=\"display: contents;\">Buy</a>";
                        string += "</div>";
                        string += "</span>";
                        string += "</div>";
                        string += "</div>";
                        string += "</div>";
                        string += "</div>";
                        mainDiv.innerHTML += string;
                        (function (i) {
                            jQuery.ajax({
                                type: "GET",
                                url: baseUrl+"/api/store/getProductInStoreById?id=" + pis,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    var productNameElement = document.getElementById("productName" + i);
                                    productNameElement.innerHTML += response["product"]["name"];

                                    var storeNameElement = document.getElementById("storeName" + i);
                                    storeNameElement.innerHTML += response["store"]["name"];
                                    if (i == 0) {
                                        var storeNameHeader = document.getElementById("store-name");
                                        storeNameHeader.innerHTML += response["store"]["name"];
                                    }
                                },
                                error: function (response) {
                                    console.log(response);
                                }
                            });

                            jQuery.ajax({
                                type: "GET",
                                url: baseUrl+"/api/store/checkPriceOfAProduct?saleId=" + saleId, //add call to get price
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    var salePriceElement = document.getElementById("salePrice" + i);
                                    salePriceElement.innerHTML += response;
                                },
                                error: function (response) {
                                    console.log(response);
                                }
                            });

                            

                        })(i);
                    }
                    (function () {
                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl+"/api/store/getOwners?storeId=" + storeId, //add call to get price
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                console.log("here");
                                console.log(response[0]["user"]["userName"]);
                                var owners = document.getElementById("owners");
                                var j;
                                for (j = 0; j < response.length - 1; j++) {
                                    owners.innerHTML += response[j]["user"]["userName"] + ", ";
                                }
                                owners.innerHTML += response[j]["user"]["userName"];

                            },
                            error: function (response) {
                                console.log(response);
                            }
                        });
                    })();

                    (function () {
                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl+"/api/store/getManagers?storeId=" + storeId, //add call to get price
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                console.log("here");
                                console.log(response[0]["user"]["userName"]);
                                var owners = document.getElementById("managers");
                                var j;
                                for (j = 0; j < response.length - 1; j++) {
                                    owners.innerHTML += response[j]["user"]["userName"] + ", ";
                                }
                                owners.innerHTML += response[j]["user"]["userName"];

                            },
                            error: function (response) {
                                console.log(response);
                            }
                        });
                    })();

                },
                error: function (response) {
                    console.log(response);
                    window.location.href = baseUrl+"/error";
                }
            });
        });


    </script>
</asp:Content>
