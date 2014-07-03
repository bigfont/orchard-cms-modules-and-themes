(function () {

    "use strict";

    function AddTracing() {

        function AddControls(id) {
            var text = id.replace('trace-', '');
            var label = $('<label/>', { for: id, text: text });
            var input = $('<input/>', { id: id, type: 'checkbox' });
            $("#theme-designer-control-bar > form")
                .append(input)
                .append(label);
        }

        function AddEvents(id) {
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

        $.each(trace, function (index, value) {

            AddControls(value);
            AddEvents(value);

        });

    }

    $(function () {

        AddTracing();

    });

}(location));
