$(document).ready(function () {
    var mainDiv = document.getElementById('shoppingCart');
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/sell/getShoppingCartBeforeCheckout",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            console.log("here");
            var i;
            var totalPrice=0;
            for (i = 0; i < response.length; i++) {
                element = response[i];
                var amount = element["Amount"];
                var saleId = element["SaleId"];
                var totalBeforeDiscount = element["Price"];
                var totalAfterDiscount = element["PriceAfterDiscount"];
                var price = totalAfterDiscount / amount;
                var priceBeforeDiscount = totalBeforeDiscount / amount;
                if (element["Offer"] !== 0) {
                    price = element["Offer"];
                }
                if (element["Offer"] !== 0) {
                    totalAfterDiscount = element["Offer"] * amount;
                }
                
                totalPrice += totalAfterDiscount;
                var string = "";
                string += "<tr class=\"table_row\">";
                string += "<td class=\"column-1\" >";
                //string += "<div class=\"how-itemcart1\">";
                string += "<input type=\"image\" onclick=\"RemoveProductFromCart(" + saleId +")\" src=\"images/removee.png\" id=\"remove" + i + "\" width=\"40\" height=\"40\">";
                //string += "</div>";
                string += "</td>";
                string += "<td class=\"column-1\" id=\"productName" + i + "\"></td>";
                string += "<td class=\"column-2\" id=\"InitialpriceModal" + i + "\">" + priceBeforeDiscount.toFixed(2) + "</td>";
                string += "<td class=\"column-3\" id=\"FinalpriceModal" + i + "\">" + price.toFixed(2) + "</td>";
                string += "<td class=\"column-4\" id=\"quantity" + i + "\">" + amount + "</td>";
                string += "<td class=\"column-5\" id=\"total" + i + "\">" + totalAfterDiscount.toFixed(2) + "</td>";
                string += "</tr>";
                mainDiv.innerHTML += string;

                (function (i, saleId) {
                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl+"/api/user/viewSaleById?saleId=" + saleId,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                            jQuery.ajax({
                                type: "GET",
                                url: baseUrl+"/api/store/getProductInStoreById?id=" + response["ProductInStoreId"],
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    var productNameElement = document.getElementById("productName" + i);
                                    productNameElement.innerHTML += response["product"]["name"];
                                },
                                error: function (response) {
                                    console.log(response);
                                }
                            });

                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });

                })(i, saleId);
            }

            var totalPriceDive = document.getElementById("total-price");
            totalPriceDive.innerHTML += totalPrice.toFixed(2);
            

        },
        error: function (response) {
            console.log(response);
            window.location.href = baseUrl+"/error";
        }
    });
});

var RemoveProductFromCart = function (saleId) {
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/sell/removeFromCart?saleId=" + saleId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("response");

            console.log(response);
        },
        error: function (response) {
            console.log("responseeee");
            window.location.href = baseUrl+"/error";
        }
    });
}


var checkoutFunc2 =function() {
    var country = $("#country").val();
    var address = $("#address").val();;
    var creditcard = $("#creditCard").val();
    //now need to add stuff to the modal
    var mainDivModal = document.getElementById('shoppingCartModal');
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/sell/checkout?country=" + country + "&address=" + address + "&creditcard=" + creditcard,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "Error: user error!" || response == "Error: country and address fields cannot be empty!")
                alert("Error: All fields must be filled!");
            else if (response.includes("Error: item with sale Id") || response.includes("does not fulfill its buying policy!")  ) {
                alert(response);
            }
            else {
                var i;
                
                for (i = 0; i < response.length; i++) {
                    element = response[i];
                    var amount = element["Amount"];
                    var saleId = element["SaleId"];
                    var totalBeforeDiscount = element["Price"];
                    var totalAfterDiscount = element["PriceAfterDiscount"];
                    var price = totalAfterDiscount / amount;
                    var priceBeforeDiscount = totalBeforeDiscount / amount;
                    if (element["Offer"] != 0) {
                        price = element["Offer"];
                    }
                    if (element["Offer"] != 0) {
                        totalAfterDiscount = element["Offer"] * amount;
                    }
                    var string = "";
                    string += "<tr class=\"table_row\">";
                    string += "<td class=\"column-1\" >";
                    //string += "<div class=\"how-itemcart1\">";
                    string += "<input type=\"image\" onclick=\"RemoveProductFromCart(" + saleId + ")\" src=\"images/removee.png\" id=\"remove" + i + "\" width=\"40\" height=\"40\">";
                    //string += "</div>";
                    string += "</td>";
                    string += "<td class=\"column-2\" id=\"InitialpriceModal" + i + "\">" + priceBeforeDiscount.toFixed(2) + "</td>";
                    string += "<td class=\"column-3\" id=\"FinalpriceModal" + i + "\">" + price.toFixed(2) + "</td>";
                    string += "<td class=\"column-4\" id=\"quantityModal" + i + "\">" + amount + "</td>";
                    string += "<td class=\"column-5\" id=\"totalModal" + i + "\">" + totalAfterDiscount.toFixed(2) + "</td>";
                    string += "</tr>";
                    mainDivModal.innerHTML += string;

                    (function (i, saleId) {
                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl+"/api/user/viewSaleById?saleId=" + saleId,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {

                                jQuery.ajax({
                                    type: "GET",
                                    url: baseUrl+"/api/store/getProductInStoreById?id=" + response["ProductInStoreId"],
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        var productNameElement = document.getElementById("productName" + i);
                                        productNameElement.innerHTML += response["product"]["name"];
                                    },
                                    error: function (response) {
                                        console.log(response);
                                    }
                                });

                            },
                            error: function (response) {
                                console.log(response);
                            }
                        });

                    })(i, saleId);

                }
                var element = document.getElementById("afterCheckoutModal");
                element.classList.add("show-modal1");
            }
        },
        error: function (response) {
            console.log("error");
            console.log(response);
            //window.location.href = baseUrl+"/error";
        }
    });

}


function applyCoupon() {
    var mainDivModal = document.getElementById('shoppingCartModal');

    var coupon = $("#coupnnNameId").val();
    var country = $("#country").val();
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/sell/applyCoupon?couponId=" + coupon +
            "&country=" + country,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            mainDivModal.innerHTML = "<tr class=\"table_head\">" +
                "<th class=\"column-1\">Product</th>" +
                "<th class=\"column-2\">Initial Price</th>" +
                "<th class=\"column-3\">Final Price</th>" +
                "<th class=\"column-4\">Quantity</th>" +
                "<th class=\"column-5\">Total</th>" +
                "</tr>";
            for (var i = 0; i < response.length; i++) {
                product = response[i];
                var string = "";
                string += "<tr class=\"table_row\">";
                string += "<td class=\"column-1\" >";
                //string += "<div class=\"how-itemcart1\">";
                string += "<input type=\"image\" onclick=\"RemoveProductFromCart(" + product.SaleId + ")\" src=\"images/removee.png\" id=\"remove" + i + "\" width=\"40\" height=\"40\">";
                //string += "</div>";
                string += "</td>";
                string += "<td class=\"column-2\" id=\"InitialpriceModal" + i + "\"> - </td>";
                string += "<td class=\"column-3\" id=\"FinalpriceModal" + i + "\">" + product.PriceAfterDiscount.toFixed(2) + "</td>";
                string += "<td class=\"column-4\" id=\"quantityModal" + i + "\">" + product.Amount + "</td>";
                string += "<td class=\"column-5\" id=\"totalModal" + i + "\">" + product.PriceAfterDiscount.toFixed(2) * product.Amount + "</td>";
                string += "</tr>";
                mainDivModal.innerHTML += string;
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
    
}