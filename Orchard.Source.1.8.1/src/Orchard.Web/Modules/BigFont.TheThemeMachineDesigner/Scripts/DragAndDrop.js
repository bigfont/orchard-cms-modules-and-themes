/*global $:false, window: false, document: false, localStorage: false */

(function (window, document, localStorage) {

    "use strict";

    var draggedElementId,
        draggedElement,
        localStorageId;

    draggedElementId = 'theme-designer-control-bar';
    localStorageId = 'data-' + draggedElementId;

    function moveElement(element, point) {
        element.style.left = point.left + 'px';
        element.style.top = point.top + 'px';
    }

    function drag_start(event) {
        var style, top, left, startPoint;
        style = window.getComputedStyle(event.target, null);

        // get the original position
        top = (parseInt(style.getPropertyValue("top"), 10) - event.clientY);
        left = (parseInt(style.getPropertyValue("left"), 10) - event.clientX);
        startPoint = {
            top: top,
            left: left
        };

        // send it to the drop event
        event.dataTransfer.setData("text/plain", JSON.stringify(startPoint));
    }
    function drag_over(event) {
        event.preventDefault();
        return false;
    }
    function drop(event) {
        var startPoint, endPoint, top, left;

        startPoint = JSON.parse(event.dataTransfer.getData("text/plain"));

        // calculate new position
        left = (event.clientX + parseInt(startPoint.left, 10));
        top = (event.clientY + parseInt(startPoint.top, 10));
        endPoint = {
            left: left,
            top: top
        };

        // move        
        moveElement(draggedElement, endPoint);

        // save
        localStorage.setItem(localStorageId, JSON.stringify(endPoint));

        // prevent default
        event.preventDefault();
        return false;
    }
    $(function () {
        var dropArea, savedPosition;

        draggedElement = document.getElementById(draggedElementId);
        draggedElement.addEventListener('dragstart', drag_start, false);

        dropArea = document.body;
        dropArea.addEventListener('dragover', drag_over, false);
        dropArea.addEventListener('drop', drop, false);

        savedPosition = JSON.parse(localStorage.getItem(localStorageId));
        if (savedPosition) {
            moveElement(draggedElement, savedPosition);
        }
    });
}(window, document, localStorage));
