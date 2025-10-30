open System
open System.Threading

//
// Todo programa es un State machine
//
// MVU
// Model -> View -> Update
//
// Elmish paradigm (viene del lenguage Elm)
//

type ProgramState = 
| Running
| Terminated

//
// Esto es un ejemplo de Scaffolding
//
type State = {
    AlienX: int
    AlienY: int
    Counter: int
    Tick: int
    Width: int
    Height: int
    ProgramState: ProgramState
}

let initState() =
    let width = Console.BufferWidth
    let height = Console.BufferHeight
    {
        Width = width
        Height = height
        AlienX = width/2
        AlienY = height/2
        ProgramState = Running
        Counter = 0
        Tick = 0 
    }

let displayMessage x y color (mensaje:string) =
    Console.SetCursorPosition(x,y)
    Console.ForegroundColor <- color
    Console.Write mensaje


let displayAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "👽"
    state

let displayCounter state =
    displayMessage (state.Width-10) 0 ConsoleColor.Red $"{state.Counter}"
    state

let cleanAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "  "
    state 
let dormirUnRato() =
    Thread.Sleep 40

let updateAlienKeyboard key state =
    match key with 
    | ConsoleKey.UpArrow ->
        {state with AlienY = max 0 (state.AlienY-1)}
    | ConsoleKey.DownArrow ->
        { state with AlienY = min (state.Height-1) (state.AlienY+1)}
    | ConsoleKey.LeftArrow ->
        { state with AlienX = max 0 (state.AlienX-1)}
    | ConsoleKey.RightArrow ->
        { state with AlienX = min (state.Width-2) (state.AlienX+1)}
    | _ -> state

let updateScape key state =
    match key with 
    | ConsoleKey.Escape ->
        {state with ProgramState = Terminated}
    | _ -> state
let updateKeyboard state =
    if Console.KeyAvailable then
        let tecla = Console.ReadKey(true)
        state 
        |> updateScape tecla.Key
        |> updateAlienKeyboard tecla.Key
    else
        state

let updateTick state =
    {state with Tick = state.Tick+1}

let updateClock state =
    if state.Tick % 25 = 0 then
        {state with Counter = state.Counter+1}
    else
        state

let updateState state =
    state
    |> updateTick
    |> updateClock
    |> updateKeyboard

let updateScreen state =
    state
    |> displayAlien
    |> displayCounter
    |> ignore

let clearObjects state =
    state
    |> cleanAlien
    |> ignore
let rec mainLoop state =
    let newState = updateState state
    clearObjects state
    updateScreen newState
    dormirUnRato()
    if newState.ProgramState = Running then
        mainLoop newState

Console.CursorVisible <- false
Console.Clear()


initState()
|> mainLoop

Console.Clear()
Console.CursorVisible <- true