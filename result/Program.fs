
open System

let leerBaseDeDatosNacionesUnidas() =
    let everythingIsOk = true    
    if everythingIsOk then 
        Ok 42
    else    
        Error "Problema con internet"
let r = leerBaseDeDatosNacionesUnidas()

match r with 
| Ok resultado -> printfn $"Perfector el resultado es {resultado}"
| Error mensaje -> printfn $"{mensaje}"

//
// Vamos a crear un simulador de errores
//