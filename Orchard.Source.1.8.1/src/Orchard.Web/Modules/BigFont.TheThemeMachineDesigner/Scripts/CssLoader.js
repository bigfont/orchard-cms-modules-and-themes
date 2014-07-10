﻿/*global $: false*/

(function () {

    "use strict";

    function addTracing() {

        function addCheckboxes(id) {

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
                    $("#layout-wrapper").addClass(id);                    
                } else {
                    $("#layout-wrapper").removeClass(id);
                }

                // persist
                localStorage.setItem(id, this.checked);
            });
        }

        function loadPersisted(id)
        {
            // persist
            if (localStorage.getItem(id) === "true") {
                $("#" + id)
                    .prop("checked", true)
                    .change();
            }
        }

        var trace = [
                "trace-zones",
                "trace-widgets",
                "trace-content-items",
                "trace-topography",
                "trace-class-and-id"];

        /*jslint unparam: true*/
        $.each(trace, function (index, value) {

            addCheckboxes(value);
            addEvents(value);
            loadPersisted(value);

        });
        /*jslint unparam: false*/

    }

    $(function () {

        addTracing();

    });

}());
