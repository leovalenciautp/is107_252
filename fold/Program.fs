//
// Map reduce
//
// Inventado por Google, procesamiento masivo paralelo
//
//
//
open System 
// [1..20]
// |> Seq.map (fun e -> 2*e)
// |> Seq.sum
// |> ignore

//
// En F# reduce se llama en realidad fold
//
// [1..20]
// |> Seq.map (fun e -> 2*e)
// |> Seq.fold (fun acumulador elemento -> acumulador + elemento) 0 // Esto es Seq.sum
// |> Console.WriteLine

//
// La operacion factorial (que en matemáticas se denota con !)
// 5! = 1x2x3x4x5
// 6! = 1x2x3x4x5x6
// 52!
//


let factorial (n:float) =
    [1..(int n)]
    |> Seq.fold ( fun acc e -> acc*(float e) ) 1.0

let rec obtenerDecimal (mensaje:string) =
    printf $"{mensaje}"
    let r = Console.ReadLine()
    match Decimal.TryParse r with 
    | true,x -> x
    | false,_ ->
        printfn "Entrada Invalida"
        obtenerDecimal mensaje

let rec obtenerFloat (mensaje:string) =
    printf $"{mensaje}"
    let r = Console.ReadLine()
    match Double.TryParse r with 
    | true,x -> x
    | false,_ ->
        printfn "Entrada Invalida"
        obtenerFloat mensaje

// obtenerFloat "Entra un número: "
// |> factorial
// |> Console.WriteLine


//
// 0 1 1 2 3 5 8 13 21 34 ....
// 
// Fibonnacci

// [1..3]
// |> Seq.fold ( fun (a,b) _ -> (b,a+b)) (0,1)
// |> snd
// |> Console.WriteLine

let generarSequenciaFibonnacci n =
    [1..(n-2)]
    |> Seq.fold (fun acc _ ->
        match acc with
        | a :: b :: _ -> (a+b) :: acc
        | _ -> acc
    ) [1;0]
    |> Seq.rev

generarSequenciaFibonnacci 10
|> Seq.iter Console.WriteLine