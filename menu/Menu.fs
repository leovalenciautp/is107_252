module App.Menu
open System
open App.Utils

type MenuState =
| Active
| Terminated

type State = {
    MenuState: MenuState
    X: int
    Y: int
    CursorX: int
    CursorIndex: int
    Commands: string list
}

let initState() =
    {
        MenuState = Active
        X = 10
        Y = 10
        CursorX = 8
        CursorIndex = 0
        Commands = [
            "New Game"
            "Load Game"
            "Exit"
        ]
    }

let updateCursorScreen oldState newState =
    displayMessage oldState.CursorX (oldState.CursorIndex+oldState.Y) ConsoleColor.Yellow "  "
    displayMessage newState.CursorX (newState.CursorIndex+newState.Y) ConsoleColor.Yellow "☠️"
let updateMenuScreen state =
    state.Commands 
    |> List.iteri (fun i c ->
        displayMessage state.X (state.Y+i) ConsoleColor.Yellow c
    )

let updateMenuKeyboard key state =
    match key with
    | ConsoleKey.Enter ->
        {state with MenuState = Terminated}
    | ConsoleKey.UpArrow ->
        {state with CursorIndex = max 0 (state.CursorIndex-1)}
    | ConsoleKey.DownArrow ->
        {state with CursorIndex = min (state.Commands.Length-1) (state.CursorIndex+1)}
    | _ -> state
let updateKeyboard state =
    if Console.KeyAvailable then
        let tecla = Console.ReadKey true
        updateMenuKeyboard tecla.Key state
    else
        state

let updateState state =
    state
    |> updateKeyboard

let updateScreen oldState newState =
    updateMenuScreen newState
    updateCursorScreen oldState newState

let rec mainLoop state =
    let newState = updateState state
    updateScreen state newState
    if newState.MenuState = Active then
        dormirUnRato()
        mainLoop newState
    else
        state
let mostrarMenu() =
    initState()
    |> mainLoop
    |> fun state ->
        state.CursorIndex

