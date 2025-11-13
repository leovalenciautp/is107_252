module App.Letras

let letraA =
    [|
        " ████ "
        "█    █"
        "█    █"
        "██████"
        "█    █"
        "█    █"
    |]

let letraL =
    [|
        "█"
        "█"
        "█"
        "█"
        "█"
        "██████"
    |]

let letraI =
    [|
        "  ███ "
        "   █"
        "   █"
        "   █"
        "   █"
        "  ███"
    |]

let letraE =
    [|
        "██████"
        "█"
        "█████"
        "█    "
        "█"
        "██████"

    |]

let letraN =
    [|
        "█    █"
        "██   █"
        "█ █  █"
        "█  █ █"
        "█   ██"
        "█    █"
    |]

let mapaDeLetras =
    [
        'A',letraA
        'L',letraL
        'I',letraI
        'N',letraN
        'E',letraE
    ]
    |> Map.ofList

let encontrarLetra letra =
    mapaDeLetras
    |> Map.tryFind letra
    |> Option.defaultValue letraA
