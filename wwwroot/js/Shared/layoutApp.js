

const sidebar = document.getElementById("sidebar");

//document.addEventListener('DOMContentLoaded', function () {
//    $('.selectpicker').select2();
//    $("span.select2.select2-container.select2-container--default").each(function () {
//        // Agregar la nueva clase deseada
//        $(this).addClass("select-form");
//    });

//    $("#sidebar-shadow").on("click", function () {
//        alert('h')
//        ocultarSidebar();
//    });
//});

$(document).ready(function () {
    $('.selectitems').select2({
        //dropdownParent: $('#modalAgregar')
        dropdownParent: $('.modal-selectitems')
    });
    //$("#sidebar-container").on("click", function () {
    //    alert('h')
    //    ocultarSidebar();
    //});
    const sidebarContainer = document.getElementById('sidebar-container');
    const sidebar = document.getElementById('sidebar');

    sidebarContainer.addEventListener('click', (e) => {
        // Verificar si el clic se hizo fuera del sidebar
        if (!sidebar.contains(e.target)) {
            ocultarSidebar();
        }
    });

});

function llenarSelect(id, data) {
    $(`#${id}`).empty();
    $(`#${id}`).append($('<option>', {
        value: "0",
        text: "-  Seleccione una opción"
    }));
    $.each(data, function (i, opcion) {
        $(`#${id}`).append($('<option>', {
            value: opcion.id,
            text: opcion.descripcion
        }));
    });
    // Finalmente, llama al método select2() para actualizar el plugin
    $(`#${id}`).select2({
        //dropdownParent: $('#modalAgregar')
        dropdownParent: $('.modal-selectitems')
    });
    //$('.select2-selection__rendered').css('display', 'none');
}

function ocultarSidebar() {
    sidebar.classList.add("hide");
    sidebar.classList.remove("show");
    $('#sidebar-container').hide();
}

$('#close-sidebar-btn').on('click', function () {
    
    ocultarSidebar();
})

$('#botonMenu').on('click', function () {
    $('#sidebar-container').show()
    sidebar.classList.add("show");
    sidebar.classList.remove("hide");
})

// Agregar un event listener para el evento 'click'
function toggleIconosCheckbox(checkboxId, icono1Id, icono2Id) {
    var checkbox = document.getElementById(checkboxId);
    var icono1 = document.getElementById(icono1Id);
    var icono2 = document.getElementById(icono2Id);

    if (checkbox.checked) {
        icono1.style.display = "none";
        icono2.style.display = "inline";
    } else {
        icono1.style.display = "inline";
        icono2.style.display = "none";
    }
}

function mostrarAnioActual(id) {
    // Obtener el elemento con el ID "anioActual"
    var elementoAnio = document.getElementById(id);

    // Obtener el año actual
    var anioActual = new Date().getFullYear();

    // Asignar el año actual al elemento
    elementoAnio.textContent = anioActual;
}

function getMesActual() {
    var mesActual = new Date().getMonth();
    return mesActual + 1;
}

function getAnioActual() {
    var anio = new Date().getFullYear();
    return anio;
}



function transformarFecha_aaaaMMdd(fecha) {
    if (fecha == "null" || fecha == null) return '';

    //console.log("fecha",fecha)
    // Verificar si la fecha está en formato "yyyy-MM-dd"
    if (/^\d{4}-\d{2}-\d{2}$/.test(fecha)) {
        return fecha; // Devolver la fecha sin modificar
    }

    // Dividir la fecha en día, mes y año
    var partes = fecha.split('/');
    var dia = partes[0];
    var mes = partes[1];
    var año = partes[2];

    // Formar la fecha en el formato deseado "aaaa-MM-dd"
    var fechaTransformada = año + '-' + mes + '-' + dia;

    return fechaTransformada;
}

function transformarFechaSubida(dateTimeString) {
    // Dividir la cadena de entrada en partes de fecha y hora
    const [dateString, timeString] = dateTimeString.split('T');

    // Dividir la parte de la fecha en año, mes y día
    const [year, month, day] = dateString.split('-');

    // Dividir la parte de la hora en horas, minutos y segundos
    const [hours, minutes, seconds] = timeString.split(':');

    // Crear un objeto de fecha y hora
    const dateTime = new Date(year, month - 1, day, hours, minutes, seconds);

    // Formatear la fecha y hora
    const formattedDate = `${day}/${month}/${year}`;
    const formattedTime = `${hours}:${minutes}:${seconds}`;
    const formattedDateTime = `${formattedDate} a las ${formattedTime}`;

    return formattedDateTime;
}

function transformarFecha_ddMMaaaa(fecha) {
    if (fecha == "null") return fecha;
    // Verificar si la fecha está en formato "aaaa-MM-dd"
    if (/^\d{4}-\d{2}-\d{2}$/.test(fecha)) {
        // Dividir la fecha en año, mes y día
        var partes = fecha.split('-');
        var año = partes[0];
        var mes = partes[1];
        var dia = partes[2];

        // Formar la fecha en el formato "dd/MM/aaaa"
        var fechaTransformada = dia + '/' + mes + '/' + año;

        return fechaTransformada;
    }

    // Si la fecha ya está en formato "dd/MM/aaaa", devolverla sin modificar
    return fecha;
}

function esCadenaVaciaOBlanca(cadena) {
    // Verifica si la cadena es nula, indefinida o si solo contiene espacios en blanco
    return cadena === null || cadena === undefined || cadena.trim() === '';
}
function esEnteroValidoNiCero(cadena) {
    // Verifica si la cadena es nula, indefinida o si solo contiene espacios en blanco
    return cadena === null || cadena === undefined || cadena === '' || cadena === 0;
}

function esEnteroValido(cadena) {
    // Verifica si la cadena es nula, indefinida o si solo contiene espacios en blanco
    return cadena === null || cadena === undefined || cadena === '';
}

function validaFechaPeriodo(fecha, mesPeriodo) {
    ////console.log(fecha, mesPeriodo)
    var mesFecha = new Date(fecha).getMonth() + 1;
    ////console.log(mesFecha == mesPeriodo)
    return mesFecha == mesPeriodo
}

function validaFechaPeriodoReune(fecha, trimestre) {
    // Convertir la cadena de fecha a un objeto Date
    var fechaObj = new Date(fecha);

    // Comprobar si la conversión a Date fue exitosa
    if (isNaN(fechaObj)) {
        //console.log('Fecha inválida');
        return false;
    }

    //console.log('fecha reune', fechaObj);
    //console.log('trimestre', trimestre);

    // Obtener el mes de la fecha proporcionada (los meses van de 0 a 11)
    var mes = fechaObj.getMonth();

    // Determinar el rango de meses para cada trimestre
    var rangosTrimestrales = [
        [0, 2],  // Trimestre 1: Enero - Marzo
        [3, 5],  // Trimestre 2: Abril - Junio
        [6, 8],  // Trimestre 3: Julio - Septiembre
        [9, 11]  // Trimestre 4: Octubre - Diciembre
    ];

    // Verificar si el mes está dentro del rango del trimestre
    return mes >= rangosTrimestrales[trimestre - 1][0] && mes <= rangosTrimestrales[trimestre - 1][1];
}


function valida2Fechas(fechaMenor, fechaMayor) {
    //console.log(fechaMenor, fechaMayor)
    var menor = new Date(fechaMenor);
    var mayor = new Date(fechaMayor);
    return menor.getTime() > mayor.getTime()
}

function validaStringNumerico(cadena) {
    if (cadena === null || cadena === undefined || cadena === '') return "0";

    return cadena
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

function capitalizeObjectKeys(obj) {
    const newObj = {};
    Object.keys(obj).forEach(key => {
        const capitalizedKey = capitalizeFirstLetter(key);
        // Ignorar la propiedad "QuejasProducto"
        if (key === "QuejasProducto") {
            newObj[key] = obj[key];
        } else {
            newObj[capitalizedKey] = obj[key];
        }
    });
    return newObj;
}

function convertirCP(cp) {
    var _cp = cp <= 9999 ? `0${cp}` : cp.toString();
    return _cp;
}

// Plugin de ordenación de fechas en formato "dd/MM/yyyy a las hh:mm:ss"
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "datetime-pre": function (a) {
        var dateTimeParts = a.split(" a las ");
        var dateParts = dateTimeParts[0].split('/');
        var timeParts = dateTimeParts[1].split(':');

        return new Date(dateParts[2], dateParts[1] - 1, dateParts[0], timeParts[0], timeParts[1], timeParts[2]).getTime();
    },
    "datetime-asc": function (a, b) {
        return a - b;
    },
    "datetime-desc": function (a, b) {
        return b - a;
    }
});

// Definir el tipo de columna personalizada
$.fn.dataTable.ext.type.order['datetime-pre'] = function (a) {
    var dateTimeParts = a.split(" a las ");
    var dateParts = dateTimeParts[0].split('/');
    var timeParts = dateTimeParts[1].split(':');

    return new Date(dateParts[2], dateParts[1] - 1, dateParts[0], timeParts[0], timeParts[1], timeParts[2]).getTime();
};