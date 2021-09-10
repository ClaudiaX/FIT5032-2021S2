let stores;

let xmlHttp = new XMLHttpRequest();
xmlHttp.open("GET","Stores/GetStores",false);// false: sync, true: async
xmlHttp.send(null);
stores = JSON.parse(xmlHttp.responseText);
console.log(stores);

let map;

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -37.810, lng: 144.964 },
        zoom: 11,
    });

    // get current location
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            position => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                map.setCenter(pos);
            },
            () => {
                handleLocationError(false, infoWindow, map.getCenter());
            }
        );
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }

    // mark stores
    
    for (var i = 0; i < stores.length; i++) {
        console.log(stores[i]);
        // geocode and mark
        geodoceAddress(map, stores[i]);
    }

}

function geodoceAddress(map, store) {
    var geocoder = new google.maps.Geocoder();
    var content = "<h3>" + store.Name + "</h3><hr/><p>" + store.Address + "</p><p>" + store.ContactNumber + "</p>";
    const infowindow = new google.maps.InfoWindow({
        content: content,
    });
    geocoder.geocode({ address: store.Address }, function (result, status) {
        if (status === "OK") {
            var marker = new google.maps.Marker({
                map: map,
                position: result[0].geometry.location
            });
        }
        marker.addListener("click", function () { infowindow.open(map, marker) });
    });
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}
