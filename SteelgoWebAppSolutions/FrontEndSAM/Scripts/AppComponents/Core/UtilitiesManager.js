﻿/********************************************/
/********************************************/
/*********                          *********/
/*********    Utilities Manager     *********/
/*********                          *********/
/********************************************/
/********************************************/

/****************************/
/*    Global Variables      */
/****************************/

//Add all global variables for your partial view here

/****************************/
/*    Document Ready        */
/****************************/

//Method to be called on the document ready and contains all the pertinent code for a partial view
function utilitiesManagerToBeExecutedOnDocumentReady() {
    //CODE
}

/****************************/
/*    Global Functions      */
/****************************/

//Function to obtain an url parameter if is not present then return the notFindValue parameter
function getUrlParameter(sParam, notFindValue) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
    return notFindValue;
}

//Function to validate the existence of an element
function validateElementExistence(element) {
    if (typeof (element) !== 'undefined'
            && element != null) {
        return true;
    }
    return false;
}

//Function to activate waiting screen
function loadingStart() {
    $body.addClass("loading");
}

//Function to deactivate waiting screen
function loadingStop() {
    $body.removeClass("loading");
}

//Function to encrypt base64 a string
function encrypt(toEncodData) {
    var encodedData = window.btoa(toEncodData);
    if (encodedData[encodedData.length - 1] == '=') {
        encodedData = encodedData.substr(0, encodedData.length - 1);
    }
    return encodedData;
}

//Function to decrypt base64 a string
function decrypt(encodedData) {
    if (encodedData[encodedData.length - 1] != '=') {
        encodedData = encodedData + '=';
    }
    return window.atob(encodedData);
}

//Function to show or hide the side menu
function toggleSideMenu() {
    if (Cookies.get("navegacion") != null && Cookies.get("navegacion") != "1" && Cookies.get("navegacion") != "2") {
        $("ul").removeClass("open");
        $("i.arrow").removeClass("down");
        $("#sidebar").toggleClass("minified");
        $(".content-container").toggleClass("expanded");
    }
}

//Function to redirect properly with the specified language
function redirectToLanguage(event,link) {
    event.preventDefault();
    window.location.href = link.href + "?leng=" + $("#language").data("kendoDropDownList").value();
}

//Function to remove any grid
function removeGrid(grid) {
    var tmp = [];
    try {
        tmp = grid.data("kendoGrid").dataSource.data() || grid.data("kendoGrid").options.dataSource.data()
    } catch (e) { }
    var contenedor = grid.parent();
    grid.remove();
    contenedor.append("<div id='" + grid.attr("id") + "'></div>");
    return tmp;
}

function messageindexKendoCombobox(obj) {
    if (obj.selectedIndex == -1) {
        displayMessage("notificationslabel0083", "", '1');
    };
};

//Ajusta el tamaño de los filtros rápidos a las columnas del grid
//Funcion: resizeFilters
//Parametros: N/A
//Return:     N/A
/*function resizeFilters() {   
    $("#grid .k-grid-header tr th:visible").each(function (i, obj) {
        var wd = 0;
        wd = $(this).outerWidth(true);
        console.log("th: "+i+" width: "+$(this).outerWidth(true));
        $("#filterContainer > div").find("input").each(function (index, event) {
            if (i == index) {
                $(this).outerWidth(wd + 1.4);
                console.log("qf: " + index + " width: " + $(this).outerWidth(true));
            }
        });
    });
    $(".quickFilters:first-of-type").outerWidth($(".quickFilters:first-of-type").outerWidth() + 0.6);

    $(window).resize(function () {
        $("#grid .k-grid-header tr th").each(function (i, obj) {
            var wd = 0
            wd = $(this).outerWidth(true);
            console.log("th: "+i+" width: "+$(this).outerWidth(true));
            $("#filterContainer > div").find("input").each(function (index, event) {
                if (i == index) {
                    $(this).outerWidth(wd + 1.4);
                    console.log("qf: " + index + " width: " + $(this).outerWidth(true));
                }
            });
        });
        $(".quickFilters:first-of-type").outerWidth($(".quickFilters:first-of-type").outerWidth() + 0.6);
    });
}*/