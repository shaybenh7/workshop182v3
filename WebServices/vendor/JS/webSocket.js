var ws;
$().ready(function () {
    ws = new WebSocket("ws://" + window.location.host +
        "/api/WebSocket/");

    ws.onopen = function () {
        console.log("connected");
    };
    ws.onmessage = function (evt) {
        console.log(evt.data);
        alert(evt.data);
    };
    ws.onerror = function (evt) {
        
        console.log(evt.message);
    };
    ws.onclose = function () {
        console.log("disconnected");
    };
});