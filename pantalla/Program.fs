open System

let centerX = Console.BufferWidth/2
let centerY = Console.BufferHeight/2

let originalForeground = Console.ForegroundColor
let originalBackground = Console.BackgroundColor

Console.BackgroundColor <- ConsoleColor.Blue
Console.Clear()
Console.SetCursorPosition(centerX-4,centerY)
Console.ForegroundColor <- ConsoleColor.Red
Console.BackgroundColor <- ConsoleColor.Yellow
Console.Write "Hola "
Console.ForegroundColor <- ConsoleColor.Green
Console.BackgroundColor <- ConsoleColor.Black
Console.WriteLine "Mundo"
Console.ReadLine() |> ignore

//
// Cuando el programa termine, es bueno restaurar
//
Console.ForegroundColor <- originalForeground
Console.BackgroundColor <- originalBackground
Console.Clear()

