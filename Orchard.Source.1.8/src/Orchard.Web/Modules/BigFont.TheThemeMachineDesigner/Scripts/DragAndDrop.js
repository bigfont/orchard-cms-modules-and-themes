/*global $:false, window: false, document: false */

(function (window, document) {

    "use strict";

    var draggedElementId = 'theme-designer-control-bar';

    function drag_start(event) {
        var style, top, left, startPoint;
        style = window.getComputedStyle(event.target, null);

        // get the original position
        top = (parseInt(style.getPropertyValue("top"), 10) - event.clientY);
        left = (parseInt(style.getPropertyValue("left"), 10) - event.clientX);
        startPoint = left + ',' + top;

        // send it to the drop event
        event.dataTransfer.setData("text/plain", startPoint);
    }
    function drag_over(event) {
        event.preventDefault();
        return false;
    }
    function drop(event) {
        var startPoint, dm, top, left;

        startPoint = event.dataTransfer.getData("text/plain").split(',');
        dm = document.getElementById(draggedElementId);

        // calculate and set the new position
        left = (event.clientX + parseInt(startPoint[0], 10));
        top = (event.clientY + parseInt(startPoint[1], 10));
        dm.style.left = left + 'px';
        dm.style.top = top + 'px';

        // prevent default
        event.preventDefault();
        return false;
    }

    $(function () {
        var dm = document.getElementById(draggedElementId);
        dm.addEventListener('dragstart', drag_start, false);
        document.body.addEventListener('dragover', drag_over, false);
        document.body.addEventListener('drop', drop, false);
    });
}(window, document));
