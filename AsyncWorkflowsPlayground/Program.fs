open AsyncLazy
open System

let (|Over10|UpTo10|) id = if id > 10 then Over10 else UpTo10

let getName (id : int) =
    AsyncLazy(
        async {
            Console.WriteLine(@"""GetName"" gets executed")

            do! Async.Sleep 500

            return "Hans Maulwurf (id: " + id.ToString() + ")"
        })

let printNameOver10 id (name : AsyncLazy<string>) =
    async {
        match id with
        | Over10 ->
            let! value = name.Force()
            Console.WriteLine(value)
        | UpTo10 ->
            Console.WriteLine("under 10")
    }

[<EntryPoint>]
let main argv =
    let id = 20 // argv.[0] |> int
    let name = getName id

    // let n = name.Force() |> Async.RunSynchronously    comment in to show that getName is executed only once

    printNameOver10 id name |> Async.RunSynchronously
    0