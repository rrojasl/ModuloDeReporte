﻿var ObjetoRenglon, IndiceRenglon;
var endRangeDate;
var TemplateBtnEnviar = "<button  type='button' class='btn btn-blue botonEnviar'> <span>Enviar</span></button>";
var reportePath;
var reporteID;


function changeLanguageCall() {
    AltaFecha();
    endRangeDate.data("kendoDatePicker").setOptions({
        format: _dictionary.FormatoFecha[$("#language").data("kendoDropDownList").value()]
    });
    CargarGrid();
    AjaxCargarListadoEmbarque('Enviados', $("#language").val());
    AjaxCargarPath();
};


function CargarGrid() {


    $("#grid").kendoGrid({
        autoBind: true,
        dataSource: {
            data: [],
            schema: {
                model: {
                    fields: {
                        EmbarqueID: { type: "string", editable: false },
                        Plana: { type: "string", editable: false },
                        Proyecto: { type: "string", editable: false },
                        PapelesCliente: {  editable: false },
                        paplelesAduana: { editable: false },
                        FolioSolicitarPermisos: { editable: false },
                        FolioAprobadoAduana: { editable: false },
                        FolioAprobadoCliente: { editable: false },
                        Enviar: {editable: false}
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        navigatable: true,
        filterable: {
            extra: false
        },
        
        editable: true,
        autoHeight: true,
        sortable: true,
        scrollable: true,
        pageable: {
            refresh: false,
            pageSizes: [10, 15, 20],
            info: false,
            input: false,
            numeric: true,
        },
        columns: [
            { field: "EmbarqueID", title: _dictionary.ListadoEmbarqueHeaderEmbarque[$("#language").data("kendoDropDownList").value()], filterable: true, width: "125px" },
            { field: "Plana", title: _dictionary.ListadoEmbarqueHeaderPlana[$("#language").data("kendoDropDownList").value()], filterable: true, width: "100px" },
            { field: "Proyecto", title: _dictionary.ListadoEmbarqueHeaderProyecto[$("#language").data("kendoDropDownList").value()], filterable: true, width: "125px" },
            { field: "PapelesCliente", title: _dictionary.ListadoEmbarqueHeaderPapelesCliente[$("#language").data("kendoDropDownList").value()], filterable: true, template: "<button  type='button' class='btn btn-blue imprimirPapelesCliente'> <span>" + _dictionary.ListadoEmbarqueBotonImprimir[$("#language").data("kendoDropDownList").value()] + "</span></button>", width: "125px" },
            { field: "paplelesAduana", title: _dictionary.ListadoEmbarqueHeaderPapelesAduana[$("#language").data("kendoDropDownList").value()], filterable: true, template: "<button  type='button' class='btn btn-blue imprimirPapelesAduana'> <span>" + _dictionary.ListadoEmbarqueBotonImprimir[$("#language").data("kendoDropDownList").value()] + "</span></button>", width: "125px" },
            { field: "FolioSolicitarPermisos", title: _dictionary.ListadoEmbarqueHeaderSolicitarPermisos[$("#language").data("kendoDropDownList").value()], filterable: true, template: "<button  type='button' class='btn btn-blue botonFolio' Style='display: #= FolioSolicitarPermisos!='' ? 'none;' : 'block;' #'> <span>" + _dictionary.ListadoEmbarqueBotonCapturar[$("#language").data("kendoDropDownList").value()] + "</span></button><span>#= FolioSolicitarPermisos #</span>", width: "125px" },
            { field: "FolioAprobadoAduana", title: _dictionary.ListadoEmbarqueHeaderAprobadoAduana[$("#language").data("kendoDropDownList").value()], filterable: true, template: "<button  type='button' class='btn btn-blue botonFolio' Style='display: #= FolioAprobadoAduana!='' ? 'none;' : 'block;' #' > <span>" + _dictionary.ListadoEmbarqueBotonCapturar[$("#language").data("kendoDropDownList").value()] + "</span></button><span>#= FolioAprobadoAduana #</span>", width: "125px" },
            { field: "FolioAprobadoCliente", title: _dictionary.ListadoEmbarqueHeaderAprobadoCliente[$("#language").data("kendoDropDownList").value()], filterable: true, template: "<button  type='button' class='btn btn-blue botonFolio' Style='display: #= FolioAprobadoCliente!='' ? 'none;' : 'block;' #'> <span>" + _dictionary.ListadoEmbarqueBotonCapturar[$("#language").data("kendoDropDownList").value()] + "</span></button><span>#= FolioAprobadoCliente #</span>", width: "125px" },
            { field: "Enviar", title: " ", filterable: true, template: "<button  type='button' class='btn btn-blue botonEnviar'  Style='display: #= RequierePermisoAduana ? 'none;' : 'block;' #' > <span>" + _dictionary.ListadoEmbarqueBotonEnviar[$("#language").data("kendoDropDownList").value()] + "</span></button>", width: "125px" },
        ]
    });
};

function VentanaModalFecha(dataItem) {
    ObjetoRenglon = dataItem
    var modalTitle = "";
    modalTitle = _dictionary.ValidacionResultadosRequisicion[$("#language").data("kendoDropDownList").value()];
    var window = $("#windowFecha");
    var win = window.kendoWindow({
        modal: true,
        title: modalTitle,
        resizable: false,
        visible: true,
        width: "400px",
        minWidth: 30,
        position: {
            top: "1%",
            left: "1%"
        },
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
    }).data("kendoWindow");
    window.data("kendoWindow").title(modalTitle);
    window.data("kendoWindow").center().open();

};


function VentanaModalFolio(dataItem,indexItem) {
    ObjetoRenglon = dataItem;
    IndiceRenglon = indexItem;
    var modalTitle = "";
    modalTitle = "";
    var window = $("#windowFolio");
    var win = window.kendoWindow({
        modal: true,
        title: modalTitle,
        resizable: false,
        visible: true,
        width: "300px",
        minWidth: 30,
        position: {
            top: "1%",
            left: "1%"
        },
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
    }).data("kendoWindow");
    window.data("kendoWindow").title(modalTitle);
    window.data("kendoWindow").center().open();

};

function AltaFecha() {
    endRangeDate = $("#Fecha").kendoDatePicker({
        value: new Date()
    });
}