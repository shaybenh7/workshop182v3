﻿<%@ Page Title="View Sale Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewInstantSale.aspx.cs" Inherits="WebServices.Views.Pages.viewInstantSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="sec-product-detail bg0 p-t-65 p-b-60">
        <div class="container">


            <div id="viewInstantSaleComponent" class="p-r-50 p-t-5 p-lr-0-lg">
            </div>
            <div class="col-md-6 col-lg-5 p-b-30">
                <div class="p-r-50 p-t-5 p-lr-0-lg">
                    <h4 class="mtext-105 cl2 js-name-detail p-b-14"></h4>


                    <!--  -->
                    <div class="p-t-33">



                        <div class="flex-w flex-r-m p-b-10">
                            <div class="size-204 flex-w flex-m respon6-next">
                                <div class="wrap-num-product flex-w m-r-20 m-tb-10">
                                    <div id="down-bar" class="btn-num-product-down cl8 hov-btn3 trans-04 flex-c-m">
                                        <i class="fs-16 zmdi zmdi-minus"></i>
                                    </div>

                                    <input class="mtext-104 cl3 txt-center num-product" type="number" id="num-product" name="num-product" value="1">

                                    <div id="up-bar" class="btn-num-product-up cl8 hov-btn3 trans-04 flex-c-m">
                                        <i class="fs-16 zmdi zmdi-plus"></i>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="p-t-33">
                            
                                
                                    <button id="submit" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04">
                                        Add to cart
                                    </button>
                        </div>
                    </div>


                </div>
            </div>



        </div>


    </section>
    <!--===============================================================================================-->
    <script src="vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/animsition/js/animsition.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/bootstrap/js/popper.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/select2/select2.min.js"></script>
    <script>
        $(".js-select2").each(function () {
            $(this).select2({
                minimumResultsForSearch: 20,
                dropdownParent: $(this).next('.dropDownSelect2')
            });
        })
    </script>
    <!--===============================================================================================-->
    <script src="vendor/daterangepicker/moment.min.js"></script>
    <script src="vendor/daterangepicker/daterangepicker.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/slick/slick.min.js"></script>
    <script src="js/slick-custom.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/parallax100/parallax100.js"></script>
    <script>
        $('.parallax100').parallax100();
    </script>
    <!--===============================================================================================-->
    <script src="vendor/MagnificPopup/jquery.magnific-popup.min.js"></script>
    <script>
        $('.gallery-lb').each(function () { // the containers for all your galleries
            $(this).magnificPopup({
                delegate: 'a', // the selector for gallery item
                type: 'image',
                gallery: {
                    enabled: true
                },
                mainClass: 'mfp-fade'
            });
        });
    </script>
    <!--===============================================================================================-->
    <script src="vendor/isotope/isotope.pkgd.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/sweetalert/sweetalert.min.js"></script>
    <script>
        $('.js-addwish-b2, .js-addwish-detail').on('click', function (e) {
            e.preventDefault();
        });

        $('.js-addwish-b2').each(function () {
            var nameProduct = $(this).parent().parent().find('.js-name-b2').html();
            $(this).on('click', function () {
                swal(nameProduct, "is added to wishlist !", "success");

                $(this).addClass('js-addedwish-b2');
                $(this).off('click');
            });
        });

        $('.js-addwish-detail').each(function () {
            var nameProduct = $(this).parent().parent().parent().find('.js-name-detail').html();

            $(this).on('click', function () {
                swal(nameProduct, "is added to wishlist !", "success");

                $(this).addClass('js-addedwish-detail');
                $(this).off('click');
            });
        });

        /*---------------------------------------------*/

        $('.js-addcart-detail').each(function () {
            var nameProduct = $(this).parent().parent().parent().parent().find('.js-name-detail').html();
            $(this).on('click', function () {
                swal(nameProduct, "is added to cart !", "success");
            });
        });

    </script>
    <!--===============================================================================================-->
    <script src="vendor/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script>
        $('.js-pscroll').each(function () {
            $(this).css('position', 'relative');
            $(this).css('overflow', 'hidden');
            var ps = new PerfectScrollbar(this, {
                wheelSpeed: 1,
                scrollingThreshold: 1000,
                wheelPropagation: false,
            });

            $(window).on('resize', function () {
                ps.update();
            })
        });
    </script>
    <!--===============================================================================================-->
    <script src="js/main.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var saleId = <%=ViewData["saleId"]%>;
            var mainDiv = document.getElementById('viewInstantSaleComponent');
            jQuery.ajax({
                type: "GET",
                url: baseUrl+"/api/user/viewSaleById?saleId=" + saleId,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    var i;
                    var pis = response["ProductInStoreId"];
                    var dueDate = response["DueDate"];
                    var quan = response["Amount"];
                    var dueDate = response["DueDate"];
                    var string = "";
                    string += "<h4 id=\"productName\" class=\"mtext-105 cl2 js-name-detail p-b-14\"> Product Name: </h4>";
                    string += "<span id=\"storeName\"  class=\"mtext-106 cl2\">Store Name: ";
                    string += "</span>";
                    string += " <br /> <br />";
                    string += "<span id=\"salePrice\" class=\"mtext-106 cl2\">Price: "
                    string += "</span><br />";
                    string += "<span id=\"salePriceAfterDiscount\" class=\"mtext-106 cl2\">Price after discount:  "
                    string += "</span><br />";
                    string += "<span class=\"mtext-106 cl2\">Due Date: " + dueDate;
                    string += "</span><br />";
                    string += "<span class=\"mtext-106 cl2\">Quantity: " + quan;
                    string += "</span><br />";
                    string += "<span id=\"policyOfInstantSale\" class=\"mtext-106 cl2\">Type of sale: Instant";

                    mainDiv.innerHTML += string;

                    var replaceAllOccurences = function (string, from, to) {
                        var newString = string.replace(from, to);
                        if (newString == string)
                            return newString;
                        return replaceAllOccurences(newString, from, to);
                    }

                     jQuery.ajax({
                                type: "GET",
                                url: baseUrl+"/api/store/showPolicy?saleId=" + saleId,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    var productNameElement = document.getElementById("policyOfInstantSale");
                                    var str = "</span><br /><span class=\"mtext-106 cl2\">POLICY: <br>" + replaceAllOccurences(response, "\n", "<br>") + "</span>";
                                     productNameElement.innerHTML += str;
                                },
                                error: function (response) {
                                    console.log("response");
                                }
                            });

                    
                    (function () {

                        $('#submit').click(function () {
                            jQuery.ajax({
                                type: "PUT",
                                url: baseUrl+"/api/store/addProductToCart?saleId=" + saleId + "&amount=" + document.getElementById("num-product").value,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                     alert(response);
                                },
                                error: function (response) {
                                    console.log("response");
                                }
                            });
                        });

                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl+"/api/store/getProductInStoreById?id=" + pis,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                console.log("fuck");
                                var productNameElement = document.getElementById("productName");
                                productNameElement.innerHTML += response["product"]["name"];

                                var storeNameElement = document.getElementById("storeName");
                                storeNameElement.innerHTML += response["store"]["name"];
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
                                console.log(response)
                                var salePriceElement = document.getElementById("salePriceAfterDiscount");
                                salePriceElement.innerHTML += response;
                            },
                            error: function (response) {
                                console.log(response);
                            }
                        });
                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl+"/api/store/checkPriceOfAProductBeforeDiscount?saleId=" + saleId, //add call to get price
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                console.log(response)
                                var salePriceElement = document.getElementById("salePrice");
                                salePriceElement.innerHTML += response;
                            },
                            error: function (response) {
                                console.log(response);
                            }
                        });
                    })();
                },
                error: function (response) {
                    console.log("fuck");
                    window.location.href = baseUrl+"/error";
                }
            });
        });
    </script>


</asp:Content>

