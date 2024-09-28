
function showLoading() {
    $('#loading-panel').show();
}

// Función para ocultar el panel
function hideLoading() {
    $('#loading-panel').hide();
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
                alert('error')
            }
        },
        error: function (err) {
            //console.log('Ocurrio un error:', err)
            alert('error')
        }
    });
})


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