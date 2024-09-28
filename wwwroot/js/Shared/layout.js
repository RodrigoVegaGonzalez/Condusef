$(document).ready(function () {
    //$("#config-a").hover(function () {
    //    alert('h')
    //    $("#config-icon").addClass("fa-spin");
    //}, function () {
    //    $("#config-icon").removeClass("fa-spin");
    //});
});

window.addEventListener('load', function () {
    removeSelect2HiddenClass();
    //$('.selectpicker').select2();
    //$("span.select2.select2-container.select2-container--default").each(function () {
    //    // Agregar la nueva clase deseada
    //    $(this).addClass("select-form");
    //});
});

function removeSelect2HiddenClass() {
    // Obtener todos los selectores que tienen las clases especificadas
    var selects = document.querySelectorAll('.select-form.selectpicker.select2-hidden-accessible');
    var selectsTextHidden = document.querySelectorAll('.select2-selection__rendered');

    // Iterar sobre cada select
    selects.forEach(function (select) {
        // Eliminar la clase select2-hidden-accessible
        select.classList.remove('select2-hidden-accessible');
    });

    //selectsTextHidden.forEach(function (select) {
    //    select.show();
    //});
}


//Función para mostrar el panel
function showLoading() {
    $('#loading-panel').show();
}

// Función para ocultar el panel
function hideLoading() {
    $('#loading-panel').hide();
}

function cambiarIdioma(lang) {
    $.ajax({
        url: urlCambiarIdioma,
        method: "POST",
        cache: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        data: {
            idioma: lang,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        success: function (respuesta) {
            if (respuesta.success) {
                location.reload();
            }
            else {
                alerta("error",'Error al cambiar el idioma')
            }
        }
    });
}

$('#logout-btn').on('click', function () {
    //__RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
    $.ajax({
        url: urlLogout,
        method: "POST",
        cache: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (res) {
            if (res.success) {
                window.location.href = res.message
            } else {
                alerta("error", "Ocurrió un error al cerrar sesión", "Intentelo nuevamente")
                console.error(res.message)
            }
        },
        error: function (err) {
            //console.log('Ocurrio un error:', err)
            alerta("error", "Error", "Ocurrió un error en su peticion. Intentelo nuevamente")
        }
    });
})

function reiniciarSelect(id) {
    $(`#${id}`).val(null).trigger('change');
}

function cambiarValorSelect(id, value) {
    $(`#${id}`).val(value).trigger('change');
}

function wait(func, time) {
    setTimeout(func(), time);
}

function alerta(icono,titulo, texto = "", tiempo = 3500) {
    Swal.fire({
        icon: icono,
        title: titulo,
        text: texto,
        timer:  tiempo
    });
}

function recargarPagina(time) {
    setTimeout(location.reload(),time)
}

function alertaConRecarga(icono, titulo, texto, time = 2500) {
    Swal.fire({
        icon: icono,
        title: titulo,
        text: texto,
        showConfirmButton: false,
        timer: time,
        didOpen: () => {
            setTimeout(function () {
                location.reload();
            }, 3000);
        }
    });
}

function capitalizeKeys(obj) {
    // Crear un nuevo objeto para almacenar las claves transformadas
    let newObj = {};

    // Iterar sobre las claves del objeto original
    for (let key in obj) {
        if (obj.hasOwnProperty(key)) {
            // Capitalizar la primera letra de la clave
            let capitalizedKey = key.charAt(0).toUpperCase() + key.slice(1);

            // Asignar el valor al nuevo objeto con la clave transformada
            newObj[capitalizedKey] = obj[key];
        }
    }

    return newObj;
}

function generarFolio() {
    // Crear un array de caracteres válidos (letras mayúsculas y números)
    const validChars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';

    // Función auxiliar para generar un bloque de 4 caracteres
    function generateBlock() {
        let block = '';
        for (let i = 0; i < 4; i++) {
            block += validChars.charAt(Math.floor(Math.random() * validChars.length));
        }
        return block;
    }

    // Generar la cadena completa con el formato deseado
    let code = '';
    for (let i = 0; i < 4; i++) {
        code += generateBlock();
        if (i < 3) {
            code += '-';
        }
    }

    return code;
}

function stringToDecimal(str) {
    // Intentamos convertir el string a un número decimal
    var decimalNumber = parseFloat(str);

    // Verificamos si la conversión fue exitosa
    if (isNaN(decimalNumber)) {
        return 0;
    }

    return decimalNumber;
}