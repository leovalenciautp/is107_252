module App.Navigator

open System
open App.Types

// Estados de la aplicacion
//
// Menu Principal
// Juego
// Pausa
// GameOver
// Terminado

type NavigatorState=
| ShowMainMenu
| ShowJuego
| ShowPausa
| ShowGameOver
| Terminated

type State = {
    NavigatorState: NavigatorState
}

let initState() =
    {
        NavigatorState = ShowMainMenu
    }


let showMainMenu state =
    Console.Clear()
    match MainMenu.mostrarMenu 20 10 with
    | MenuCommand.NewGame -> 
        {state with NavigatorState = ShowJuego}
    | MenuCommand.LoadGame ->
        // Magia previa para leer los datos
        // del disco
        {state with NavigatorState = ShowJuego}
    | MenuCommand.Exit ->
        {state with NavigatorState = Terminated}

let showJuego state =
    Console.Clear()
    Juego.mostrarJuego()
    {state with NavigatorState=ShowPausa}

let showGameOver state =
    Console.Clear()
    match GameOver.mostrarMenu 20 10 with
    | GameOverCommand.NewGame -> 
        {state with NavigatorState = ShowJuego}
    | GameOverCommand.Exit ->
        {state with NavigatorState = Terminated}

let showPause state =
    Console.Clear()
    match PauseMenu.mostrarMenu 20 10 with
    | PauseCommand.Continue ->
        //
        // Magia aqui
        //
        {state with NavigatorState = ShowJuego}
    | PauseCommand.SaveGame ->
        //
        // Magia de grabar
        //
        {state with NavigatorState = Terminated}
    | PauseCommand.Exit ->
        {state with NavigatorState = Terminated}    

let updateState state =
    match state.NavigatorState with
    | ShowMainMenu -> showMainMenu state
    | ShowJuego -> showJuego state
    | ShowPausa -> showPause state
    | ShowGameOver -> showGameOver state
    | _ -> state

let rec mainLoop state =
    let newState = updateState state
    if newState.NavigatorState <> Terminated then
        mainLoop newState

let mostrar() =
    initState()
    |> mainLoop