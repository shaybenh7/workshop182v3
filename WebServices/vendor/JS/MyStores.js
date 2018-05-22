var lastClickedStoreId;
var productsInStore;


$(document).ready(function () {
    var mainDiv = document.getElementById('allStoresComponent');

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/user/getAllStoresUnderUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var i;
            for (i = 0; i < response.length; i++) {
                storeRole = response[i];
                if (storeRole["store"]["isActive"] === 1 && (storeRole["type"] === "Manager" || storeRole["type"] === "Owner")) {
                    var storeName = storeRole["store"]["name"];
                    var storeId = storeRole["store"]["storeId"];
                    var disabledLinksInitial = "disabledLink";
                    var actionInitial = "";
                    if (storeRole["type"] === "Owner") {
                        disabledLinksInitial = "";
                        actionInitial = "modalLinkListener(event);";
                    }
                    var string = "";

                    string += "<div class=\"p-t-50\" style=\"padding-left:50px\">";
                    string += "<h4 id=\"storeNameHeader" + i +"\" class=\"mtext-112 cl2 p-b-27\">" + storeName + "</h4>";
                    string += "<div class=\"flex-w m-r--5\">";
                    string += "<a href=\"#\" id=\"addProductInStore" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add Product</a>";
                    string += "<a href=\"#\" id=\"editProductInStore" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Edit Product</a>";
                    string += "<a href=\"#\" id=\"viewProductInStore" + i + "\" data-id=\"" + storeId + "\" onclick=\"viewProducts(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">View Product</a>";
                    string += "<a href=\"#\" id=\"removeProductFromStore" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Remove Product</a>";
                    string += "<a href=\"#\" id=\"addStoreManager" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add Store Manager</a>";
                    string += "<a href=\"#\" id=\"removeStoreManager" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Remove Store Manager</a>";
                    string += "<a href=\"#\" id=\"addStoreOwner" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add Store Owner</a>";
                    string += "<a href=\"#\" id=\"removeStoreOwner" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Remove Store Owner</a>";
                    string += "<a href=\"#\" id=\"addManagerPermission" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add Manager Permission</a>";
                    string += "<a href=\"#\" id=\"removeManagerPermission" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Remove Manager Permission</a>";
                    string += "<a href=\"#\" id=\"addSaleToStore" + i + "\" data-id=\"" + storeId + "\" onclick=\"addSaleView(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add Sale</a>";
                    string += "<a href=\"#\" id=\"editSale" + i + "\" data-id=\"" + storeId + "\" onclick=\"editSaleView(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Edit Sale</a>";
                    string += "<a href=\"#\" id=\"removeSaleFromStore" + i + "\" data-id=\"" + storeId + "\" onclick=\"modalLinkListener(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Remove Sale</a>";
                    string += "<a href=\"#\" id=\"addDiscount" + i + "\" data-id=\"" + storeId + "\" onclick=\"viewAddDiscount(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add Discount</a>";
                    string += "<a href=\"#\" id=\"addNewCoupon" + i + "\" data-id=\"" + storeId + "\" onclick=\"viewCopun(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add Coupon</a>";
                    string += "<a href=\"#\" id=\"viewPurchasesHistory" + i + "\" data-id=\"" + storeId + "\" onclick=\"viewHistory(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">View History</a>";
                    string += "<a href=\"#\" id=\"viewAddPolicy" + i + "\" data-id=\"" + storeId + "\" onclick=\"viewAddPolicy(event);\" class=\"flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5 \">Add policy</a>";
                    string += "</div>";
                    string += "</div>";
                    mainDiv.innerHTML += string;


                    if (storeRole["type"] === "Manager") {
                        (function (i, storeId) {
                            jQuery.ajax({
                                type: "GET",
                                url: baseUrl+"/api/user/getPremissionsOfAManager?storeId=" + storeId,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) { //iterate through premissions and enable links
                                    response = response["privileges"];
                                    if (response.addProductInStore != true)
                                        $("#addProductInStore"+i).css('display', 'none');
                                    if (response.editProductInStore != true)
                                        $("#editProductInStore" + i).css('display', 'none');
                                    if (response.viewProductInStore != true)
                                        $("#viewProductInStore" + i).css('display', 'none');
                                    if (response.removeProductFromStore != true)
                                        $("#removeProductFromStore" + i).css('display', 'none');
                                    if (response.addStoreManager != true)
                                        $("#addStoreManager" + i).css('display', 'none');
                                    if (response.removeStoreManager != true)
                                        $("#removeStoreManager" + i).css('display', 'none');
                                    if (response.addStoreOwner != true)
                                        $("#addStoreOwner" + i).css('display', 'none');
                                    if (response.removeStoreOwner != true)
                                        $("#removeStoreOwner" + i).css('display', 'none');
                                    if (response.addManagerPermission != true)
                                        $("#addManagerPermission" + i).css('display', 'none');
                                    if (response.removeManagerPermission != true)
                                        $("#removeManagerPermission" + i).css('display', 'none');
                                    if (response.addSaleToStore != true)
                                        $("#addSaleToStore" + i).css('display', 'none');
                                    if (response.editSale != true)
                                        $("#editSale" + i).css('display', 'none');
                                    if (response.removeSaleFromStore != true)
                                        $("#removeSaleFromStore" + i).css('display', 'none');
                                    if (response.addDiscount != true)
                                        $("#addDiscount" + i).css('display', 'none');
                                    if (response.addNewCoupon != true)
                                        $("#addNewCoupon" + i).css('display', 'none');
                                    if (response.viewPurchasesHistory != true)
                                        $("#viewPurchasesHistory" + i).css('display', 'none');
                                    if (response.viewAddPolicy != true)
                                        $("#viewAddPolicy" + i).css('display', 'none');
                                },
                                error: function (response) {
                                    console.log(response);
                                }
                            });

                        })(i, storeId);
                    }

                }

            }
        },
        error: function (response) {
            console.log(response);
            window.location.href = baseUrl+"/error";
        }
    });

});

var editSaleView = function (e) {
    viewAddPolicy(e);
    $("#editSaleBtn").click(editSale);

}

var createStoreButton = function () {

    Storename = $("#storeName").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/createStore?storeName=" + Storename,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            window.location.reload(false);
        }
    });
};


var addProductFunct = function () {
    productName = $("#product-name").val();
    price = $("#product-price").val();
    amount = $("#product-amount").val();
    cat = $("#product-cat").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/addProductInStore?productName=" + productName + "&price=" + price
            + "&amount=" + amount + "&storeId=" + lastClickedStoreId + "&category=" + cat,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var addNewManager = function () {
    ManagerName = $("#new-manager-name").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/addStoreManager?storeId=" + lastClickedStoreId + "&newManager=" + ManagerName,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var RemoveStoreManager = function () {
    ManagerName = $("#old-manager-name").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/removeStoreManager?storeId=" + lastClickedStoreId + "&oldManageruserName=" + ManagerName,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var addStoreOwner = function () {
    NewOwnerName = $("#new-owner-name").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/addStoreOwner?storeId=" + lastClickedStoreId + "&newOwner=" + NewOwnerName,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var removeStoreOwner = function () {
    OldOwnerName = $("#old-owner-name").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/removeStoreOwner?storeId=" + lastClickedStoreId + "&oldOwner=" + OldOwnerName,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var editStoreProduct = function () {
    productId = $("#product-id2").val();
    price = $("#product-price2").val();
    amount = $("#product-amount2").val();
    
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/editProductInStore?productInStoreId=" + productId +
            "&price=" + price + "&amount=" + amount + "&storeId=" + lastClickedStoreId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var removeStoreProduct = function () {
    productId = $("#product-id3").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/removeProductFromStore?storeId=" + lastClickedStoreId +
            "&ProductInStoreId=" + productId, 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var addSale = function () {
    productId = $("#products")[0].selectedIndex;
    productId = productsInStore[productId].productInStoreId;
    amount = $("#product-amount-in-sale2").val();
    kindOfSale = $("#saleOption")[0].selectedIndex+1;
    if (kindOfSale === 2) {
        kindOfSale = 3;
    }
    date = $("#product-due-date2").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/addSaleToStore?storeId=" + lastClickedStoreId +
            "&pisId=" + productId + "&typeOfSale=" + kindOfSale + "&amount=" + amount +
            "&dueDtae=" + date,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var editSale = function () {
    saleId = $("#Sale-id5").val();
    amount = $("#product-amount-in-sale").val();
    date = $("#product-due-date").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/editSale?storeId=" + lastClickedStoreId +
            "&saleId=" + saleId + "&amount=" + amount + "&dueDate=" + date +
            "&dueDtae=" + date,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};

var removeSale = function () {
    saleId = $("#Sale-id6").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/removeSaleFromStore?storeId=" + lastClickedStoreId +
            "&saleId=" + saleId ,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);

        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
};


function addSaleView(e) {
    modalLinkListener(e);
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/getProductInStore?storeId=" + lastClickedStoreId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var viewHistory = document.getElementById("viewHistory");
            productsInStore = response;
            var productHtml = document.getElementById("products");
            productHtml.innerHTML = ""
            for (i = 0; i < productsInStore.length; i++) {
                productHtml.innerHTML += "<option>" + productsInStore[i].product.name + "</option>";
            }

        },
        error: function (response) {
            console.log(response);

        }
    });

}
function viewProducts(e) {
    modalLinkListener(e);
    storeid = lastClickedStoreId;
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/getProductInStore?storeId=" + lastClickedStoreId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var viewHistory = document.getElementById("ViewProduct");
            if (response.length === 0) {
                viewHistory.innerHTML = "<div style=\"padding-left: 30px;\"> there are no products in this store  </div>"
            }
            else {
                viewHistory.innerHTML = "<div >"
                for (var i = 0; i < response.length; i++){
                    if (response[i].isActive == 0)
                        continue;
                    viewHistory.innerHTML += "<div style=\"width: 300px; padding: 17px; border-color: black; border-width: 1px; margin-left: 20px; margin-bottom: 20px; border-style: groove; \"> "+
                        "<div> product in store id : " + response[i].productInStoreId + "</div>"+
                        "<div>     product-id : " + response[i].product.productId + "</div>" +
                        "<div>     product name : " + response[i].product.name + "</div>" +
                        "<div>     price : " + response[i].price + "</div>" +
                        "<div>     amount : " + response[i].quantity + "</div>" +
                        "<div>     category : " + response[i].category + "</div>" +
                            "</div>";

                }
                viewHistory.innerHTML +="</div>"
            }

        },
        error: function (response) {
            console.log(response);

        }
    });
}

function viewHistory(e) {
    modalLinkListener(e);
    storeid = lastClickedStoreId;
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/viewStoreHistory?storeId=" + lastClickedStoreId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var viewHistory = document.getElementById("viewHistory");
            if (response.length === 0) {
                viewHistory.innerHTML = "<div style=\"padding-left: 30px;\"> there were not purcheses from this store  </div>"
            }
            else {
                viewHistory.innerHTML = "<div >"
                for (var i = 0; i < response.length; i++) {
                    newDiv= "<div style=\"width: 300px; padding: 17px; border-color: black; border-width: 1px; margin-left: 20px; margin-bottom: 20px; border-style: groove; \"> " +
                        "<div> Buyer : " + response[i].UserName + "</div>" +
                        "<div> product-id : " + response[i].ProductId + "</div>" +
                        "<div> Date : " + response[i].Date + "</div>";
                    if (response[i] == 1) {
                        newDiv += "<div>Type of sale : instant sale</div>" +
                            "</div>";
                    }
                    else {
                        newDiv += "<div>Type of sale : Raffle sale</div>" +
                            "</div>";
                    }
                    viewHistory.innerHTML += newDiv;
                       

                }
                viewHistory.innerHTML += "</div>"
            }
            
        },
        error: function (response) {
            console.log(response);
            
        }
    });
    
}

var viewAddPolicy= function(e){
    modalLinkListener(e);
    $("#addPolicy33").click(addPolicy);
    $("#PolicyType").on('change', function () {
        policyType = $("#PolicyType")[0].selectedIndex;
        changedTypeOfPolicy(policyType, "#PolicyChange");
    });
}

var changedTypeOfPolicy = function (typeOfCopun, inbox) {
    switch (typeOfCopun) {
        case 0:
            $(inbox).attr("placeholder", "enter product in store id");
            $(inbox).show();
            break;
        case 1:
            $(inbox).attr("placeholder", "enter category name");
            $(inbox).show();
            break;
        case 2:
            $(inbox).hide();
            break;
        case 3:
            $(inbox).show();
            $(inbox).attr("placeholder", "enter country name");
            break;
        case 4:
            $(inbox).attr("placeholder", "enter product id");
            $(inbox).show();
            break;
    }
}

var viewCopun = function (e) {
    modalLinkListener(e);
    $("#typeOfCopun").on('change', function () {
        typeOfCopun = $("#typeOfCopun")[0].selectedIndex;
        changeTypeOfCopun(typeOfCopun, "#to-what");
    });

}

var viewAddDiscount = function (e) {
    modalLinkListener(e);
    $("#typeOfDiscount").on('change', function () {
        typeOfCopun = $("#typeOfDiscount")[0].selectedIndex;
        changeTypeOfCopun(typeOfCopun,"#discountto-what");
    });

}

var changeTypeOfCopun = function (typeOfCopun, towhat) {
    switch (typeOfCopun) {
        case 0:
            $(towhat).attr("placeholder", "enter the products in store ids you want the copun to act on divide by ','");
            break;
        case 1:
            $(towhat).attr("placeholder", "enter the categoris names you want the copun to act on divide by ','");
            break;

        case 2:
            $(towhat).attr("placeholder", "enter the product names you want the copun to act on divide by ','");
            break;
    }
}

var addCopun = function () {
    var a = $("#copunRaffle")[0].checked;
    var b = $("#copunInstant")[0].checked;
    if (!a && !b) {
        alert("you have to check at least one of this checkboxes : Raffle sale, Instant sale")
        return;
    }

    copunId = $("#copun-id").val();
    typeOfCopun = $("#typeOfCopun")[0].selectedIndex+1;
    to_what = $("#to-what").val();
    Restriction = fixRestricion("#Restriction", "#copunRaffle", "#copunInstant");
    DiscountPrecentage = $("#DiscountPrecentage").val();
    CopunDueDate = $("#CopunDueDate").val();
    

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/addCouponDiscount?storeId=" + lastClickedStoreId +
            "&couponId=" + copunId + "&type=" + typeOfCopun + "&towaht=" + to_what +
            "&percentage=" + DiscountPrecentage + "&dueDate=" + CopunDueDate +
            "&restrictions=" + Restriction ,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);
        },
        error: function (response) {
            console.log(response);

        }
    });
}

var addPolicy = function () {
    typeOfPolicy = $("#PolicyType")[0].selectedIndex ;
    minAmount = $("#minPolicy").val();
    maxAmount = $("#maxPolicy").val();
    noDiscount = $("#NoDiscount")[0].checked;
    NoCopuns = $("#NoCopuns")[0].checked
    PolicyChange = $("#PolicyChange").val();
    switch (typeOfPolicy) {
        case 0:
            addProductInStorePolicy(minAmount, maxAmount, noDiscount, NoCopuns, PolicyChange);
            break;
        case 1:
            addCategoryPolicy(minAmount, maxAmount, noDiscount, NoCopuns, PolicyChange);
            break;
        case 2:
            addStorePolicy(minAmount, maxAmount, noDiscount, NoCopuns);
            break;
        case 3:
            addCountryPolicy(minAmount, maxAmount, noDiscount, NoCopuns, PolicyChange);
            break;
        case 4:
            addProductPolicy(minAmount, maxAmount, noDiscount, NoCopuns, PolicyChange);
            break;
    }

}

var addProductPolicy = function (minAmpunt, maxAmount, noDiscount, NoCopuns, pId) {
    if (minAmount !== undefined && minAmount !== "" && maxAmount !== undefined && maxAmount !== "") {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/user/setAmountPolicyOnProduct?productName=" + pId +
                "&minAmount=" + minAmount + "&maxAmount=" + maxAmount,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (noDiscount) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/user/setAmountPolicyOnProduct?productName=" + pId ,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }

    if (NoCopuns) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/user/setNoCouponsPolicyOnProduct?productName=" + pId ,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
}

var addCountryPolicy = function (minAmpunt, maxAmount, noDiscount, NoCopuns, country) {
    if (minAmount !== undefined && minAmount !== "" && maxAmount !== undefined && maxAmount !== "") {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setAmountPolicyOnCountry?storeId=" + lastClickedStoreId +
                "&country=" + country + "&minAmount=" + minAmount + "&maxAmount=" + maxAmount,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (noDiscount) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoDiscountPolicyOnCountry?storeId=" + lastClickedStoreId +
                "&country=" + country,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (NoCopuns) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoCouponPolicyOnCountry?storeId=" + lastClickedStoreId +
                "&country=" + country,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
}

var addStorePolicy = function (minAmount, maxAmount, noDiscount, NoCopuns) {
    if (minAmount !== undefined && minAmount !== "" && maxAmount !== undefined && maxAmount !== "") {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setAmountPolicyOnStore?storeId=" + lastClickedStoreId +
                "&minAmount=" + minAmount + "&maxAmount=" + maxAmount,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (noDiscount) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoDiscountPolicyOnStore?storeId=" + lastClickedStoreId ,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (NoCopuns) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoCouponsPolicyOnStore?storeId=" + lastClickedStoreId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
}

var addCategoryPolicy = function (minAmpunt, maxAmount, noDiscount, NoCopuns, catName) {
    if (minAmount !== undefined && minAmount !== "" && maxAmount !== undefined && maxAmount !== "") {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setAmountPolicyOnCategory?storeId=" + lastClickedStoreId +
                "&category=" + catName + "&minAmount=" + minAmount + "&maxAmount=" + maxAmount,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (noDiscount) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoDiscountPolicyOnCategoty?storeId=" + lastClickedStoreId +
                "&category=" + catName,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (NoCopuns) {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoDiscountPolicyOnCategoty?storeId=" + lastClickedStoreId +
                "&category=" + catName,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
}

var addProductInStorePolicy = function (minAmount, maxAmount, noDiscount, NoCopuns, pisId) {
    if (minAmount !== undefined && minAmount !== "" && maxAmount !== undefined && maxAmount !== "")
    {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setAmountPolicyOnProductInStore?storeId=" + lastClickedStoreId +
                "&productInStoreId=" + pisId + "&minAmount=" + minAmount + "&maxAmount=" + maxAmount,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (noDiscount)
    {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoDiscountPolicyOnProductInStore?storeId=" + lastClickedStoreId +
                "&productInStoreId=" + pisId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
    if (NoCopuns)
    {
        jQuery.ajax({
            type: "GET",
            url: baseUrl+"/api/store/setNoCouponPolicyOnProductInStore?storeId=" + lastClickedStoreId +
                "&productInStoreId=" + pisId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response);
                window.location.reload(false);
            },
            error: function (response) {
                console.log(response);

            }
        });
    }
}

var addDiscountFunc = function () {
    type = $("#typeOfDiscount")[0].selectedIndex + 1;
    DiscountPrecentage = $("#DiscountPrecentage2").val();
    DueDate = $("#discountDueDate").val();
    to_what = $("#discountto-what").val();
    Restriction = fixRestricion("#Restriction2", "#discountRaffle", "#discountInstant");

    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/api/store/addDiscount?storeId=" + lastClickedStoreId +
            "&type=" + type + "&percentage=" + DiscountPrecentage +
            "&toWhat=" + to_what + "&dueDate=" + DueDate +
            "&restrictions=" + Restriction,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);
        },
        error: function (response) {
            console.log(response);

        }
    });
}

var fixRestricion = function (restriction, copunRaffle, copunInstant) {
    Restriction = $(restriction).val();
    if (Restriction !== null && Restriction !== "") {
        Restriction = "COUNTRY=" + Restriction;
    }
    RaffleCheck = $(copunRaffle)[0].checked;
    InstantCheck = $(copunInstant)[0].checked
    if (Restriction !== null && Restriction !== "" && (RaffleCheck || InstantCheck))
        Restriction += "/";
    if (RaffleCheck || InstantCheck) {
        Restriction += "TOS=";
    }
    if (RaffleCheck & !InstantCheck) {
        Restriction += "3";
    }
    else if (!RaffleCheck & InstantCheck) {
        Restriction += "1";
    }
    else {
        Restriction += "1,3";
    }
    return Restriction;
}

var removeManagerPermision = function () {
    manager = $("#manager-to-delete-permissions").val();
    if ($("#addProductInStore22")[0].checked) {
        sendRemoveManagerPremission(manager, "addProductInStore");
    }
    if ($("#editProductInStore2")[0].checked) {
        sendRemoveManagerPremission(manager, "editProductInStore");
    }
    if ($("#removeProductFromStore2")[0].checked) {
        sendRemoveManagerPremission(manager, "removeProductFromStore");
    }
    if ($("#addStoreManager2")[0].checked) {
        sendRemoveManagerPremission(manager, "addStoreManager");
    }
    if ($("#removeStoreManager2")[0].checked) {
        sendRemoveManagerPremission(manager, "removeStoreManager");
    }
    if ($("#addManagerPermission2")[0].checked) {
        sendRemoveManagerPremission(manager, "addManagerPermission");
    }
    if ($("#removeManagerPermission2")[0].checked) {
        sendRemoveManagerPremission(manager, "removeManagerPermission");
    }
    if ($("#viewPurchasesHistory2")[0].checked) {
        sendRemoveManagerPremission(manager, "viewPurchasesHistory");
    }
    if ($("#removeSaleFromStore2")[0].checked) {
        sendRemoveManagerPremission(manager, "removeSaleFromStore");
    }
    if ($("#editSale2")[0].checked) {
        sendRemoveManagerPremission(manager, "editSale");
    }
    if ($("#addSaleToStore2")[0].checked) {
        sendRemoveManagerPremission(manager, "addSaleToStore");
    }
    if ($("#addDiscount2")[0].checked) {
        sendRemoveManagerPremission(manager, "addDiscount");
    }
    if ($("#addNewCoupon2")[0].checked) {
        sendRemoveManagerPremission(manager, "addNewCoupon");
    }
    if ($("#addPolicy2")[0].checked) {
        sendRemoveManagerPremission(manager, "changePolicy");
    }
}

var addmanagerPermisionFunc = function () {
    manager = $("#manager-to-change-permissions").val();
    if ($("#addProductInStore")[0].checked) {
        sendAddManagerPremission(manager, "addProductInStore");
    }
    if ($("#editProductInStore")[0].checked) {
        sendAddManagerPremission(manager, "editProductInStore");
    }
    if ($("#removeProductFromStore")[0].checked) {
        sendAddManagerPremission(manager, "removeProductFromStore");
    }
    if ($("#addStoreManager")[0].checked) {
        sendAddManagerPremission(manager, "addStoreManager");
    }
    if ($("#removeStoreManager")[0].checked) {
        sendAddManagerPremission(manager, "removeStoreManager");
    }
    if ($("#addManagerPermission")[0].checked) {
        sendAddManagerPremission(manager, "addManagerPermission");
    }
    if ($("#removeManagerPermission")[0].checked) {
        sendAddManagerPremission(manager, "removeManagerPermission");
    }
    if ($("#viewPurchasesHistory")[0].checked) {
        sendAddManagerPremission(manager, "viewPurchasesHistory");
    }
    if ($("#removeSaleFromStore")[0].checked) {
        sendAddManagerPremission(manager, "removeSaleFromStore");
    }
    if ($("#editSale")[0].checked) {
        sendAddManagerPremission(manager, "editSale");
    }
    if ($("#addSaleToStore")[0].checked) {
        sendAddManagerPremission(manager, "addSaleToStore");
    }
    if ($("#addDiscount")[0].checked) {
        sendAddManagerPremission(manager, "addDiscount");
    }
    if ($("#addNewCoupon")[0].checked) {
        sendAddManagerPremission(manager, "addNewCoupon");
    }
    if ($("#addPolicy")[0].checked) {
        sendAddManagerPremission(manager, "changePolicy");
    }
}

var sendRemoveManagerPremission = function (manager, premission) {
    sendPremission(manager, premission, "api/store/removeManagerPermission")
}

var sendAddManagerPremission = function (manager, premission) {
    sendPremission(manager, premission,"api/store/addManagerPermission")
}

var sendPremission = function (manager, premission, uri) {
    jQuery.ajax({
        type: "GET",
        url: baseUrl+"/"+ uri +"?storeId=" + lastClickedStoreId +
            "&ManageruserName=" + manager + "&permission=" + premission,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            window.location.reload(false);
        },
        error: function (response) {
            console.log(response);

        }
    });
}

function modalLinkListener(e) {
    lastClickedStoreId = e["srcElement"]["dataset"]["id"];
    key = e["srcElement"]["id"];
    key = key.replace(/[0-9]/g, '');
    openModal(key + "Modal");
    return false;
}

function enableLink(id) {
    var element = document.getElementById(id);
    element.setAttribute("onClick", "modalLinkListener(event);");
    element.classList.remove("disabledLink");
}

function openModal(id) {
    var element = document.getElementById(id);
    element.classList.add("show-modal1");
}
