function geoLocation() {
    if (navigator.geolocation) {
    var location = navigator.geolocation.getCurrentPosition(showPosition);
    }
}

function showPosition(position) {
    alert(position.coords.latitude + position.coords.longtitude);

}