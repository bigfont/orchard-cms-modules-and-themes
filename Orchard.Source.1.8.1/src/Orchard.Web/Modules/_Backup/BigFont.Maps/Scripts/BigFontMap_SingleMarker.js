(function () {

    function initialize() {

        var map;

        map = plotMap();

    }

    function plotMap() {
        var mapOptions, map, center, latitude, longitude;

        latitude = $('input#latitude').val();
        longitude = $('input#longitude').val();

        center = new google.maps.LatLng(latitude, longitude);

        // TODO Do no repeat myself here.
        mapOptions = {
            zoom: 14,
            zoomControl: true,
            scrollwheel: false,
            disableDoubleClickZoom: true,
            center: center,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            disableDefaultUI: true

        };
        map = new google.maps.Map(document.getElementById('map-single-marker-canvas'),
        mapOptions);

        plotMarker(map, center, 'Garden Faire Campground');

        return map;
    }

    function plotMarker(map, position, title) {
        var marker = new google.maps.Marker({
            position: position,
            title: title,
            map: map
        });
        return marker;

    }

    $(document).ready(function () {

        initialize();

    });

}());

