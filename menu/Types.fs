module App.Types

type MenuCommand =
| NewGame
| LoadGame
| Exit

type PauseCommand =
| Continue
| SaveGame
| Exit

type GameOverCommand =
| NewGame
| Exit
