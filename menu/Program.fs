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

let choice = Menu.mostrarMenu()

Console.Clear()
Console.CursorVisible <- true

let msg =
    match choice with
    | NewGame -> "Nuevo Juego"
    | LoadGame -> "Cargar un juego"
    | Exit -> "Salir"
printfn $"El usuario eligio {msg}"


