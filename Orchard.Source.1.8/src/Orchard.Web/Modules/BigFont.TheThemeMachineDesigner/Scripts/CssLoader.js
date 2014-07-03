(function () {

    // see http://www.gotknowhow.com/articles/how-to-get-the-base-url-with-javascript
    function getBaseURL() {
        var url = location.href;  // entire url including querystring - also: window.location.href;
        var baseURL = url.substring(0, url.indexOf('/', 14));


        if (baseURL.indexOf('http://localhost') != -1) {
            // Base Url for localhost
            var url = location.href;  // window.location.href;
            var pathname = location.pathname;  // window.location.pathname;
            var index1 = url.indexOf(pathname);
            var index2 = url.indexOf("/", index1 + 1);
            var baseLocalUrl = url.substr(0, index2);

            return baseLocalUrl + "/";
        }
        else {
            // Root Url for domain name
            return baseURL + "/";
        }

    }

    function AddTracing() {

        function LinkStyleSheet() {
            var cssPath = getBaseURL() + "/Modules/BigFont.TheThemeMachineDesigner/Styles/module.css";
            $('head').append('<link rel="stylesheet" href=' + cssPath + ' type="text/css" data-trace />');
        }

        function AddEventListeners() {
            var trace = ["trace-zones", "trace-widgets", "trace-topography", "trace-class-and-id"];

            $.each(trace, function (index, value) {
                $("#" + value).change(function (e) { $("body").toggleClass(value); });
            });
        }

        function SynchronizeSomeFunctionality() {
            $("#trace-class-and-id").change(function (e) {
                $("#trace-topography")
                    .prop("checked", this.checked)
                    .change();
            });
        }

        LinkStyleSheet();
        AddEventListeners();
        SynchronizeSomeFunctionality();
    }

    $(function () {

        AddTracing();

    });

}(location));

