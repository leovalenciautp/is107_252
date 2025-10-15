open System
open System.IO
open System.Text.Json

//
// Tarjeta de Identidad
// Cedula
// Cedula de Extranjeria
// NIT

type Documento =
| TI of decimal
| Cedula of decimal
| CedulaExtranjeria of decimal
| NIT of decimal

let rec obtenerDecimal (mensaje:string) =
    printf $"{mensaje}"
    let r = Console.ReadLine()
    match Decimal.TryParse r with 
    | true,x -> x
    | false,_ ->
        printfn "Entrada Invalida"
        obtenerDecimal mensaje

let rec obtenerEntero (mensaje:string) =
    printf $"{mensaje}"
    let r = Console.ReadLine()
    match Int32.TryParse r with 
    | true,x -> x
    | false,_ ->
        printfn "Entrada Invalida"
        obtenerEntero mensaje

let rec obtenerTipoDeDocumento() =
    printf "Entra el tipo de documento (ti,cedula,ce,nit): "
    match Console.ReadLine() with
    | "ti" -> TI
    | "cedula" -> Cedula
    | "ce" -> CedulaExtranjeria
    | "nit" -> NIT
    | _ ->
        printfn "Entrada Invalida"
        obtenerTipoDeDocumento()

let obtenerIdentification() =
    let tipo = obtenerTipoDeDocumento()
    let numero = obtenerDecimal "Entra el número del documento: "
    tipo numero

let imprimirInformacion documento =
    match documento with
    | TI x -> printfn $"Tu tarjeta de identidad es {x}"
    | Cedula n -> printfn $"Tu Cédula es {n}"
    | CedulaExtranjeria x -> printfn $"Tu Cedula de Extranjeria es {x}"
    | NIT x -> printfn $"Tu NIT es {x}"


type Palo =
| Diamantes
| Corazones
| Picas
| Treboles

type Carta =
| K of Palo
| Q of Palo
| J of Palo
| As of Palo
| Numero of int*Palo

let rec obtenerPalo() =
    printf "Entra el Palo de tu carta (corazones, diamantes, picas, treboles): "
    match Console.ReadLine() with
    | "corazones" -> Corazones
    | "diamantes" -> Diamantes
    | "picas" -> Picas
    | "treboles" -> Treboles
    | _ ->
        printfn "Entrada Invalida"
        obtenerPalo()

let obtenerCarta() =
    let palo = obtenerPalo()
    let rec pedirValor() =
        printf "Entra el valor de la carta: "
        let valor = Console.ReadLine()
        match valor with
        | "K" -> K palo
        | "Q" -> Q palo
        | "J" -> J palo
        | "As" -> As palo
        | "2" | "3" |"4" |"5" |"6" |"7"
        | "8" | "9" | "10" ->
            Numero (valor |> int,palo)
        | _ ->
            printfn "Entrada Invalida"
            pedirValor()
    pedirValor()

let imprimirCarta carta =
    match carta with
    | K x -> $"Es un K de {x}"
    | Q n -> $"Es una Q de {n}"
    | J x -> $"Es una J de {x}"
    | As x -> $"Es un As de {x}"
    | Numero (x,palo) ->
        $"Es el {x} de {palo}"
    |> Console.WriteLine


//
// Comercio exterior
//
type Valor =
| Dolares of decimal
| Euros of decimal
| Pesos of decimal
| Yenes of decimal

let v = Dolares 10_000m
let p = Pesos 1_500_000_000m

let e = Euros 250_0000m

//
// Tabla de conversion
// 1 USD 3,885.2400
// 1 Euro = 1.1744 Dolares
// 1 USD = 150 yen

let convertirADolares valor =
    match valor with
    | Dolares x -> x
    | Euros x -> x*1.1744m
    | Pesos x -> x/3_885.24m
    | Yenes x -> x/150.0m


//
// Listas
// Es una coleccion de elementos del mismo tipo.
//

let miTupla = 3,7.2,1,"Hola",4
let _,_,_,x,_ = miTupla
let miLista = [3;7;1;2;4]
 

//
// List.iter se usa mas que todo para mostrar cosas en pantalla
// porque el lambda no puede retornar nada.
//

let bancos = [
    Pesos 2_000_000m
    Dolares 1_000m
    Euros 3_000m
    Yenes 200_000m
]

//
// Google inventó Map/Reduce (en el 2017 regala: Attention is the only thing you need)
//
// List.map
//

//
// Eager Evaluation
//
// bancos
// |> List.map convertirADolares
// |> List.map ( fun e ->
//     printfn $"Conversion es: {e}"
//     e
// )
// |> List.sum
// |> fun total ->
//     printfn $"El total en dolares es %0.4f{total}"

// 
// Tarea, investigar optimizacion para no recorrer
// la lista multiples veces (sola una vez)
//
// Hint: Lazy Evaluation
//

//
// Atlas del Mundo
//
// Colombia, USA, Argentina, España, Alemania, Azerbaiyan, Japon
// La base de datos, contiene, Pais, Capital y número de habitantes
//

type Atlas = {
    Pais: string
    Capital: string
    Habitantes: int
}

let atlas = [
    {
        Pais = "Colombia"
        Habitantes =52_890_000
        Capital = "Bogotá DC"
    }

    {
        Pais = "USA"
        Capital = "Washington DC"
        Habitantes = 340_100_000
    }

    {
        Pais = "Argentina"
        Capital = "Buenos Aires"
        Habitantes =45_700_000
    }

    {
        Pais = "España"
        Capital = "Madrid"
        Habitantes = 48_810_000
    }
    {
        Pais = "Alemania"
        Capital = "Berlin"
        Habitantes= 83_510_000
    }
    {
        Pais="Azerbaiyan"
        Capital="Baku"
        Habitantes = 10_200_000
    }
    {
        Pais = "Japon"
        Capital ="Tokyo"
        Habitantes =124_000_000
    }
]

let imprimirAtlas lista =
    lista
    |> Seq.iter (fun r ->
        printfn $"%-10s{r.Pais} %-15s{r.Capital} %10d{r.Habitantes}"
    )

let totalizarHabitantes (lista:Atlas list)=
    lista
    |> Seq.map ( fun r -> r.Habitantes)
    |> Seq.sum

// atlas
// |> Seq.sortBy (fun e -> e.Habitantes)
// |> Seq.rev
// |> imprimirAtlas

// atlas
// |> totalizarHabitantes
// |> fun total -> printfn $"El total es: {total}"

//
// List.find
//
//

//
// DRY Don't repeat yourself
//


let encontrarNumeroDeHabitantes lista (pais:string) =
    lista
    |> Seq.tryFind (fun (p:string,_,_) -> p.ToLower() = pais.ToLower())
    |> Option.map (fun (_,_,h) -> h)
    


// printf "Pais a buscar: "
// Console.ReadLine()
// |> encontrarNumeroDeHabitantes atlas
// |> fun o ->
//     match o with 
//     | Some h -> printfn $"El numero de habitantes es {h}"
//     | None -> printfn "No se encontró el pais"
// 
// Es posible pasar de Listas a Secuencias
// No se puede pasar de Sec a lista, ( o de Lazy a Eager)

//
// Option, es una forma de reportar errores.

// Tony Hoare, invento NULL
//

let cuentas = [] // Lista vacia

//
// :: CONS operator, agrega un elemento al principio de una lista
//

//
// @ Concat operator (en ingles se dice "at")
// Une dos listas
// 


//
// Vamos un hacer un programa, que arme una lista programatica de paises y capitales
// preguntamos pais y capital
//
// Deseas agregar mas? (s) repite y agrega. si responde (n),
// muestra la base de datos que se tiene. Ordenar por pais.
// 

// Modifiquemos para pedir PIB por pais.
// Listar ordenado por PIB de mayor a menor
// En columnas bonitas.

let rec crearBaseDeDatos lista =
    printf "Entra un pais: "
    let pais = Console.ReadLine()
    printf "Entra la capital: "
    let capital = Console.ReadLine()
    let pib = obtenerDecimal "Entra el PIB: "

    let nuevaLista = (pais,capital,pib) :: lista 
    printf "Deaseas agregar un pais: "
    if Console.ReadLine() = "n" then 
        nuevaLista
    else
        crearBaseDeDatos nuevaLista

// crearBaseDeDatos []
// |> Seq.sortBy (fun (_,_,pib) -> pib)
// |> Seq.rev
// |> Seq.iter (fun (pais,capital,pib) -> printfn $"%-15s{pais} %-15s{capital} %15.0f{pib}")    

//%-15s{pais} %-15s{capital} %15.0f{pib}

//
// Tarea para el martes
// Usar el programa de las cartas de Poker
//
//

let rec crearListaDeCartas lista =
    let carta = obtenerCarta()
    let nuevaLista = carta :: lista
    printf "Quieres agregar otra carta? (s/n)"
    if Console.ReadLine() = "n" then 
        nuevaLista
    else
        crearListaDeCartas nuevaLista

let valorDeCarta carta =
    match carta with
    | Numero (x,_) -> x
    | J _ -> 11
    | Q _ -> 12
    | K _ -> 13
    | As _ -> 14

// crearListaDeCartas []
// |> Seq.sortBy valorDeCarta
// |> Seq.iter imprimirCarta


//
// let (rec)
// if... then.. else
// match ... with
// fun (lambdas)
// |>
// type (Discriminated unions)
// (,,,,) Tuplas Son casi exclusivas de F# y lenguajes funcionales
// [;;;;] listas
//

//
// Records
//

// Declaracion de un Record

type Pais = {
    Nombre: string // Esto es un field
    Capital: string // Esto es otro field
    Pib: decimal // también es un field
}

let unPais = {
    Nombre = "Colombia"
    Capital = "Bogota"
    Pib = 100m
}

type Amigo = {
    Nombre: string
    Apellido: string
    Apodo: string option
    Email: string
    Telefono: decimal
}

let unAmigo = {
    Nombre = "Leo"
    Apellido = "Valencia"
    Apodo = None
    Email = "leo@gmail"
    Telefono = 1254321m
}



type State = {
    BaseDeDatos: Amigo list
}

let serializarLista lista =
    JsonSerializer.Serialize lista

let deserializarLista (json:string) =
    JsonSerializer.Deserialize<Amigo list> json

let obtenerApodo() =
    printf "Entra apodo (Enter si no tiene): "
    let r = Console.ReadLine()
    if r = "" then 
        None
    else
        Some r
let obtenerDatosDeAmigo() =
    printf "Entra el Nombre: "
    let nombre = Console.ReadLine()
    printf "Entra Apellido: "
    let apellido = Console.ReadLine()

     

    printf "Email: "
    let email = Console.ReadLine()
    let apodo = obtenerApodo()
    let telefono = obtenerDecimal "Entra Telefono: "
    {
        Nombre = nombre
        Apellido = apellido
        Apodo = apodo
        Email = email
        Telefono = telefono
    }

let imprimirAmigo amigo =
    let apodo =
        match amigo.Apodo with 
        | None -> "No Tiene"
        | Some x -> x

    printfn $"%-15s{amigo.Nombre} %-15s{amigo.Apellido} %-15s{apodo} %-20s{amigo.Email} %11.0f{amigo.Telefono}"

let imprimirEncabezado() =
    let nombre = "Nombre"
    let apellido = "Apellido"
    let apodo = "Apodo"
    let email = "Email"
    let telefono = "Teléfono"
    printfn $"%-15s{nombre} %-15s{apellido} %-15s{apodo} %-20s{email} %11s{telefono}"


let rec crearBaseDeDatosDeAmigos lista =
    let amigo = obtenerDatosDeAmigo()
    let nuevaLista = amigo :: lista
    printf "Deseas agregar otro amigo? (s/n): "
    if Console.ReadLine() = "n" then
        nuevaLista
    else
        crearBaseDeDatosDeAmigos nuevaLista

let leerBaseDeDatosDeAmigos (databaseName:string) =
    File.ReadAllText databaseName
    |> deserializarLista

let nombreArchivo = "amigos.json"
let obtenerBaseDeDatosDeAmigos() =
    if File.Exists nombreArchivo then
        leerBaseDeDatosDeAmigos nombreArchivo
    else
        let lista = crearBaseDeDatosDeAmigos []
        File.WriteAllText(nombreArchivo,lista |> serializarLista)
        lista

let listarAmigos lista =
    imprimirEncabezado()
    lista
    |> Seq.sortBy (fun r -> r.Apellido)
    |> Seq.iter imprimirAmigo

// DRY
// Don't Repeat Yourself
// No se repitan a si mismos
//

// Refactoring -> refactorizacion

let buscarUnAmigo lista =
    printf "Entra Nombre: "
    let nombre = Console.ReadLine()
    printf "Entra Apellido: "
    let apellido = Console.ReadLine()
    lista |> List.tryFind (fun r -> r.Nombre = nombre && r.Apellido = apellido) 

let consultarAmigo lista =
    match buscarUnAmigo lista with
    | None -> printfn "No se encontró el amigo"
    | Some x -> 
        imprimirEncabezado()
        imprimirAmigo x

let borrarAmigo lista =
    match buscarUnAmigo lista with
    | None ->
        printfn "No se encontró el amigo"
        lista
    | Some amigo ->
        let nuevaLista =
            lista 
            |> List.filter (fun r -> not (r.Nombre = amigo.Nombre && r.Apellido = amigo.Apellido))
        File.WriteAllText(nombreArchivo,nuevaLista |> serializarLista)
        nuevaLista

let añadirAmigo lista =
    let nuevaLista = obtenerDatosDeAmigo() :: lista
    //
    // Es importante escribir la nueva lista
    //
    File.WriteAllText(nombreArchivo,nuevaLista |> serializarLista)
    nuevaLista

type ModificarResultado =
| Guardar
| Cancelar

let rec imprimirMenuModificarAmigo amigo =
    let apodo = amigo.Apodo |> Option.defaultValue "No tiene"
    printfn $"1. Nombre: {amigo.Nombre}"
    printfn $"2. Apellido: {amigo.Apellido}"
    printfn $"3. Apodo: {apodo}"
    printfn $"4. Email: {amigo.Email}"
    printfn $"5. Telefono: %0.0f{amigo.Telefono}"
    printfn "6. Guardar y salir"
    printfn "7. Cancelar y salir"
    printfn ""
    printf "Elije el campo a cambiar: "
    let o = Console.ReadLine()
    let nuevoAmigo =
        match o with 
        | "1" -> 
            printf "Entra nuevo nombre: "
            let nombre = Console.ReadLine()
            if nombre <> "" then
                {amigo with Nombre = nombre}
            else
                amigo

        | "2" ->
            printfn "Entra nuevo apellido: "
            let apellido = Console.ReadLine()
            if apellido <> "" then
                {amigo with Apellido = apellido}
            else
                amigo

        | "3" ->
            let apodo = obtenerApodo()
            {amigo with Apodo = apodo}

        | "4" ->
            printf "Entra el email: "
            let email = Console.ReadLine()
            if email <> "" then 
                {amigo with Email=email}
            else
                amigo
        | "5" ->
            let telefono = obtenerDecimal "Entra nuevo telefono: "
            {amigo with Telefono = telefono}

        | _ -> amigo

    if o <> "6" && o <> "7" then
        imprimirMenuModificarAmigo nuevoAmigo
    else
        if o = "6" then
            Guardar,nuevoAmigo
        else
            Cancelar,nuevoAmigo
     


let modificarAmigo lista =
    match buscarUnAmigo lista with
    | None ->
        printfn "No se encontró el amigo"
        lista
    | Some amigo ->
        let r = imprimirMenuModificarAmigo amigo
        match r with
        | Guardar,nuevoAmigo ->
            //Primero borramos al amigo
            let nuevaLista =
                lista 
                |> List.filter (fun r -> not (r.Nombre = amigo.Nombre && r.Apellido = amigo.Apellido))
            let modLista = nuevoAmigo :: nuevaLista
            File.WriteAllText(nombreArchivo,modLista |> serializarLista)
            modLista
        | Cancelar,_ ->
            lista


let rec imprimirMenuPrincipal state =
    printfn "1. Añadir amigo"
    printfn "2. Consultar amigo"
    printfn "3. Listar amigos"
    printfn "4. Borrar amigo"
    printfn "5. Editar amigo"
    printfn "6. Salir"
    printfn ""
    let o = obtenerEntero "Elige una opción: "
    printfn ""
    let newState =
        match o with
        | 2 -> 
            consultarAmigo state.BaseDeDatos
            state
        | 3 -> 
            listarAmigos state.BaseDeDatos
            state
        | 4 -> 
            let newBase = borrarAmigo state.BaseDeDatos
            {state with BaseDeDatos = newBase}
        | 5 ->
            let newBase = modificarAmigo state.BaseDeDatos
            {state with BaseDeDatos= newBase}
        | 1  -> 
            let newBase = añadirAmigo state.BaseDeDatos
            { state with BaseDeDatos = newBase} // Copy and Update
        | _ ->
            if o <> 6 then 
                printfn "Opcion inválida"
            state
    printfn ""
    if o <> 6 then 
        imprimirMenuPrincipal newState

obtenerBaseDeDatosDeAmigos()
|> fun baseDeDatos -> 
    { BaseDeDatos = baseDeDatos }
|> imprimirMenuPrincipal


//
// CRUD
// C -> Create
// R -> Read
// U -> Update
// D -> Delete
//

//
// GitHub -> es un cloud repository de git
// Linus Torvalds -> Linux es una copia de Unix.
// Source Control -> git
//

//
// Result
//


