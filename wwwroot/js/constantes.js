
export const VISTAS = {
    consultas: 1,
    reclamaciones: 2,
    aclaraciones: 3
}


export const ESTATUS_TICKET = {
    pendiente: 1,
    rechazado: 2,
    aceptado: 3,
    eliminado: 4
}

export const NOMBRE_MESES = [
    'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
    'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
];

export const ID_MESES = {
    'Enero': 1,
    'Febrero': 2,
    'Marzo': 3,
    'Abril': 4,
    'Mayo': 5,
    'Junio': 6,
    'Julio': 7,
    'Agosto': 8,
    'Septiembre': 9,
    'Octubre': 10,
    'Noviembre': 11,
    'Diciembre': 12,
    'Enero': 12,
    'Diciembre': 12,
    'Diciembre': 12,
}

export const ID_PERIODO_REDECO = {
    1: textoScript.enero,
    2: textoScript.febrero,
    3: textoScript.marzo,
    4: textoScript.abril,
    5: textoScript.mayo,
    6: textoScript.junio,
    7: textoScript.julio,
    8: textoScript.agosto,
    9: textoScript.septiembre,
    10: textoScript.octubre,
    11: textoScript.noviembre,
    12: textoScript.diciembre,
    13: textoScript.eneroMarzo,
    14: textoScript.abrilJunio,
    15: textoScript.julioSeptiembre,
    16: textoScript.octubreDiciembre,
}

export const TIPO_DOCUMENTO = {
    1: textoScript.consulta,
    2: textoScript.aclaracion,
    3: textoScript.reclamacion
}

export const QUEJAS_FICTICIAS = [
    {
        "QuejasDenominacion": "",
        "QuejasSector": "",
        "QuejasNoMes": 3,
        "QuejasNum": 1,
        "QuejasFolio": "2AAAA-1234-2220",
        "QuejasFecRecepcion": "10/03/2024",
        "QuejasMedio": 1,
        "QuejasNivelAT": 1,
        "QuejasProducto": "026911831261",
        "QuejasCausa": "0871",
        "QuejasPORI": "NO",
        "QuejasEstatus": 2,
        "QuejasEstados": 1,
        "QuejasMunId": 1,
        "QuejasLocId": 22,
        "QuejasColId": 3,
        "QuejasCP": "20008",
        "QuejasTipoPersona": 1,
        "QuejasSexo": "H",
        "QuejasEdad": 44,
        "QuejasFecResolucion": "11/03/2024",
        "QuejasFecNotificacion": "12/03/2024",
        "QuejasRespuesta": 1,
        "QuejasNumPenal": null,
        "QuejasPenalizacion": 2
    }

]

export const CONSULTAS_FICTICIAS = [
    {
        "InstitucionClave": "Mex-Factor, S.A. de C.V. SOFOM E.N.R.",
        "Sector": "Sociedades Financieras de Objeto Múltiple E.N.R.",
        "ConsultasTrim": 1,
        "NumConsultas": 1,
        "ConsultasFolio": "REUNE_FOLIO561",
        "ConsultasEstatusCon": 2,
        "ConsultasFecAten": "17/03/2024",
        "EstadosId": 1,
        "ConsultasFecRecepcion": "16/03/2024",
        "MediosId": 1,
        "Producto": "026911791256",
        "CausaId": "0162",
        "ConsultasCP": 20008,
        "ConsultasMpioId": 1,
        "ConsultasLocId": 22,
        "ConsultasColId": 3,
        "ConsultascatnivelatenId": 1,
        "ConsultasPori": "SI"
    },
    {
        "InstitucionClave": "Mex-Factor, S.A. de C.V. SOFOM E.N.R.",
        "Sector": "Sociedades Financieras de Objeto Múltiple E.N.R.",
        "ConsultasTrim": 1,
        "NumConsultas": 1,
        "ConsultasFolio": "REUNE_FOLIO681",
        "ConsultasEstatusCon": 2,
        "ConsultasFecAten": "17/03/2024",
        "EstadosId": 1,
        "ConsultasFecRecepcion": "16/03/2024",
        "MediosId": 1,
        "Producto": "026911791256",
        "CausaId": "0162",
        "ConsultasCP": 20008,
        "ConsultasMpioId": 1,
        "ConsultasLocId": 22,
        "ConsultasColId": 3,
        "ConsultascatnivelatenId": 1,
        "ConsultasPori": "SI"
    }
]

export const RECLAMACIONES_FICTICIAS = [
    {
        "RecDenominacion": "Mex-Factor, S.A. de C.V. SOFOM E.N.R.",
        "RecSector": "Sociedades Financieras de Objeto Múltiple E.N.R.",
        "RecTrimestre": 1,
        "RecNumero": 1,
        "RecFolioAtencion": "REUNE_FOLIORec6",
        "RecEstadoConPend": 2,
        "RecFechaReclamacion": "16/03/2024",
        "RecFechaAtencion": "18/03/2024",
        "RecMedioRecepcionCanal": 1,
        "RecProductoServicio": "026911791256",
        "RecCausaMotivo": "0162",
        "RecFechaResolucion": "20/03/2024",
        "RecFechaNotifiUsuario": "21/03/2024",
        "RecEntidadFederativa": 9,
        "RecCodigoPostal": 9070,
        "RecMunicipioAlcaldia": 7,
        "RecLocalidad": 9,
        "RecColonia": null,
        "RecMonetario": "SI",
        "RecMontoReclamado": 1,
        "RecImporteAbonado": 1,
        "RecFechaAbonoImporte": "22/03/2024",
        "RecPori": "NO",
        "RecTipoPersona": 1,
        "RecSexo": "H",
        "RecEdad": 18,
        "RecSentidoResolucion": 1,
        "RecNivelAtencion": 1,
        "RecFolioCondusef": null,
        "RecReversa": null
    }
]

export const ACLARACIONES_FICTICIAS = [
    {
        "AclaracionDenominacion": "Mex-Factor, S.A. de C.V. SOFOM E.N.R.",
        "AclaracionSector": "Sociedades Financieras de Objeto Múltiple E.N.R.",
        "AclaracionTrimestre": 1,
        "AclaracionNumero": 1,
        "AclaracionFolioAtencion": "REUNE_FOLIOAcl1",
        "AclaracionEstadoConPend": 2,
        "AclaracionFechaAclaracion": "01/03/2024",
        "AclaracionFechaAtencion": "30/03/2024",
        "AclaracionMedioRecepcionCanal": 5,
        "AclaracionProductoServicio": "026911791256",
        "AclaracionCausaMotivo": "0552",
        "AclaracionFechaResolucion": "30/03/2024",
        "AclaracionFechaNotifiUsuario": "30/03/2024",
        "AclaracionEntidadFederativa": 9,
        "AclaracionCodigoPostal": 9070,
        "AclaracionMunicipioAlcaldia": 7,
        "AclaracionLocalidad": 9,
        "AclaracionColonia": null,
        "AclaracionMonetario": "NO",
        "AclaracionMontoReclamado": null,
        "AclaracionPori": "SI",
        "AclaracionTipoPersona": 1,
        "AclaracionSexo": "H",
        "AclaracionEdad": 18,
        "AclaracionNivelAtencion": 1,
        "AclaracionFolioCondusef": null,
        "AclaracionReversa": null,
        "AclaracionOperacionExtranjero": "SI"
    }
]
