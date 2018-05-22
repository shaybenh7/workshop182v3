var proxy;

ServiceProxy = function () //constructor for the proxy
{
    this._baseURL = "services/";
};

ServiceProxy.prototype =
    {
        _defaultErrorHandler: function (xhr, status, error) {
            console.log(xhr.statusText);
        },

        _doAjaxGET: function (method, data, fnSuccess, fnError) {
            if (!data) data = {};

            if (!fnError) fnError = this._defaultErrorHandler;

            jQuery.ajax({
                type: "GET",
                url: this._baseURL + method,
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: fnSuccess,
                error: fnError,
                dataFilter: function (data) {
                    var response;

                    if (typeof (JSON) !== "undefined" && typeof (JSON.parse) === "function")
                        response = JSON.parse(data);
                    else
                        response = val("(" + data + ")");

                    if (response.hasOwnProperty("d"))
                        return response.d;
                    else
                        return response;
                }
            });
        },
        _doAjaxPOST: function (method, data, fnSuccess, fnError) {
            if (!data) data = {};

            if (!fnError) fnError = this._defaultErrorHandler;

            jQuery.ajax({
                type: "POST",
                url: this._baseURL + method,
                data: data,
                contentType: "text/xml; charset=utf-8",
                dataType: "xml",
                success: fnSuccess,
                error: fnError
            });
        }


    };
$(document).ready(function () {
    jQuery(document).ready(function () {
    //register web service handler
        proxy = new ServiceProxy()
    });
});
