open System
let dictionario =
    [
        "EL","THE"
        "MESA","TABLE"
        "AMARILLO","YELLOW"
        "SOBRE","ON"
        "LA","THE"
        "ESTA","IS"
        "LAPIZ","PENCIL"
        "BLANCO","WHITE"
        "BLANCA","WHITE"
        "ROJO","RED"
        "ROJA","RED"
        "CORRIENDO","RUNNING"
        "COMIENDO","EATING"
        "HOMBRE","MAN"
        "MUJER","WOMAN"
        "HOMBRES","MEN"
        "MUJERES","WOMEN"
        "ESTAN","ARE"
        "ESTAMOS","ARE"
        "LUNES","MONDAY"
        "MARTES","TUESDAY"
        "MIERCOLES","WEDNESDAY"
        "DOMINGO","SUNDAY"
        "SABADO","SATURDAY"
        "VIERNES","FRIDAY"
        "JUEVES","THURSDAY"
        "CLASE","CLASS"
        "VOY","GO"
        "A","TO"
        "PARA","TO"
    ]
    |> Map.ofList

let traducirPalabra (español:string) =
    dictionario
    |> Map.tryFind español
    |> Option.defaultValue español

let traducirFrase (frase:string) =
    frase.ToUpper().Split(" ") //CSV: Comma Separated Values
    |> Seq.map traducirPalabra
    |> Seq.reduce (fun acumulador elemento -> acumulador+" "+elemento)


Console.Write "Entra una frase: "
Console.ReadLine()
|> traducirFrase
|> Console.WriteLine


