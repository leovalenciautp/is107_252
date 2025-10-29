open System
open System.Threading

let centerX = Console.BufferWidth/2
let centerY = Console.BufferHeight/2

let originalForeground = Console.ForegroundColor
let originalBackground = Console.BackgroundColor

Console.BackgroundColor <- ConsoleColor.Blue
Console.Clear()

let displayMessage x y color (mensaje:string) =
    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.WriteLine mensaje

//
// Es importante que el eventLoop duerma unos cuantos
// milisegundos para no ocupar el CPU todo el tiempo
//
let performSleep() =
    Thread.Sleep 17

displayMessage centerX centerY ConsoleColor.Yellow "🛸"

Console.ReadLine() |> ignore

[0..centerX-1]
|> Seq.iter ( fun x -> 
    displayMessage (centerX-x) centerY ConsoleColor.Yellow " "
    displayMessage (centerX-x-1) centerY ConsoleColor.Yellow "🛸"
    performSleep()
)

Console.ReadLine() |> ignore

//
// Cuando el programa termine, es bueno restaurar
//
Console.ForegroundColor <- originalForeground
Console.BackgroundColor <- originalBackground
Console.Clear()

//
// Como leer las flechas del teclado y en general como leer cualquier tecla
//

