<%@ Page Title="View Sale Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewRaffleSale.aspx.cs" Inherits="WebServices.Views.Pages.viewRaffleSale" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="sec-product-detail bg0 p-t-65 p-b-60">
        <div class="container">
            <div class="row">


                <div class="col-md-6 col-lg-5 p-b-30">
                <div id="viewRaffleSaleComponent" class="p-r-50 p-t-5 p-lr-0-lg">
                    
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
		$(".js-select2").each(function(){
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
		$('.gallery-lb').each(function() { // the containers for all your galleries
			$(this).magnificPopup({
		        delegate: 'a', // the selector for gallery item
		        type: 'image',
		        gallery: {
		        	enabled:true
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
		$('.js-addwish-b2, .js-addwish-detail').on('click', function(e){
			e.preventDefault();
		});

		$('.js-addwish-b2').each(function(){
			var nameProduct = $(this).parent().parent().find('.js-name-b2').html();
			$(this).on('click', function(){
				swal(nameProduct, "is added to wishlist !", "success");

				$(this).addClass('js-addedwish-b2');
				$(this).off('click');
			});
		});

		$('.js-addwish-detail').each(function(){
			var nameProduct = $(this).parent().parent().parent().find('.js-name-detail').html();

			$(this).on('click', function(){
				swal(nameProduct, "is added to wishlist !", "success");

				$(this).addClass('js-addedwish-detail');
				$(this).off('click');
			});
		});

		/*---------------------------------------------*/

		$('.js-addcart-detail').each(function(){
			var nameProduct = $(this).parent().parent().parent().parent().find('.js-name-detail').html();
			$(this).on('click', function(){
				swal(nameProduct, "is added to cart !", "success");
			});
		});
	
	</script>
<!--===============================================================================================-->
	<script src="vendor/perfect-scrollbar/perfect-scrollbar.min.js"></script>
	<script>
		$('.js-pscroll').each(function(){
			$(this).css('position','relative');
			$(this).css('overflow','hidden');
			var ps = new PerfectScrollbar(this, {
				wheelSpeed: 1,
				scrollingThreshold: 1000,
				wheelPropagation: false,
			});

			$(window).on('resize', function(){
				ps.update();
			})
		});
	</script>
<!--===============================================================================================-->
	<script src="js/main.js"></script>

    <script type ="text/javascript">
        $(document).ready(function () {
            var saleId = <%=ViewData["saleId"]%>;
            var mainDiv = document.getElementById('viewRaffleSaleComponent');
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
                    var string = "";
                    string += "<h4 id=\"productName\" class=\"mtext-105 cl2 js-name-detail p-b-14\"> Product Name: </h4>";
                    string += "<span id=\"storeName\"  class=\"mtext-106 cl2\">Store Name: ";
                    string += "</span>";
                    string += " <br /> <br />";
                    string += "<span id=\"salePrice\" class=\"mtext-106 cl2\">Price: "
                    string += "</span><br />";
                    string += "<span id=\"maxOffer\" class=\"mtext-106 cl2\">Max offer: "
                    string += "</span><br />";
                    string += "<span class=\"mtext-106 cl2\">Due Date: " + dueDate;
                    string += "</span><br />";
                    string += "<span class=\"mtext-106 cl2\">Quantity: " + quan;
                    string += "</span><br />";
                    string += "<span class=\"mtext-106 cl2\">Type of sale: Raffle";
                    string += "</span><br />";
                    string += "<span class=\"mtext-106 cl2\" id=\"policyOfRaffleSale\">POLICY:<br> </span><br />";
                    string += "<div class=\"size-204 flex-w flex-m respon6-next\">";
                    string += "<div class=\"wrap-input1 w-full p-b-4\">";
                    string += "<input class=\"input1 bg-none plh1 stext-107 cl7\" type=\"text\" name=\"myOffer\" id=\"myOffer\" placeholder=\"Enter your offer\">";
                    string += "<div class=\"focus-input1 trans-04\"></div>"
                    string += "</div> </div>";
                    string += "<!--  -->";
                    string += "<div class=\"p-t-33\">";
                    string += "<div class=\"flex-w flex-r-m p-b-10\">";
                    string += "<div id = \"tryOffer\" class=\"size-204 flex-w flex-m respon6-next\">";
                    string += "<button id = \"submit\" class=\"flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail\"> Add to cart";
                    string += "</button>";

                    string += "</div> </div> </div>";


                    var replaceAllOccurences = function (string, from, to) {
                        var newString = string.replace(from, to);
                        if (newString == string)
                            return newString;
                        return replaceAllOccurences(newString, from, to);
                    }


                    mainDiv.innerHTML += string;

                    jQuery.ajax({
                                type: "GET",
                                url: baseUrl+"/api/store/showPolicy?saleId=" + saleId,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    var productNameElement = document.getElementById("policyOfRaffleSale");
                                    var str = replaceAllOccurences(response, "\n", "<br>") ;
                                     productNameElement.innerHTML += str;
                                },
                                error: function (response) {
                                    console.log("response");
                                }
                            });
                    (function () {
                        
                            $('#submit').click(function () {
                                jQuery.ajax({
                                    type: "GET",
                                    url: baseUrl+"/api/store/addRaffleProductToCart?saleId=" + saleId + "&offer=" + document.getElementById("myOffer").value,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        alert(response);
                                        console.log(response);
                                    },
                                    error: function (response) {
                                        console.log("responseeee");
                                    }
                                });
                            });
                        
                            jQuery.ajax({
                                type: "GET",
                                url: baseUrl+"/api/store/getProductInStoreById?id=" + pis,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
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
                                url: baseUrl+"/api/store/checkRaffleBids?saleId=" + saleId,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    
                                    var productNameElement = document.getElementById("maxOffer");
                                    productNameElement.innerHTML += response.toFixed(2);;
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

