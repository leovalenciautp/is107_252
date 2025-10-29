//
// Listas
//  Es muy lento acceder un elemento de la lista
//

//
// Nuevo typo de datos, llamado 
// Array
//
printfn "Creando array...."
let a = [|1..1_000_000_000|]
printfn "Done.!"

printfn "Buscando ultimo elemento.."
let ultimo = a[999_999_999]

printfn $"{ultimo}"

//
// List -> Ventaja: Agregar un nuevo elemento es bastante rapido, 
//          toma tiempo O(1) (tiempo constante)
//          Agregar un elemento no incrementa la memoria en mas de lo
//          que el nuevo elemento require.
//
//      Desventajas:
//          una lista de n elementos, comsume mas memoria que un
//            array de n elementos
//
//
// Array -> Ventaja: Acceder a cualquier elemento del array es
//         super rapido, tiempo O(1).
//
//        Desventaja: Agregar un nuevo elemento al array, duplica
//        el uso de memoria, y es muy lento, de tiempo O(2*n)

//
// List: [4;8;7;9]

let miLista = [4;8;7;9]
//
// miLista es una direccion de memoria donde esta el primer elemento
//  miLista -> [4 (direcion del siguiente elemento) ]
//                -> [8 (direccion del siguiente)]

//
let nuevaLista = 10 :: miLista

let miArray = [|4;8;7;9|]
// --------------------
// +       4           +
// _____________________
// --------------------
// +       8           +
// _____________________
// --------------------
// +        7          +
// _____________________
// --------------------
// +        9          +
// _____________________

let nuevoArray = miArray |> Array.insertAt 0 10