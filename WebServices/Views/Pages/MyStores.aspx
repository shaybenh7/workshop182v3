<%@ Page Title="index Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyStores.aspx.cs" Inherits="WebServices.Views.Pages.MyStores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

        <div class="col-lg-10 col-xl-7 m-lr-auto m-b-50" style="max-width: 67%; flex: 0 0 67%;">
            <div id="allStoresComponent">
                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="addProductInStoreModal">
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
                                                <span class="mtext-106 cl2">Add product to store</span>
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-name" placeholder="Enter product name">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-price" placeholder="Enter product price">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-amount" placeholder="Enter amount">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>


                                                <div class="flex-w flex-r-m p-b-10">
                                                    <div></div>

                                                    <div class="wrap-input1 w-full p-b-4">
                                                        <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-cat" placeholder="Enter category">
                                                        <div class="focus-input1 trans-04"></div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <input type="button" value="Add product" id="add_product_btn" onclick="addProductFunct();" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>



                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="editProductInStoreModal">
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
                                                <span class="mtext-106 cl2">Edit product</span>
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-id2" placeholder="Enter product id">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-price2" placeholder="Enter product price">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-amount2" placeholder="Enter amount">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>


                                                <div class="flex-w flex-r-m p-b-10">
                                                    
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <input type="button" value="Edit product" id="aviad-Edit-product" onclick="editStoreProduct();" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="removeProductFromStoreModal">
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
                                                <span class="mtext-106 cl2">Remove product</span>
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-id3" placeholder="Enter product id">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>
                                                <br />
                                                <br />
                                                <br />

                                                <input type="button" value="Remove product" id="aviad-Remove-product" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail" onclick="removeStoreProduct();"/>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="addStoreManagerModal">
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
                                                <span class="mtext-106 cl2">Add store manager</span>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="new-manager-name" placeholder="Enter manager name">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <input type="button" value="Add manager" id="Add-manager-Btn" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail" onclick="addNewManager()"/>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="removeStoreManagerModal">
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
                                                <span class="mtext-106 cl2">Remove store manager</span>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="old-manager-name" placeholder="Enter manager name">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <input type="button" value="Remove manager" id="Remove-manager-Btn" onclick="RemoveStoreManager();" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="addStoreOwnerModal">
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
                                                <span class="mtext-106 cl2">Add store owner</span>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="new-owner-name" placeholder="Enter manager name">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <input type="button" value="Add Owner" id="AddOwnerBtn" onclick="addStoreOwner();" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="removeStoreOwnerModal">
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
                                                <span class="mtext-106 cl2">Remove store owner</span>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="old-owner-name" placeholder="Enter owner name">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <input type="button" onclick="removeStoreOwner();" value="Remove owner" id="aviad-Remove-owner" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="addManagerPermissionModal">
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
                                                <span class="mtext-106 cl2">Add manager permissions</span>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="manager-to-change-permissions" placeholder="Enter manager name">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>
                                                <br />
                                                <br />
                                                <br />


                                                <div style="display: table; width: 100%;">
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addProductInStore" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                                        Add product in store
                                                    </div>

                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="editProductInStore" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Edit product in store
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeProductFromStore" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove product from store  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addStoreManager" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add store manager  
                                                    </div>

                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeStoreManager" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove store manager  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addManagerPermission" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add manager permission  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeManagerPermission" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove manager permission  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="viewPurchasesHistory" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        View purchases history  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeSaleFromStore" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove sale from store  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="editSale" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Edit sale  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addSaleToStore" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add sale to store  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addDiscount" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add discount  
                                                    </div>

                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addNewCoupon" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add new coupon  
                                                    </div>

                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addPolicy" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add policy  
                                                    </div>
         
                                                </div>

                                                <input type="button" style="margin-left:700px;" value="Add permissions" onclick="addmanagerPermisionFunc()" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="removeManagerPermissionModal">
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
                                                <span class="mtext-106 cl2">Remove manager permissions</span>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="manager-to-delete-permissions" placeholder="Enter manager name">
                                                    <div class="focus-input1 trans-04"></div>

                                                </div>
                                                <br />
                                                <br />
                                                <br />


                                                <div style="display: table; width: 100%;">
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addProductInStore22" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                                        Add product in store
                                                    </div>

                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="editProductInStore2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Edit product in store
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeProductFromStore2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove product from store  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addStoreManager2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add store manager  
                                                    </div>

                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeStoreManager2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove store manager  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addManagerPermission2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add manager permission  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeManagerPermission2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove manager permission  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="viewPurchasesHistory2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        View purchases history  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="removeSaleFromStore2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Remove sale from store  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="editSale2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Edit sale  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addSaleToStore2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add sale to store  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addDiscount2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add discount  
                                                    </div>

                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addNewCoupon2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add new coupon  
                                                    </div>
                                                    <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" id="addPolicy2" name="gender" value="female" style="margin-top: 5px; margin-right: 10px;">
                                                        Add Policy  
                                                    </div>
                                                </div>
                                                <input type="button" value="Remove permissions" onclick="removeManagerPermision()" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="addSaleToStoreModal">
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
                                                <span class="mtext-106 cl2">Add sale to store</span>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <div class="size-204 respon6-next">
                                                    <div>
                                                        <select name="time" id="products">
                                                        </select>
                                                        <div class="dropDownSelect2"></div>
                                                    </div>
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-amount-in-sale2" placeholder="Enter amount">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>

                                                <div class="size-204 respon6-next">
                                                    <div>
                                                        <select name="time" id="saleOption">
                                                            <option>Instant sale</option>
                                                            <option>Raffle Sale</option>
                                                        </select>
                                                        <div class="dropDownSelect2"></div>
                                                    </div>
                                                </div>

                                                <br />
                                                <br />
                                                <div class="flex-w flex-r-m p-b-10">
                                                    <div></div>
                                                    <div class="wrap-input1 w-full p-b-4">
                                                        <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-due-date2" placeholder="Enter due date - XX/XX/XXXX">
                                                        <div class="focus-input1 trans-04"></div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <input type="button" value="Add sale" id="AddSaleBtn" onclick="addSale();" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="editSaleModal">
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
                                                <span class="mtext-106 cl2">Edit sale in store</span>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                  <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="Sale-id5" placeholder="Enter sale id">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-amount-in-sale" placeholder="Enter amount">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                        <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="product-due-date" placeholder="Enter due date - XX/XX/XXXX">
                                                        <div class="focus-input1 trans-04"></div>
                                                 </div>
                                                <div class="flex-w flex-r-m p-b-10">
                                                    <div></div>
                                                    
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <input type="button" value="Edit sale" id="editSaleBtn" onclick="editSale();" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="removeSaleFromStoreModal">
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
                                                <span class="mtext-106 cl2">Remove sale</span>
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                        <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="Sale-id6" placeholder="Enter sale id">
                                                        <div class="focus-input1 trans-04"></div>
                                                 </div>
                                               

                                                <input type="button" value="Remove Sale" id="aviad-Remove-Sale" onclick="removeSale();" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="addDiscountModal">
                    <div class="overlay-modal1 js-hide-modal1"></div>

                    <div class="container">
                        <div class="bg0 p-t-60 p-b-30 p-lr-15-lg how-pos3-parent" style="padding-top: 20px;">

                            <button class="how-pos3 hov3 trans-04 js-hide-modal1">
                                <img src="images/icons/icon-close.png" alt="CLOSE">
                            </button>
                            <div class="row">
                                <div class="col-md-6 col-lg-7 p-b-30">
                                    <div class="p-l-25 p-r-30 p-lr-0-lg">
                                        <div class="wrap-slick3 flex-sb flex-w">

                                            <div class="size-204 flex-w flex-m respon6-next" style="padding-left: 50px;">
                                                <span class="mtext-106 cl2">Add discount</span>
                                                 <br />
                                                <br /> 
                                                <br />
                                                <br />
                                                
                                                <div class="size-204 respon6-next">
                                                    <div>

                                                        <select name="time" id="typeOfDiscount">
                                                            <option>product in store</option>
                                                            <option>category</option>
                                                            <asp:PlaceHolder ID="PlaceHolder2" Visible="false" runat="server">
                                                                <option>product</option>
                                                            </asp:PlaceHolder>
                                                        </select>
                                                        
                                                        <div class="dropDownSelect2"></div>
                                                    </div>
                                                </div>
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="DiscountPrecentage2" placeholder="enter the discount precentage">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="discountDueDate" placeholder="enter due date">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="discountto-what" placeholder="enter the products ids you want the copun to act on divide by ','">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                        
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="Restriction2" placeholder="enter the contry you want the copun to act on (divide by ',' )">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                                <br />
                                                <br />

                                                <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" name="gender" id="discountRaffle" value="male" style="margin-top: 5px; margin-right: 10px;">
                                                        Raffle sale
                                                </div>
                                                <br />
                                                <br />
                                                <div style="display: flex; margin-bottom: 10px; margin-left: 60px;">
                                                        <input type="checkbox" name="gender" id="discountInstant" value="male" style="margin-top: 5px; margin-right: 10px;">
                                                         instant sale
                                                </div>
                                                <div style="margin-top:40px;margin-left:700px;">
                                                    <input type="button" value="Add discount" onclick="addDiscountFunc()" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="removeDiscountModal">
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
                                                <span class="mtext-106 cl2">Remove discount</span>
                                                <br />
                                                <br />
                                                <div class="size-204 respon6-next">
                                                    <div>
                                                        <select name="time">
                                                            <option>Choose sale</option>
                                                            <option>Milk</option>
                                                            <option>Shawarma</option>
                                                        </select>
                                                        <div class="dropDownSelect2"></div>
                                                    </div>
                                                </div>
                                                <br />
                                                <br />
                                                <br />

                                                <button class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail">
                                                    Remove discount
                                                </button>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
                <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="addNewCouponModal">
                    <div class="overlay-modal1 js-hide-modal1"></div>

                    <div class="container">
                        <div class="bg0 p-t-60 p-b-30 p-lr-15-lg how-pos3-parent" style="padding-top:20px;">

                            <button class="how-pos3 hov3 trans-04 js-hide-modal1">
                                <img src="images/icons/icon-close.png" alt="CLOSE">
                            </button>
                            <div class="row">
                                <div class="col-md-6 col-lg-7 p-b-30">
                                    <div class="p-l-25 p-r-30 p-lr-0-lg">
                                        <div class="wrap-slick3 flex-sb flex-w">

                                            <div class="size-204 flex-w flex-m respon6-next" style="padding-left: 50px;">
                                                <span class="mtext-106 cl2">Add new coupon</span>
                                                <br />
                                                <br /> <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="copun-id" placeholder="Enter copun id">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                                 <br />
      
                                                <div class="size-204 respon6-next">
                                                    <div>

                                                        <select name="time" id="typeOfCopun">
                                                            <option>product in store</option>
                                                            <option>category</option>
                                                            <asp:PlaceHolder ID="productOptionForAddCopun" Visible="false" runat="server">
                                                                <option>product</option>
                                                            </asp:PlaceHolder>
                                                        </select>
                                                        
                                                        <div class="dropDownSelect2"></div>
                                                    </div>
                                                </div>
                                                 <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="DiscountPrecentage" placeholder="enter the discount precentage">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="CopunDueDate" placeholder="enter due date">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>

                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="to-what" placeholder="enter the products ids you want the copun to act on divide by ','">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                        
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="input1 bg-none plh1 stext-107 cl7" type="text" name="offer" id="Restriction" placeholder="enter the contry you want the copun to act on (divide by ',' )">
                                                    <div class="focus-input1 trans-04"></div>
                                                </div>
                                                <br />
                                                <br />

                                                <div style="display: flex; margin-bottom: 10px;">
                                                        <input type="checkbox" name="gender" id="copunRaffle" value="male" style="margin-top: 5px; margin-right: 10px;" checked>
                                                        Raffle sale
                                                </div>
                                                <br />
                                                <br />
                                                <div style="display: flex; margin-bottom: 10px; margin-left: 60px;">
                                                        <input type="checkbox" name="gender" id="copunInstant" value="male" style="margin-top: 5px; margin-right: 10px;" checked>
                                                         instant sale
                                                </div>

                                                <div style="margin-top:40px;margin-left:700px;">
                                                    <input type="button" value="Add coupon" onclick="addCopun()" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-10 col-lg-7 col-xl-5 m-lr-auto m-b-50" style="max-width: 33%; flex: 0 0 33%;">
            <div class="bor10 p-lr-40 p-t-30 p-b-40 m-l-63 m-r-40 m-lr-0-xl p-lr-15-sm" style="margin-top:46px; margin-right:91px; margin-left:-27px;">
                <h4 class="mtext-109 cl2 p-b-30">Create New Store
                </h4>




                <div class="flex-w flex-t bor12 p-t-15 p-b-30">
                    <div class="size-208 w-full-ssm">
                        <span class="stext-110 cl2">Store Name:
                        </span>

                    </div>
                    <div class="size-209 p-r-18 p-r-0-sm w-full-ssm">

                        <div class="p-t-15">

                            <div class="bor8 bg0 m-b-12">
                                <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="storeName" name="storeName" placeholder="My store">
                            </div>


                        </div>
                    </div>

                </div>

                <input type="button" value="Create Store" id="createStoreButton12" onclick="createStoreButton()" class="flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer"/>
            </div>
        </div>
    </div>

    <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="viewPurchasesHistoryModal">
                    <div class="overlay-modal1 js-hide-modal1"></div>
                    
                    <div class="container">
                        <div class="bg0 p-t-60 p-b-30 p-lr-15-lg how-pos3-parent" style="padding-top: 25px;">

                            <button class="how-pos3 hov3 trans-04 js-hide-modal1">
                                <img src="images/icons/icon-close.png" alt="CLOSE">
                            </button>
                            <div class="row">
                                <div class="col-md-6 col-lg-7 p-b-30">
                                    <div class="p-l-25 p-r-30 p-lr-0-lg">
                                        <div class="wrap-slick3 flex-sb flex-w">

                                            <div class="size-204 flex-w flex-m respon6-next">
                                                <span class="mtext-106 cl2">Store History Purchase</span>
                                                <br />
                                                <br />
                                                <div id="viewHistory" style=" margin-top:50px" class="size-204 respon6-next">
                                                    
                                                </div>
                                                <br />
                                                <br />
                                                <br />


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>

     <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="viewProductInStoreModal">
                    <div class="overlay-modal1 js-hide-modal1"></div>
                    
                    <div class="container">
                        <div class="bg0 p-t-60 p-b-30 p-lr-15-lg how-pos3-parent" style="padding-top: 25px;">

                            <button class="how-pos3 hov3 trans-04 js-hide-modal1">
                                <img src="images/icons/icon-close.png" alt="CLOSE">
                            </button>
                            <div class="row">
                                <div class="col-md-6 col-lg-7 p-b-30">
                                    <div class="p-l-25 p-r-30 p-lr-0-lg">
                                        <div class="wrap-slick3 flex-sb flex-w">

                                            <div class="size-204 flex-w flex-m respon6-next">
                                                <span class="mtext-106 cl2">All Product In Store</span>
                                                <br />
                                                <br />
                                                <div id="ViewProduct" style=" margin-top:50px" class="size-204 respon6-next">
                                                    
                                                </div>
                                                <br />
                                                <br />
                                                <br />


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>


        <div class="wrap-modal1 js-modal1 p-t-60 p-b-20" id="viewAddPolicyModal">
                    <div class="overlay-modal1 js-hide-modal1"></div>
                    
                    <div class="container">
                        <div class="bg0 p-t-60 p-b-30 p-lr-15-lg how-pos3-parent" style="padding-top: 25px;">

                            <button class="how-pos3 hov3 trans-04 js-hide-modal1">
                                <img src="images/icons/icon-close.png" alt="CLOSE">
                            </button>
                            <div class="row">
                                <div class="col-md-6 col-lg-7 p-b-30">
                                    <div class="p-l-25 p-r-30 p-lr-0-lg">
                                        <div class="wrap-slick3 flex-sb flex-w">

                                            <div class="size-204 flex-w flex-m respon6-next">
                                                <div>
                                                <span class="mtext-106 cl2" style="margin-right:50px">Add policy</span>
                                                    </div>
                                                <br />
                                                <br />
                                                <br />
                                                <div class="size-204 respon6-next">
                                                    <div>
                                                        <select name="time" id="PolicyType">
                                                                        <option>product in store</option>
                                                                        <option>category</option>
                                                                        <option>store</option>
                                                                        <option>country</option>
                                                                        <asp:PlaceHolder ID="PlaceHolder1" Visible="false" runat="server">
                                                                            <option>product</option>
                                                                        </asp:PlaceHolder>
                                                        </select>
                                                    </div>
                                                </div>
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="minPolicy" name="storeName" placeholder="minmum amount">
                                                </div>
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="maxPolicy" name="storeName" placeholder="max amount">
                                                </div>
                                                <br />
                                                <br />
                                                <div style="display: flex; margin-bottom: 10px; margin-left:20px">
                                                        <input type="checkbox" id="NoDiscount" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                                        without discount
                                                </div>
                                                <br />
                                                <br />
                                                <div style="display: flex; margin-bottom: 10px;margin-left:40px">
                                                        <input type="checkbox" id="NoCopuns" name="gender" value="male" style="margin-top: 5px; margin-right: 10px;">
                                                        without coupons
                                                </div>
                                                <br />
                                                <br />
                                                <div class="wrap-input1 w-full p-b-4">
                                                    <input class="stext-111 cl8 plh3 size-111 p-lr-15" type="text" id="PolicyChange"  name="storeName" placeholder="enter product in store id">
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <input type="button" id="addPolicy33" value="Add policy" onclick="addPolicy()" style="margin-left: 700px;" class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"/>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                
                        </div>



                    </div>
                </div>


            <script type="text/javascript" src="vendor/JS/MyStores.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#MyStoresMenuButton").addClass("active-menu")
        });
    </script>
</asp:Content>

