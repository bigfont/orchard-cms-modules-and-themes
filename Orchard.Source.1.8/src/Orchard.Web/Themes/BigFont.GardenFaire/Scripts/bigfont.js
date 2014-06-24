(function () {

    "use strict";
    /*jslint browser:true */
    /*global $ */

    function wrapElementWithLink(selector, linkTarget) {        
        var element = $(selector);
        element.wrap('<a href="' + linkTarget + '" />');
    }    

    function addReturnToTopButton() {

        var widgetWithHeader, returnToTopContainer, returnToTopLink, lastHorizontalRule;
        returnToTopContainer = $('<div/>', { 'class': 'return-to-top' });
        returnToTopLink = $('<a/>', {
            text: 'Return to Top',
            'class': 'btn btn-info',
            href: '#'
        });

        returnToTopContainer.append(returnToTopLink);

        // add before widgets with header
        widgetWithHeader = $('article.widget:has(header):not(:first)');
        returnToTopContainer.clone().insertBefore(widgetWithHeader.find('header'));

        // also add to the end of the zone-after-content
        returnToTopContainer.clone().addClass('return-to-top-last').insertAfter('div.zone-after-content');

        // add just before the final horizontal rule of layout-content
        lastHorizontalRule = $('div#layout-content > hr');
        returnToTopContainer.clone().addClass('visible-phone').insertBefore(lastHorizontalRule);

    }

    function setAffixWidth() {
        // ensure the affix element maintains it width
        var affix, width;
        affix = $('.affixed-element');
        width = affix.width();
        affix.width(width);
    }

    function setupAdminSignIn() {
        var container, link, div, username, html;
        container = $('div#theSignIn');
        link = $('a#showSignIn');
        div = $('div#doSignIn');
        html = $('html');
        username = $('input#username-email');
        // on click anywhere in the theSignIn container
        container.children().click(function (event) {
            // prevent triggering the html click event
            event.stopPropagation();
            // show the sign in form, and hide the clicked link
            link.hide();
            div.show(function () {
                // move cursor focus to the username input
                username.focus();
                // setup an html click event that lasts for only one click
                html.one('click', function () {
                    // Hide the menus
                    link.show();
                    div.hide();
                });
            });
        });
    }

    $(document).ready(function () {

        setAffixWidth();

        addReturnToTopButton();

        setupAdminSignIn();

        wrapElementWithLink('article.widget-mouat-park-hero-unit img', '/Media/Default/OtherImages/mouat_park_trails_900x600.jpg');
    });

}());
