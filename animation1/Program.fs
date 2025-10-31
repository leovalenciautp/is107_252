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
    RocketX: int
    RocketY: int
    Counter: int
    MissilX: int
    MissilY: int
    MissilOn: bool
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
        RocketX = 0
        RocketY= 0
        MissilX = 0
        MissilY = 0
        MissilOn = false
        ProgramState = Running
        Counter = 0
        Tick = 0 
    }

let displayMessage x y color (mensaje:string) =
    Console.SetCursorPosition(x,y)
    Console.ForegroundColor <- color
    Console.Write mensaje

let displayJustifiedMessage x y color (mensaje:string) =
    let nuevaX = x-(mensaje.Length-1)
    displayMessage nuevaX y color mensaje


let displayAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "👽"
    state

let displayRocket state =
    displayMessage state.RocketX state.RocketY ConsoleColor.Yellow "🚀"
    state

let displayMissil state =
    if state.MissilOn then 
        displayMessage state.MissilX state.MissilY ConsoleColor.Red "=>"
    
    state

//
// Modificar esta función para que el contador salga perfecto
// con el último digito en la ultima columna.
//
let displayCounter state =
    displayJustifiedMessage (state.Width-1) 0 ConsoleColor.Yellow $"{state.Counter}"
    state

let cleanAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "  "
    state 

let cleanRocket state =
    displayMessage state.RocketX state.RocketY ConsoleColor.Yellow "  "
    state

let cleanMissil state =
    if state.MissilOn then
        displayMessage state.MissilX state.MissilY ConsoleColor.Yellow "  "
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

let updateRocketKeyboard key state =
    match key with 
    | ConsoleKey.W ->
        {state with RocketY = max 0 (state.RocketY-1)}
    | ConsoleKey.S ->
        { state with RocketY = min (state.Height-1) (state.RocketY+1)}
    | ConsoleKey.A ->
        { state with RocketX = max 0 (state.RocketX-1)}
    | ConsoleKey.D ->
        { state with RocketX = min (state.Width-2) (state.RocketX+1)}
    | _ -> state

let updateScape key state =
    match key with 
    | ConsoleKey.Escape ->
        {state with ProgramState = Terminated}
    | _ -> state

let updateMissilKeyboard key state =
    match key with 
    | ConsoleKey.Spacebar ->
        if not state.MissilOn then 
            {state with MissilOn=true;MissilX=state.AlienX+2;MissilY=state.AlienY}
        else
            state
    | _ -> state
let updateKeyboard state =
    if Console.KeyAvailable then
        let tecla = Console.ReadKey(true)
        state 
        |> updateScape tecla.Key
        |> updateAlienKeyboard tecla.Key
        |> updateRocketKeyboard tecla.Key
        |> updateMissilKeyboard tecla.Key
    else
        state

let updateTick state =
    {state with Tick = state.Tick+1}

let updateClock state =
    if state.Tick % 25 = 0 then
        {state with Counter = state.Counter+1}
    else
        state

let updateMissilAnimation state =
    if state.MissilOn then 
        let nuevaX = state.MissilX+1
        if nuevaX >= state.Width then
            {state with MissilOn=false}
        else
            {state with MissilX= nuevaX}
    else
        state

let updateState state =
    state
    |> updateTick
    |> updateClock
    |> updateMissilAnimation
    |> updateKeyboard

let updateScreen state =
    state
    |> displayAlien
    |> displayRocket
    |> displayCounter
    |> displayMissil
    |> ignore

let clearObjects state =
    state
    |> cleanAlien
    |> cleanRocket
    |> cleanMissil
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