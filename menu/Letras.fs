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

let letraC =
    [|
        " ████ "
        "█    █"
        "█"
        "█"
        "█    █"
        " ████"
    |]

let letraG =
    [|
        " ████ "
        "█    █"
        "█"
        "█  ███ "
        "█    █"
        " ████"
    |]

let letraO =
    [|
        " ████ "
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " ████"
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

let letraT =
    [|
        "█████ "
        "  █"
        "  █"
        "  █"
        "  █"
        "  █"
    |]

let letraK =
    [|
        "█    █"
        "█   █ "
        "████"
        "█   █"
        "█    █"
        "█     █"
    |]

let letraSpc =
    [|
        " "
        " "
        " "
        " "
        " "
        " "
    |]

let letraM =
    [|
        "█    █"
        "██  ██"
        "█ ██ █"
        "█    █"
        "█    █"
        "█    █"
    |]

let letraR =
    [|
        "█████ "
        "█    █"
        "█    █"
        "█████"
        "█   █"
        "█    █"
    |]

let letraV =
    [|
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " █  █"
        "  ██"
    |]

let mapaDeLetras =
    [
        'A',letraA
        'L',letraL
        'I',letraI
        'N',letraN
        'E',letraE
        ' ',letraSpc
        'T',letraT
        'K',letraK
        'C',letraC
        'O',letraO
        'M',letraM
        'R',letraR
        'V',letraV
        'G',letraG
    ]
    |> Map.ofList

let encontrarLetra letra =
    mapaDeLetras
    |> Map.tryFind letra
    |> Option.defaultValue letraA
