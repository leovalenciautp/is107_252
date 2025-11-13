//
// Programming in the large
//
// Modules and Namespaces
//

module Leo.Main
open App
open App.Types
open System

Console.Clear()
Console.CursorVisible <- false

let choice = GameOver.mostrarMenu 20 10

Console.Clear()
Console.CursorVisible <- true

let msg =
    match choice with
    | GameOverCommand.NewGame -> "Nuevo Juego"
    | GameOverCommand.Exit -> "Salir del juego"
printfn $"El usuario eligio {msg}"


