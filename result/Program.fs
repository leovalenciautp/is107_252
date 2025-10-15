// For more information see https://aka.ms/fsharp-console-apps

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