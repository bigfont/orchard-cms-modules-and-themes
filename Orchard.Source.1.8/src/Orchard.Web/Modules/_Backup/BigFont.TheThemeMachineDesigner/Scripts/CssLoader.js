$(function () {    
    var cssPath = "/Modules/BigFont.TheThemeMachineDesigner/Styles/module.css";

    $('head').append('<link rel="stylesheet" href=' + cssPath + ' type="text/css" />');

    console.log('Appended to head: ' + cssPath);
});