$(document).ready(function () {
    $('.selectitems').select2();
    $("span.select2.select2-container.select2-container--default").each(function () {
        // Agregar la nueva clase deseada
        $(this).addClass("select-form");
    });
});

//$(document).ready(function () {
//    $('.selectitems').select2();
//});

var otablaErroress = $("#tablaErrores").DataTable({
    "scrollCOllapse": true,
    "autoWidth": false,
    processing: true,
    "language":
    {
        paginate:
        {
            first: textoScript.primero,
            previous: textoScript.anterior,
            next: textoScript.siguiente,
            last: textoScript.ultimo
        },
        aria: {
            first: textoScript.inicio,
            previous: textoScript.anterior,
            next: textoScript.siguiente,
            last: textoScript.ultimo
        },
        emptyTable: textoScript.sinDatos,
        info: "",
        search: textoScript.buscar,
        infoFiltered: "",
        infoEmpty: "",
        lengthMenu: `${textoScript.mostrar} _MENU_`,
        searchPlaceHolder: textoScript.buscando,
        processing: `<span style="width:100%;">${textoScript.cargandoRegistros}</br>`,
        "zeroRecords": textoScript.noseencontraron,
    },
    "columns":
        [
            { "data": "descripcion", "name": "descripcion", "autowidth": true },
        ],
    "columnDefs":
        [
            {
                "targets": [0],
                "width": "100%"
            },
        ]
});

function llenarTablaErrores(listado) {
    otablaErroress.clear().draw();
    $.each(listado, function (index, dato) {
        otablaErroress.row.add({ descripcion: dato }).draw();
    });

    $("#modalErrores").modal("show")
}


$("#btn_enviarPrerregistro").on("click", function () {
    var datos = {
        Sector: $("#Sector").val(),
        DirCiudad: $("#Ciudad").val().trim(),
        RFC: $("#RFC").val().trim(),
        Nombre: $("#RazonSocial").val().trim(),
        NomCorto: $("#NombreCorto").val().trim(),
        DirCalle: $("#Calle").val().trim(),
        DirColonia: $("#Colonia").val().trim(),
        DirNumInt: $("#NumInt").val().trim(),
        DirNumExt: $("#NumExt").val().trim(),
        DirCP: $("#CP").val().trim(),
        DirMunicipio: $("#Municipio").val().trim(),
        DirEstado: $("#Estado").val(),
        CorreoContacto: $("#Email").val().trim(),
        TelefonoContacto: $("#Telefono").val().trim(),
        PersonaContacto: $("#NombreContacto").val().trim()
    };

    let validar = validarDatos(datos);
    if (validar.hayErrores) {
        llenarTablaErrores(validar.listaErrores)
    }
    else {
        $.ajax({
            url: urlEnviarPrerregistro,
            method: "POST",
            cache: false,
            beforeSend: function () {
                showLoading();
            },
            complete: function () {
                hideLoading();
            },
            data: {
                datosEmpresa: datos,
                __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
            },
            success: function (respuesta) {
                if (respuesta.success) {
                    alertaConRecarga("success", textoScript.mExitoTitulo, textoScript.mExitoRegistro)
                }
                else {
                    alerta("error", textoScript.mErrorTitulo, textoScript.mErrorRegistro);
                }
            },
            error: function (err) {
                alerta("error", textoScript.mErrorTitulo, textoScript.ErrorGenerico);

            }
        });
    }
})

function validarTexto(texto) {
    // Expresión regular para verificar si el texto contiene solo caracteres alfanuméricos y espacios
    var regex = /^[a-zA-Z0-9\s]+$/;
    return regex.test(texto);
}

function validarDatos(datos) {
    let errores = [];
    if (esCadenaVaciaOBlanca(datos.Nombre)) errores.push("Debe agregar el nombre de su empresa")
    if (esCadenaVaciaOBlanca(datos.NomCorto)) errores.push("Debe agregar un nombre corto/alias para su empresa")
    if (esCadenaVaciaOBlanca(datos.PersonaContacto)) errores.push("Debe agregar un nombre de contacto")
    if (esCadenaVaciaOBlanca(datos.TelefonoContacto)) errores.push("Debe agregar un número de telefono válido")
    if (esCadenaVaciaOBlanca(datos.DirCalle)) errores.push("Debe agregar la calle de su domicilio")
    if (esCadenaVaciaOBlanca(datos.DirColonia)) errores.push("Debe agregar la colonia de su domicilio")
    if (esCadenaVaciaOBlanca(datos.DirNumExt)) errores.push("Debe agregar el número exterior de su domicilio")
    if (esCadenaVaciaOBlanca(datos.DirNumInt)) errores.push("Debe agregar el número interior de su domicilio")
    if (esCadenaVaciaOBlanca(datos.DirMunicipio)) errores.push("Debe agregar el municipio de su domicilio")
    if (esCadenaVaciaOBlanca(datos.DirCiudad)) errores.push("Debe agregar la ciudad de su domicilio")
    if (esCadenaVaciaOBlanca(datos.DirCP)) errores.push("Debe agregar el código postal de su domicilio")

    if (datos.DirCP.length != 5) errores.push("El código postal debe tener 5 números")
    if (datos.TelefonoContacto.length != 10) errores.push("El número de telefono debe tener 10 caracteres")
    if (esEnteroValidoNiCero(datos.Sector)) errores.push("Debe seleccionar un sector")
    if (esEnteroValidoNiCero(datos.DirEstado)) errores.push("Debe seleccionar un estado")
    if(!validarRFCPersonaMoral(datos.RFC)) errores.push("El RFC no cumple con el formato")
    if (!validarEmail(datos.CorreoContacto)) errores.push("Debe ingresar un correo de contacto válido")

    let _hayErrores = errores.length > 0;
    return {
        hayErrores: _hayErrores,
        listaErrores: errores
    }
}

function validarEmail(email) {
    // Expresión regular para validar el formato de una dirección de correo electrónico
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}

function validarRFCPersonaMoral(rfc) {
    // Expresión regular para validar el formato del RFC de persona moral
    const regex = /^[A-Z&Ñ]{3}[0-9]{6}[A-Z0-9]{3}$/;
    return regex.test(rfc);
}


