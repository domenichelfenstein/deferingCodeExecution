open AsyncLib
open System

let (|Over10|UpTo10|) id = if id > 10 then Over10 else UpTo10

let getName (id : int) =
    async {
        Console.WriteLine(@"""GetName"" gets executed")

        // only here to demonstrate interoperability
        // would normally just call Async.Sleep
        do! Async.AwaitTask (delayTask 500.)

        return "Hans Maulwurf (id: " + id.ToString() + ")"
    }

let printNameOver10 id (name : Async<string>) =
    async {
        match id with
            | Over10 ->
                let! value = name
                Console.WriteLine(value)
            | UpTo10 ->
                Console.WriteLine("under 10")
    }

[<EntryPoint>]
let main argv =
    async {
        let id = 20
        let name = getName id |> replay

//        let! n = name   // comment in to show that getName is executed only once

        do! printNameOver10 id name
    } |> Async.RunSynchronously
    0