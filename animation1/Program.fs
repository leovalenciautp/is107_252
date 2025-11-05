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


type Missil = {
    X: int
    Y: int
}

//
// Esto es un ejemplo de Scaffolding
//
type State = {
    AlienX: int
    AlienY: int
    RocketX: int
    RocketY: int
    Counter: int
    Misiles: Missil list
    Tick: int
    Width: int
    Height: int
    ProgramState: ProgramState
    EnemyX: int
    EnemyY: int
    EnemySpeed: float
    EnemyVisible: bool
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
        Misiles=[]
        ProgramState = Running
        Counter = 0
        Tick = 0
        EnemyX = width-10
        EnemyY = 0 
        EnemySpeed = Math.PI / 50.0
        EnemyVisible= true
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

let displayEnemy state =
    if state.EnemyVisible then 
        displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "👾"
    state

let displayMissil state =
    state.Misiles
    |> List.iter ( fun m ->
        displayMessage m.X m.Y ConsoleColor.Red "=>"
    )    
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

let cleanEnemy state =
    if state.EnemyVisible then
        displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "  "
    state

let cleanMissil state =
    state.Misiles
    |> List.iter ( fun m -> 
        displayMessage m.X m.Y ConsoleColor.Yellow "  "
    )
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
        let nuevoMisil = { X = state.AlienX+2; Y= state.AlienY}
        {state with Misiles = nuevoMisil :: state.Misiles}
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
    let misiles =
        state.Misiles
        |> Seq.map ( fun m -> 
            { m with X = m.X+1}
        )
        |> Seq.filter ( fun m -> m.X < state.Width-1)
        |> Seq.toList
    {state with Misiles=misiles}

let updateEnemy state =
    let newY = - float state.Height /2.0 * (Math.Cos (state.EnemySpeed*float state.Tick)- 1.0 )
    {state with EnemyY = int newY}

let updateCollision state =
    if state.EnemyVisible then
        let nuevosMisiles =
            state.Misiles
            |> List.filter ( fun m -> not (m.Y = state.EnemyY && (m.X+1) = state.EnemyX))
        if nuevosMisiles.Length <> state.Misiles.Length then
            {state with Misiles = nuevosMisiles; EnemyVisible=false}
        else
            state  
    else
        state


let updateState state =
    state
    |> updateTick
    |> updateClock
    |> updateEnemy
    |> updateMissilAnimation
    |> updateCollision
    |> updateKeyboard

let updateScreen state =
    state
    |> displayAlien
    |> displayRocket
    |> displayCounter
    |> displayMissil
    |> displayEnemy
    |> ignore

let clearObjects state =
    state
    |> cleanAlien
    |> cleanRocket
    |> cleanMissil
    |> cleanEnemy
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