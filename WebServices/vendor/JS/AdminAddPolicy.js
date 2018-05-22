var addPolicyAdmin = function () {
    minAmount = $("#minPolicyAdmin").val();
    maxAmount = $("#maxPolicyAdmin").val();
    noDiscount = $("#NoDiscountAdmin")[0].checked;
    NoCopuns = $("#NoCopunsAdmin")[0].checked
    productId = $("#ProductIdAdmin").val(); 
    addProductPolicyAdmin(minAmount, maxAmount, noDiscount, NoCopuns, productId);

}

var addProductPolicyAdmin = function (minAmount, maxAmount, noDiscount, NoCopuns, pId) {
    if (minAmount !== undefined && minAmount !== "" && maxAmount !== undefined && maxAmount !== "") {
        jQuery.ajax({
            type: "GET",
            url: baseUrl + "/api/user/setAmountPolicyOnProduct?productName=" + pId +
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
            url: baseUrl + "/api/user/setAmountPolicyOnProduct?productName=" + pId,
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
            url: baseUrl + "/api/user/setNoCouponsPolicyOnProduct?productName=" + pId,
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

var addDiscountAdmin = function () {
    type = 3;
    DiscountPrecentage = $("#zahiDiscountPrecentage").val();
    DueDate = $("#zahiDueDateDiscount").val();
    to_what = $("#zahiProductsName").val();
    Restriction = fixRestricion("#zahiCountryDiscount", "#RaffleAdminDiscount", "#InstanteAdminDiscount");

    jQuery.ajax({
        type: "GET",
        url: baseUrl + "/api/store/addDiscount?storeId=" + 12 +
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
    if (Restriction !== null && Restriction !== "" && (RaffleCheck || InstantCheck))
        Restriction += "/";
    RaffleCheck = $(copunRaffle)[0].checked;
    InstantCheck = $(copunInstant)[0].checked
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
var removeUserFunc = function () {
    userName = $("#userName").val();

    jQuery.ajax({
        type: "GET",
        url: baseUrl + "/api/user/removeUser?userDeleted=" + userName,
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

var viewHistory = function () {
    name = $("#nameInput").val();
    var ajaxURL = "";

    if (document.getElementById('userRadio').checked) {
        ajaxURL = baseUrl + "/api/store/viewUserHistory?userToGet=" + name;
    } else if (document.getElementById('storeRadio').checked) {
        ajaxURL = baseUrl + "/api/store/viewStoreHistory?storeId=" + name;
    }

    var mainDivModal = document.getElementById('historyTable');
    jQuery.ajax({
        type: "GET",
        url: ajaxURL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var i;
            if (response == "Errror: permissions or store not valid!")
            {
                alert(response);
            }
            else if (response == "Error: user does not exist!") {
                alert(response);
            }
            else {
                for (i = 0; i < response.length; i++) {
                    element = response[i];
                    var Amount = element["Amount"];
                    var BuyId = element["BuyId"];
                    var Date = element["Date"];
                    var Price = element["Price"];
                    var ProductId = element["ProductId"];
                    var StoreId = element["StoreId"];
                    var TypeOfSale = element["TypeOfSale"];
                    var UserName = element["UserName"];

                    var string = "";
                    string += "<tr class=\"table_row\">";
                    string += "<td class=\"column-2\">" + UserName + "</td>";
                    string += "<td class=\"column-1\">" + StoreId + "</td>";
                    string += "<td class=\"column-1\">" + ProductId + "</td>";
                    string += "<td class=\"column-1\">" + BuyId + "</td>";
                    string += "<td class=\"column-1\">" + Amount + "</td>";
                    string += "<td class=\"column-1\">" + Price + "</td>";
                    string += "<td class=\"column-1\">" + TypeOfSale + "</td>";
                    string += "<td class=\"column-5\">" + Date + "</td>";

                    string += "</tr>";
                    mainDivModal.innerHTML += string;

                }

                var element = document.getElementById("viewHistoryModal");
                element.classList.add("show-modal1");
            }


        },
        error: function (response) {
            console.log(response);
            // window.location.reload(false); 
        }
    });
    //var element = document.getElementById("viewHistoryModal");
    //element.classList.add("show-modal1");
};