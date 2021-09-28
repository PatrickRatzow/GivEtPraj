var x = document.getElementById("demo");
var latitude;
var longitude;

function getLocation(successCallback, failureCallback) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(successCallback, failureCallback);
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}

function showPosition(position) {
    longitude = position.coords.latitude;
    latitude = position.coords.latitude;
    alert("Latitude: " + longitude +
        "<br>Longitude: " + latitude);
}

function alertLocation() {
    alert(showPosition);
}

function getCoords(successCallback, failureCallback) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(pos => {
            successCallback(pos.coords.longitude + "/" + pos.coords.latitude)
        }, dsed => {
            failureCallback("f")
        });
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}
