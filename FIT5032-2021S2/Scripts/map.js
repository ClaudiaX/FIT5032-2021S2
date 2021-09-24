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

    // auto complete for start
    var start = document.getElementById("start");
    const autoComplete = new google.maps.places.Autocomplete(start);
    autoComplete.bindTo("bounds", map);

    // get direction
    const directionsRenderer = new google.maps.DirectionsRenderer();
    const directionsService = new google.maps.DirectionsService();
    directionsRenderer.setMap(map);
    directionsRenderer.setPanel(document.getElementById("sidebar"));
    var getDirection = document.getElementById("get-direction");
    getDirection.addEventListener("click", function () {
        directionsService.route({
            origin: {
                query:document.getElementById("start").value
            },
            destination: {
                query:document.getElementById("end").value
            },
            travelMode: google.maps.TravelMode[document.getElementById("mode").value]
        }, (response, status) => {
            if (status == "OK") {
                directionsRenderer.setDirections(response);
            } else {
                window.alert("Unable to direct due to " + status);
            }
        });
    });


}

function geodoceAddress(map, store) {
    var geocoder = new google.maps.Geocoder();
    var content = "<h3>" + store.Name + "</h3><hr/><p>" + store.Address + "</p><p>" + store.ContactNumber + "</p>";
    const infowindow = new google.maps.InfoWindow({
        content: content,
    });
    geocoder.geocode({ address: store.Address }, function (result, status) {
        if (status === "OK") {

            const image = {
                url: "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png",
                // This marker is 20 pixels wide by 32 pixels high.
                size: new google.maps.Size(20, 32),
                // The origin for this image is (0, 0).
                origin: new google.maps.Point(0, 0),
                // The anchor for this image is the base of the flagpole at (0, 32).
                anchor: new google.maps.Point(0, 32),
            };

            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                animation: google.maps.Animation.DROP,
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
