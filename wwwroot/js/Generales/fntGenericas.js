function validaNullBool(value) {
    return value.toLowerCase() === "true";
}

function ConstruirTablaElementos(elementoTabla, arregloDatos) {
    elementoTabla.rows().remove();
    if (!/undefined|NaN|null/.test(arregloDatos)) {
        elementoTabla.rows.add(arregloDatos).draw(true);
    }
    
    else {
        elementoTabla.draw(true);
    }
        
}