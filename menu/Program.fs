//
// Programming in the large
//
// Modules and Namespaces
//

module Leo.Main
open App
open System

Console.Clear()
Console.CursorVisible <- false

let choice = Menu.mostrarMenu()

Console.Clear()
Console.CursorVisible <- true
printfn $"El usuario eligio {choice}"


