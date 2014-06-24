(function () {


    var locations;

    locations = [

// TODO Store these in the database, 
// and make them editable from the Orchard UI.

['100 Mile House, BC', 51.6439705, -121.2950097],
['Adelaide, Australia', -34.9286212, 138.5999594],
['Anacortes, WA', 48.5126045, -122.6126718],
['Antigonish, NS', 45.6226605, -61.9928025],
['Armdale, Australia', -30.5143425, 151.6669644],
['Athione, Ireland', 53.4239331, -7.9406898],
['Austin, TX', 30.267153, -97.7430608],
['Austria', 47.516231, 14.550072],
['Banff, AB', 51.1783629, -115.5707694],
['Barrie, ON', 44.3780902, -79.7016159],
['Battle Ground, WA', 45.7809491, -122.5334307],
['Leiptig, Germany', 51.3396955, 12.3730747],
['Beaver Mines, AB', 49.465779, -114.19194],
['Belfast, Northern Ireland', 54.597285, -5.93012],
['Belgium', 50.503887, 4.469936],
['Bellevue, WA', 47.610377, -122.2006786],
['Berrian, MO', 37.9642529, -91.8318334],
['Black Creek, Regina', 50.4728067, -104.6855817],
['Blaine, WA', 48.993723, -122.7471191],
['Boise, ID', 43.6187102, -116.2146068],
['Bologna, Italy', 44.494887, 11.3426163],
['Bowen Island, BC', 49.3767653, -123.3701541],
['Brandon, MB', 49.848471, -99.9500904],
['Breensboro, North Carolina', 36.0726354, -79.7919754],
['Bristol, Enland', 51.454513, -2.58791],
['Camano Island, WA', 48.1870556, -122.5078469],
['Campbell River, BC', 50.0331226, -125.2733354],
['Jasper, AB', 52.879277, -118.079256],
['Canmore, AB', 51.086985, -115.346814],
['Cardiff, Wales', 51.481581, -3.17909],
['Channel Islands', 49.2166667, -2.1325],
['Chilliwack, BC', 49.1579401, -121.9514666],
['Cobble Hill, Ladysmith, BC', 48.990429, -123.821098],
['Collingwood, ON', 44.5007687, -80.2169047],
['Cologne, Germany', 50.937531, 6.9602786],
['Colorado', 39.5500507, -105.7820674],
['Columbus, Ohio', 39.9611755, -82.9987942],
['Copenhagen', 55.6760968, 12.5683371],
['Copper Cliff, ON', 46.47544, -81.069119],
['Coquitlam, BC', 49.2837626, -122.7932065],
['Cork, Ireland', 51.8968917, -8.4863157],
['Cowichan, BC', 48.7817906, -123.6695808],
['Cranbrook, BC', 49.5129678, -115.7694002],
['Darlaston, England', 52.567544, -2.032828],
['Dartmouth, NS', 44.6652059, -63.5677427],
['Denmark, Dublin', 53.3020324, -6.2295806],
['Devan, England', 50.7772135, -3.999461],
['Dinsbury, AB', 51.6608359, -114.136516],
['Dublin', 53.3498053, -6.2603097],
['Edmonds, WA', 47.8106521, -122.3773552],
['Edmonton, AB', 53.544389, -113.4909267],
['El Paso, TX', 31.7587198, -106.4869314],
['Eugene, OR', 44.0520691, -123.0867536],
['New Westminster, BC', 49.2057179, -122.910956],
['Fort St.James, BC', 54.443649, -124.254097],
['France', 46.227638, 2.213749],
['French Beach, BC', 48.3949504, -123.943074],
['Gabriola Island, BC', 49.1650852, -123.7959878],
['Gates, OR', 44.7562329, -122.4167483],
['Glenora, BC', 57.837863, -131.386695],
['Godeneh, ON', 45.2820988, -75.6801832],
['Gotebourg, Sweden', 57.70887, 11.97456],
['Hamilton, ON', 43.2500208, -79.8660914],
['High River, AB', 50.5801021, -113.8707312],
['Hoechst, Austria', 47.4592718, 9.6356383],
['Holland', 52.132633, 5.291266],
['Houston, TX', 29.7601927, -95.3693896],
['Ireland', 53.41291, -8.24389],
['Israel', 31.046051, 34.851612],
['Italy', 41.87194, 12.56738],
['Japan', 36.204824, 138.252924],
['Jasper, AB', 52.879277, -118.079256],
['Sechelt, BC', 49.4741736, -123.7545601],
['Kamloops, BC', 50.674522, -120.3272674],
['Kelowna, BC', 49.8879519, -119.4960106],
['Kentville, NS', 45.0769115, -64.4944735],
['Kitchener, ON', 43.434311, -80.4777469],
['Elora, ON', 43.683303, -80.430751],
['Lake Cowichan, BC', 48.8258118, -124.054167],
['Landshort, Germany', 48.5392247, 12.1459218],
['Leslieville, AB', 52.384329, -114.601406],
['Lincoln, England', 53.230688, -0.540579],
['London, England', 51.5112139, -0.1198244],
['Madison, WI', 43.0730517, -89.4012302],
['Maple Bay, BC', 48.817237, -123.61511],
['Maple Ridge, BC', 49.2193226, -122.598398],
['Whistler, BC', 50.1163196, -122.9573563],
['Mission, BC', 49.200282, -122.420248],
['Parksville, BC', 49.3193375, -124.3136412],
['MapleBay, BC', 48.817237, -123.61511],
['Mattawa, ON', 46.31748, -78.702193],
['Guelph, ON', 43.5448048, -80.2481666],
['Melbourne, Australia', -37.814107, 144.96328],
['Merritt, BC', 50.1113079, -120.7862222],
['Mill Bay, BC', 48.6505297, -123.5571899],
['Minneapolis, Minnisota', 44.983334, -93.26667],
['Missoula, Montana', 46.8605189, -114.019501],
['Montana', 46.8796822, -110.3625658],
['Mount Shasta, CA', 41.3098746, -122.3105666],
['Mudge Island', 49.1317719, -123.788954],
['Muskoka, ON', 44.901642, -79.575821],
['Nanaimo, BC', 49.1658836, -123.9400647],
['New England', 43.9653889, -70.8226541],
['Newfoundland', 53.1355091, -57.6604364],
['NewYork, New York', 40.7143528, -74.0059731],
['New Zealand', -40.900557, 174.885971],
['Niagara on the Lake, ON', 43.2549988, -79.0772616],
['Huntsville, ON', 45.3269323, -79.2167539],
['Norquay, Saskatchewan', 51.882039, -102.0882845],
['North Carolina', 35.7595731, -79.0192997],
['North Vancouver, BC', 49.319647, -123.068237],
['Oakland, CA', 37.8043637, -122.2711137],
['Oklahoma', 35.0077519, -97.092877],
['Olympia, WA', 47.0378741, -122.9006951],
['Onanole, MB', 50.6229579, -99.968798],
['Orcas Island, WA', 48.6597887, -122.8457029],
['Paris, France', 48.856614, 2.3522219],
['Pemberton, BC', 50.322028, -122.8050498],
['Pender Island, BC', 48.7758765, -123.2556152],
['Penticton, BC', 49.4991381, -119.5937077],
['Perth, Australia', -31.9530044, 115.8574693],
['Port Angeles, WA', 48.118146, -123.4307413],
['Maine', 45.253783, -69.4454689],
['Portland, OR', 45.5234515, -122.6762071],
['Port Townsend, WA', 48.1170387, -122.7604472],
['Qualicum Beach, BC', 49.3482346, -124.4428262],
['Quebec City, QC', 46.8032826, -71.242796],
['Queensland, Australia', -20.9175738, 142.7027956],
['Red Deer, AB', 52.2681118, -113.8112386],
['Brooks, AB', 50.5659752, -111.8991668],
['Redlands, CA', 34.0555693, -117.1825381],
['Regensberg, Germany', 49.0145423, 12.1008559],
['Rocky Rapids, AB', 53.2800447, -114.9528016],
['Rosenberg, OR', 45.7406716, -119.2795336],
['Russian Federation', 61.52401, 105.318756],
['Sacrament, CA', 38.5815719, -121.4943996],
['San Diego, CA', 32.7153292, -117.1572551],
['San Fransisco, CA', 37.7749295, -122.4194155],
['Santa Cruz, CA', 36.9741171, -122.0307963],
['Sarnia, ON', 42.974536, -82.4065901],
['Saskatoon, Saskatchewan', 52.1343699, -106.647656],
['Saturna, BC', 48.797607, -123.200556],
['Scotch Village, NS', 45.057001, -64.001802],
['Scottland', 56.4906712, -4.2026458],
['Seattle, WA', 47.6062095, -122.3320708],
['Shawville, Quebec City', 45.603663, -76.4914639],
['Sherbrook, QC', 45.4009928, -71.8824288],
['Shiga, Japan', 35.0045306, 135.8685899],
['Slocan Valley, BC', 49.5087567, -117.6184461],
['Smithers, BC', 54.782355, -127.1685541],
['Sooke, BC', 48.3761111, -123.7377778],
['South Africa', -30.559482, 22.937506],
['St Waskberg, Saskatchewan', 52.9399159, -106.4508639],
['St. Paul, Minnesota', 44.9537029, -93.0899578],
['Langley, BC', 49.0743308, -122.5593218],
['Burnaby, BC', 49.2294908, -123.0025753],
['Surrey, BC', 49.186495, -122.823134],
['Station, BC', 49.2403059, -122.96819],
['Bristoll, England', 51.454513, -2.58791],
['Switzerland', 46.818188, 8.227512],
['Sydney, Australia', -33.8674869, 151.2069902],
['Sylvan Lake, AB', 52.3076201, -114.0979947],
['Tacoma, WA', 47.2528768, -122.4442906],
['Tasmania', -41.3650419, 146.6284905],
['Texada Island', 49.6596634, -124.4121947],
['Thunder Bay, ON', 48.3808951, -89.2476823],
['Ucluelet, BC', 48.9415997, -125.5463445],
['Uxbridge, ON', 44.1094028, -79.1204998],
['Visalla, CA', 36.3302284, -119.2920585],
['Wales', 52.1306607, -3.7837117],
['Warrnambool, Australia', -38.3827659, 142.4844995],
['Waterville, MA', 47.6470761, -120.0711788],
['White Rock, BC', 49.022034, -122.803643],
['Whitecourt, AB', 54.1424043, -115.6849812],
['Whitehorse, YT', 60.7211871, -135.0568449],
['Toronto, ON', 43.653226, -79.3831843],
['Blaine, WA', 48.993723, -122.7471191],
['Windsor, ON', 42.279609, -83.0085329],
['Yellowknife, NT', 53.7266683, -127.6476206]

    ];

    function sortList(mylist) {
        var listitems = mylist.children('li').get();
        listitems.sort(function (a, b) {
            return $(a).text().toUpperCase().localeCompare($(b).text().toUpperCase());
        })
        $.each(listitems, function (idx, itm) { mylist.append(itm); });
    }

    function addLocationToLocationList(locationList, location) {
        var locationElement, separatorElement;
        locationElement = $('<li/>', { text: location });
        separatorElement = $('<i/>', { 'class': 'icon-leaf' });
        locationElement.prepend(separatorElement);
        $(locationList).append(locationElement);
    }

    function plotMarker(map, position, title) {
        var marker = new google.maps.Marker({
            position: position,
            title: title,
            map: map
        });
        return marker;

    }

    function plotMultipleMarkers(map, locationList) {

        var location, position, marker;

        for (var i = 0; i < locations.length; i++) {
            location = locations[i];
            position = new google.maps.LatLng(location[1], location[2]);
            plotMarker(map, position, location[0]);
            addLocationToLocationList(locationList, location[0]);
        }
        sortList(locationList);

    }

    function getLocationList() {
        var locationList;
        locationList = $('#location-list');
        return locationList;
    }

    function plotMap() {
        var mapOptions, map, center;

        // Change the center based on the location that you want to display       
        center = new google.maps.LatLng(35, 0);

        // TODO Do no repeat myself here.
        mapOptions = {
            zoom: 1,
            zoomControl: true,
            scrollwheel: false,
            disableDoubleClickZoom: true,
            center: center,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            disableDefaultUI: true
        };
        map = new google.maps.Map(document.getElementById('map-multiple-markers-canvas'),
        mapOptions);

        return map;
    }

    function initialize() {

        var map;

        map = plotMap();

        locationList = getLocationList();

        plotMultipleMarkers(map, locationList);


    }

    $(document).ready(function () {

        initialize();

    });

}());

