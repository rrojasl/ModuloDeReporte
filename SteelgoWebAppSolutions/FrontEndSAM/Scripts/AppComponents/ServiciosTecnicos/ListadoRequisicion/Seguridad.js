﻿
Cookies.set("home", true, { path: '/' });
Cookies.set("navegacion", "43", { path: '/' });

var $ListadoRequisicionModel = {
    listContainer: {
        create: "",
        list: "",
        detail: "",
        destroy: ""
    },
    properties: {

        btnAgregarRequisicion: {
            visible: "#AgregarRequisicionDiv",
            editable: "#btnAgregarRequisicion",
        },
        grid: {
            visible: "#ContenedorGrid",
            editable: "#grid",
        }
    }
};