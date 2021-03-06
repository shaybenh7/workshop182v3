﻿<%@ Page Title="View Search Results" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="searchResults.aspx.cs" Inherits="WebServices.Views.Pages.searchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<div class="col-lg-10 col-xl-7 m-lr-auto m-b-50" style="max-width: 80%; flex: 0 0 80%;">
			<div class="bg0 m-t-23 p-b-140" style="margin-left: auto; margin-right: auto; margin-top: 45px; max-width: 100%;">
				<div class="container">
					<div id="allSalesComponent" class="row isotope-grid" style="position: relative;">
					</div>
				</div>

			</div>

		</div>
		<div class="col-sm-10 col-lg-7 col-xl-5 m-lr-auto m-b-50" style="max-width: 20%; flex: 0 0 20%;">
			<div class="bor10 p-lr-40 p-t-30 p-b-40 m-l-63 m-r-40 m-lr-0-xl p-lr-15-sm" style="margin-top: 46px; margin-right: 91px; margin-left: -27px;">
				<div class="flex-w flex-t bor12 p-t-15 p-b-30">
					Categories:
                    <div id="categoriesContainer" style="display: table; width: 100%;"></div>
					Prices Range:
                    <div id="priceRangeContainer">
						<div class="wrap-input1 w-full p-b-4">
							<input class="input1 bg-none plh1 stext-107 cl7" type="text" id="minPrice" placeholder="Min Price">
							<div class="focus-input1 trans-04"></div>
						</div>
						<div class="wrap-input1 w-full p-b-4">
							<input class="input1 bg-none plh1 stext-107 cl7" type="text" id="maxPrice" placeholder="Max Price">
							<div class="focus-input1 trans-04"></div>
						</div>

					</div>

				</div>

				<input type="button" value="Apply" id="applyButton" onclick="applyFilter()" class="flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer" />
			</div>
		</div>
	</div>

	<script type="text/javascript">
		$(document).ready(function () {
			$("#AllProductsMenuButton").addClass("active-menu")
		});
	</script>

	<script type="text/javascript">
		function loadModal(saleId) {
			document.getElementById("modalContent").innerHTML = '<object type="text/html" data=' + baseUrl + '"/viewInstantSale?saleId=' + saleId + ' ></object>';
		}
	</script>


	<script type="text/javascript">
		var searchQuery = "<%=ViewData["query"]%>";
		var categories = [];
		var i;
		$(document).ready(function () {
			var mainDiv = document.getElementById('allSalesComponent');
			var categoriesContainer = document.getElementById('categoriesContainer');
			jQuery.ajax({
				type: "GET",
				url: baseUrl + "/api/sell/search?query=" + searchQuery,
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					console.log(response);
					for (i = 0; i < response.length; i++) {
						sale = response[i];
						var pis = sale["ProductInStoreId"];
						var saleId = sale["SaleId"];
						typeOfSale = sale["TypeOfSale"]; //typeOfSale = sale[typeOfSale];
						var string = "";
						string += "<div class=\"col-sm-6 col-md-4 col-lg-3 p-b-35 isotope-item women\" id=\"saleElement" + i + "\" style=\"display: block; width: 300px;padding: 17px;border-color: black;border-width: 1px;border-style: groove;margin-left:20px; margin-bottom: 20px;\"  >";
						string += "<div class=\"block2\">";
						string += "<div class=\"block2-pic hov-img0\">";
						//string += "<img src=\"images/product-01New.jpg\" alt=\"IMG-PRODUCT\">";
						string += "<a href=\"" + baseUrl + "/viewInstantSale?saleId=" + saleId + "\" id=\"viewSaleFromPicture" + i + "\"";
						string += "<img src=\"images/itamar.jpg\" alt=\"IMG-PRODUCT\">";
						string += "</a>";
						if (typeOfSale == 1)
							string += "<a href=\"" + baseUrl + "/viewInstantSale?saleId=" + saleId + "\" id=\"viewSaleFromInnerPicture" + i + "\" class=\"block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1\">Quick Buy</a>";
						else
							string += "<a href=\"" + baseUrl + "/viewRaffleSale?saleId=" + saleId + "\" id=\"viewSaleFromInnerPicture" + i + "\" class=\"block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1\">Quick Buy</a>";
						string += "</div>";
						string += "<div class=\"block2-txt flex-w flex-t p-t-14\">";
						string += "<div class=\"block2-txt-child1 flex-col-l \">";
						//string += "<a href=\"product-detail.html\" class=\"stext-104 cl4 hov-cl1 trans-04 js-name-b2 p-b-6\">";
						string += "<b><div id=\"productName" + i + "\">Product Name: </div></b>"; // add sale name here to saleName1
						//string += "</a>";
						string += "<span class=\"stext-105 cl3\">";
						string += "<div id=\"saleid" + i + "\">Sale id: " + saleId + " </div>"; // add sale id here to storeName
						string += "<div id=\"ProductInStoreId" + i + "\">product in store id: " + pis + " </div>"; // add sale id here to storeName
						string += "</span>";
						string += "<span class=\"stext-105 cl3\">";
						string += "<div id=\"category" + i + "\">category: </div>"; // add sale name here to storeName
						string += "</span>";
						string += "<span class=\"stext-105 cl3\">";
						string += "<div id=\"salePrice" + i + "\">Sale price: </div>"; // add sale name here to storeName
						string += "</span>";
						string += "<span class=\"stext-105 cl3\">";
						string += "<div id=\"storeName" + i + "\">Store Name: </div>"; // add sale name here to storeName
						string += "</span>";
						if (typeOfSale == 1)
							string += "<span class=\"stext-105 cl3\">type of sale: instant sale</span>";
						else
							string += "<span class=\"stext-105 cl3\">type of sale: Raffle sale</span>";
						string += "<span class=\"stext-105 cl3\" style=\"width: 100%;direction: rtl;\">";
						string += "<div>";
						if (typeOfSale == 1)
							//    string += "<a href=\"+baseUrl+"/viewInstantSale?saleId="+saleId+"\" id=\"viewSale"+i+"\" class=\" stext-103  size-102 bg0 bor2 p-lr-15 trans-04 js-show-modal1\" style=\"display: contents;\">Buy</a>";
							string += "<input type=\"button\" value=\"Buy\" id=\"viewSale" + i + "\" onclick=\"window.location.href='" + baseUrl + "/viewInstantSale?saleId=" + saleId + "\'\" class=\"flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer\" />";
						else
							string += "<input type=\"button\" value=\"Buy\" id=\"viewSale" + i + "\" onclick=\"window.location.href='" + baseUrl + "/viewRaffleSale?saleId=" + saleId + "\'\" class=\"flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer\" />";
						//string += "<a href=\"+baseUrl+"/viewRaffleSale?saleId="+saleId+"\" id=\"viewSale"+i+"\" class=\" stext-103  size-102 bg0 bor2 p-lr-15 trans-04 js-show-modal1\"  style=\"display: contents;\">Buy</a>";
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
								url: baseUrl + "/api/store/getProductInStoreById?id=" + pis,
								contentType: "application/json; charset=utf-8",
								dataType: "json",
								success: function (response) {
									var category = response["category"];
									if (categories.indexOf(category) == -1) {
										categories.push(category);
										var categoryString = "";
										categoryString += "<div style=\"display: flex; margin-bottom: 10px;\">";
										categoryString += "<input type=\"checkbox\" id=\"category-" + category + "\" style=\"margin-top: 5px; margin-right: 10px;\" checked>";
										categoryString += category;
										categoryString += "</div>";
										categoriesContainer.innerHTML += categoryString;
									}
									
									var productNameElement = document.getElementById("productName" + i);
									productNameElement.innerHTML += response["product"]["name"];

									var productNameElement = document.getElementById("category" + i);
									productNameElement.innerHTML += category;

									var storeNameElement = document.getElementById("storeName" + i);
									storeNameElement.innerHTML += response["store"]["name"];
								},
								error: function (response) {
                                    alert("Lost DB connection");
                                    window.location.href = baseUrl+"/index";
                                }
							});

							jQuery.ajax({
								type: "GET",
								url: baseUrl + "/api/store/checkPriceOfAProduct?saleId=" + saleId, //add call to get price
								contentType: "application/json; charset=utf-8",
								dataType: "json",
								success: function (response) {
									var salePriceElement = document.getElementById("salePrice" + i);
									salePriceElement.innerHTML += response.toFixed(2);
								},
								error: function (response) {
                                    alert("Lost DB connection");
                                    window.location.href = baseUrl+"/index";
                                }
							});
						})(i);
					}
				},
				error: function (response) {
					console.log(response);
					window.location.href = baseUrl + "/error";
				}
			});
		});

		function applyFilter() {
			var minPrice = $("#minPrice").val();
			if (minPrice == "")
				minePrice = 0;
			var maxPrice = $("#maxPrice").val();
			if (maxPrice == "")
				maxPrice = 2147483646;
			var category = $("#category").val();
			var j;
			for (j = 0; j < i; j++) {
				var price = document.getElementById("salePrice" + j).innerText.replace("Sale price: ", "");
				var category = document.getElementById("category" + j).innerText.replace("category: ", "");
				price = parseFloat(price);
				if (price <= maxPrice && price >= minPrice && $("#category-" + category)[0].checked)
					showElement("saleElement" + j);
				else hideElement("saleElement" + j);
			}
		}
		function showElement(id) {
			document.getElementById(id).style.display = "block";
		}
		function hideElement(id) {
			document.getElementById(id).style.display = "none";
		}

	</script>

</asp:Content>

