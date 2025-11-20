//
// RPC
//  Remote Procedure Calling
//  Llamado a procedimiento remoto
//
//  Es una tecnica para llamar a una funcion que esta en otro computador.
//
// Internet, tecnicamente la fecha es la de Arpanet
//  TCP/IP Vincent Cerf.
// Microsoft invento SOAP, y para eso creo XML
// HTML -> Tim Berne Lee

//
// REST -> La informacion es transmitida en formato JSON
//
// Recientement Google invento un nuevo protocolo: gRPC
// 

//
// REST necesita conectarse a un URL (uniform resource locator)
// https://google.com (esto es un URL)
//
//
// <protocol>://<path>
// https://google.com
// file://Program.fs
// gopher://example.com
// ftp://ftp.google.cpom

open System
open System.Net.Http
open System.Text.Json

type Person = {
  birth_year: int option
  death_year: int option
  name: string
}

type Book = {
    id: int
    title: string
    subjects: string array
    authors: Person array
    summaries: string array
    translators: Person array
    bookshelves: string array
    languages: string array
    copyright: bool option
    media_type: string
    formats: Map<string,string>
    download_count: int
}

type Answer = {
  count: int
  next: string option
  previous: string option
  results: Book array
}
let getURLAsync (url:string) =
    task {
        use cliente = new HttpClient()
        let! respuesta = cliente.GetAsync url
        let! body = respuesta.Content.ReadAsStringAsync()
        return body
    }

//
// Esta funcion usa el operador de Composicion
//
let getURL = getURLAsync >> Async.AwaitTask >> Async.RunSynchronously


//
// API-> Application Programmers Interface
//
let result = getURL @"https://gutendex.com/books/?search=Lovecraft%20Mountain"

let libros = JsonSerializer.Deserialize<Answer> result

//
// Si funciona debemos tener algo de informacion aqui
//
printfn $"Numero de libros: {libros.count}"

printfn "----Titulos----"
//
// Vamos a imprimir el titulo de los libros encontrados
//
libros.results
|> Array.iter (fun libro -> printfn $"{libro.title}")

//
// en este mapa estan los formatos y deonde obtener
// el librp mismo. Ejemplo para el primer libro de la lista
//
printfn "----Formatos----"
libros.results[0].formats
|> Map.iter (fun key value -> printfn $"{key} -> {value}")
