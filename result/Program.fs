
open System

//
// Vamos a crear un simulador de errores
//

let generador = new Random()

let leerBaseDeDatosNacionesUnidas() =
    if generador.NextDouble() <= 0.9 then 
        Ok 42
    else    
        Error "Problema con internet"

let usarSuperComputador x =
    if generador.NextDouble() <= 0.9 then
        Ok (x*17+31)
    else
        Error "Supercomputador caido"

let guardarBaseDeDatos x =
    if generador.NextDouble() <= 0.9 then
        Ok $"Grabado Perfecto: {x}"
    else
        Error "Base de datos caida"    

let convertirAEntero (valor:string) =
    let r = Int32.TryParse valor
    match r with 
    | true,x -> Ok x
    | false,_ -> Error "No pude convertir el numero"

//
// Esto es un estilo de progrmacion, solo usado en programación funcional
// Rail oriented programming.
//
// En otros lenguajes se usan, Excepciones.
//
leerBaseDeDatosNacionesUnidas()
|> Result.bind usarSuperComputador
|> Result.bind guardarBaseDeDatos
|> fun r ->
    match r with 
    | Ok valor -> printfn $"Exito!: {valor}"
    | Error mensaje -> printfn $"Error! {mensaje}"

// printf "Entra un número: "
// Console.ReadLine()
// |>convertirAEntero
// |> Result.defaultValue 0
// |> Console.WriteLine


let actualizarDato nombreDeCampo valorAnterior=
    printf $"{nombreDeCampo}: {valorAnterior} -> "
    let valor = Console.ReadLine()
    if valor = "" then
        Ok valorAnterior
    else
        Ok valor


let convertirStringADecimal (valor:string) =
    let r = Decimal.TryParse valor
    match r with 
    | true,x -> Ok x
    | false,_ -> Error "No pude convertir el numero"

let actualizarDatoDecimal nombreDeCampo valorAnterior =
    printf $"{nombreDeCampo}: {valorAnterior} -> "
    let valor = Console.ReadLine()
    if valor = "" then 
        Ok valorAnterior
    else
        valor |> convertirStringADecimal
    

let rec modificarTelefono() =
    actualizarDatoDecimal "Telefono" 1247365m
    |> fun r ->
        match r with
        | Ok valor -> valor
        | Error mensaje ->
            printfn $"{mensaje}"
            modificarTelefono()

modificarTelefono()
|> Console.WriteLine
