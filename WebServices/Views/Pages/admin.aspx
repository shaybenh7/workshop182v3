<%@ Page Title="index Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="WebServices.Views.Pages.admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row" style="margin-left:40px;">
            <div class="bor10 p-lr-40 p-t-30 p-b-40 m-l-63 m-r-40 m-lr-0-xl p-lr-15-sm " style="margin-left: 0px; margin-right: 80px; margin-top:40px">


                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="viewHistoryModal">
                    <div class="overlay-modal1 js-hide-modal1"></div>
                    <div class="container">
                        <div class="bg0 p-t-60 p-b-30 p-lr-15-lg how-pos3-parent">

                            <button class="how-pos3 hov3 trans-04 js-hide-modal1">
                                <img src="images/icons/icon-close.png" alt="CLOSE">
                            </button>
                            <div class="row">
                                <div class="col-md-6 col-lg-7 p-b-30">
                                    <div class="p-l-25 p-r-30 p-lr-0-lg">
                                        <div class="wrap-slick3 flex-sb flex-w">

                                            <div class="size-204 flex-w flex-m respon6-next">
                                                <span class="mtext-106 cl2">View History</span>
                                                <br />

                                                    <table id="historyTable" class="table-shopping-cart">

                                                        <tr class="table_head">
                                                            <th class="column-2">userName</th>
                                                            <th class="column-1">StoreId</th>
                                                            <th class="column-1">ProductId</th>
                                                            <th class="column-1">BuyId</th>
                                                            <th class="column-1">Amount</th>
                                                            <th class="column-1">Price</th>
                                                            <th class="column-1">TypeOfSale</th>
                                                            <th class="column-5">Date</th>
                                                        </tr>
                                                    </table>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>

                <h4 class="mtext-109 cl2 p-b-30">Remove User</h4>
                <div class="flex-w flex-t bor12 p-t-15 p-b-30">
                    <div class="size-208 w-full-ssm">
                        <span class="stext-110 cl2">Username:
                        </span>

                    </div>
                    <div class="size-209 p-r-18 p-r-0-sm w-full-ssm">

                        <div class="p-t-15">

                            <div class="bor8 bg0 m-b-12">
                                <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="userName" name="userName" placeholder="Vadim">
                            </div>


                        </div>
                    </div>

                </div>
                <input type="button" value="Remove User" id="removeUserButton" onclick="removeUserFunc();" class="flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer" />
            </div>
            <div class="bor10 p-lr-40 p-t-30 p-b-40 m-l-63 m-r-40 m-lr-0-xl p-lr-15-sm" style="margin-left: 0px; margin-right: 80px; margin-top:40px;">
                <h4 class="mtext-109 cl2 p-b-30">View History</h4>
                <div class="flex-w flex-t">
                    <input type="radio" id="userRadio" name="optradio" checked="checked">User
                    <input type="radio" id="storeRadio" name="optradio">Store
                </div>
                <div class="flex-w flex-t bor12 p-t-15 p-b-30">

                    <div class="size-208 w-full-ssm">
                        <span class="stext-110 cl2">Name / StoreId:
                        </span>

                    </div>
                    <div class="size-209 p-r-18 p-r-0-sm w-full-ssm">

                        <div class="p-t-15">

                            <div class="bor8 bg0 m-b-12">
                                <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="nameInput" name="userName" placeholder="Vadim">
                            </div>


                        </div>
                    </div>

                </div>
                <input type="button" value="View Purchase History" id="viewHistoryButton" onclick="viewHistory();" class="flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer" />
            </div>
            
            <div class="bor10 p-lr-40 p-t-30 p-b-40 m-l-63 m-r-40 m-lr-0-xl p-lr-15-sm" style="margin-left: 0px; margin-right: 80px; margin-top:40px;width: 875px;height: 370px; margin-bottom: 100px;">
                <h4 class="mtext-109 cl2 p-b-30">Add Policy</h4>
                <div class="flex-w flex-t bor12 p-t-15 p-b-30" style="margin-top: -30px;">
                        <div class="wrap-input1 w-full p-b-4">
                            <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="minPolicyAdmin" name="storeName" placeholder="minmum amount">
                        </div>
                        <br />
                        <br />
                        <div class="wrap-input1 w-full p-b-4">
                            <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="maxPolicyAdmin" name="storeName" placeholder="max amount">
                        </div>
                        <br />
                        <br />
                        <div style="display: flex; margin-bottom: 10px; margin-left:20px">
                                <input type="checkbox" id="NoDiscountAdmin" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                without discount
                        </div>
                        <br />
                        <br />
                        <div style="display: flex; margin-bottom: 10px;margin-left:40px">
                                <input type="checkbox" id="NoCopunsAdmin" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                without coupons
                        </div>
                        <br />
                        <br />
                        <div class="wrap-input1 w-full p-b-4">
                            <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="ProductIdAdmin"  name="storeName" placeholder="enter product id">
                        </div>
                        <br />
                        <br />
                        <br />
                        <input type="button" id="addPolicy33Admin" value="Add policy" onclick="addPolicyAdmin()" style="margin-left: 600px;" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>

                </div>
            </div>

              <div class="bor10 p-lr-40 p-t-30 p-b-40 m-l-63 m-r-40 m-lr-0-xl p-lr-15-sm" style="margin-left: 0px; margin-right: 80px; margin-top:40px;width: 875px;height: 370px; margin-bottom: 100px; margin-top: -50px;">
                <h4 class="mtext-109 cl2 p-b-30">Add Discount</h4>
                <div class="flex-w flex-t bor12 p-t-15 p-b-30" style="margin-top: -30px;">
                        <div class="wrap-input1 w-full p-b-4">
                            <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="zahiProductsName" name="storeName" placeholder="enter the product names you want the copun to act on divide by ','"">
                        </div>
                        <br />
                        <br />
                        <div class="wrap-input1 w-full p-b-4">
                            <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="zahiDiscountPrecentage" name="storeName" placeholder="enter the discount precentage">
                        </div>
                        <div class="wrap-input1 w-full p-b-4">
                            <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="zahiDueDateDiscount"  name="storeName" placeholder="enter due date">
                        </div>
                        <br />
                        <br />
                        <div style="display: flex; margin-bottom: 10px; margin-left:20px">
                                <input type="checkbox" id="RaffleAdminDiscount" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                Raffle sale
                        </div>
                        
                        <br />
                        <br />
                        <div style="display: flex; margin-bottom: 10px;margin-left:40px">
                                <input type="checkbox" id="InstanteAdminDiscount" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                Instant sale
                        </div>
                        <br />
                        <br />
                        <div class="wrap-input1 w-full p-b-4">
                            <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="zahiCountryDiscount"  name="storeName" placeholder="enter the contry you want the copun to act on (divide by ',' )">
                        </div>
                        <br />
                        <br />
                        <br />
                        <input type="button" id="addPolicy33AdminDiscount" value="Add discount" onclick="addDiscountAdmin()" style="margin-left: 600px;" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>

                </div>
            </div>
        </div>
    </div>





    <script type="text/javascript">
        

        
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#AdminMenuButton").addClass("active-menu")
        });
    </script>
                <script type="text/javascript" src="vendor/JS/AdminAddPolicy.js"></script>

</asp:Content>

