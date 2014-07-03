/*global $: false*/

(function () {

    "use strict";

    function addTracing() {

        function addControls(id) {

            var text,
                label,
                input;

            text = id.replace('trace-', '');
            label = $('<label/>', { for: id, text: text });
            input = $('<input/>', { id: id, type: 'checkbox' });
            $("#theme-designer-control-bar > form")
                .append(input)
                .append(label);
        }

        function addEvents(id) {
            $("#" + id).change(function () {
                if (this.checked) {
                    $("body").addClass(id);
                } else {
                    $("body").removeClass(id);
                }
            });
        }

        var trace = [
                "trace-zones",
                "trace-widgets",
                "trace-content-items",
                "trace-topography",
                "trace-class-and-id"];

        /*jslint unparam: true*/
        $.each(trace, function (index, value) {

            addControls(value);
            addEvents(value);

        });
        /*jslint unparam: false*/

    }

    $(function () {

        addTracing();

    });

}());
