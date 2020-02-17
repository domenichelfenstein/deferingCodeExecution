open System

let (|Over10|UpTo10|) id = if id > 10 then Over10 else UpTo10

let getName (id : int) =
    async {
        Console.WriteLine(@"""GetName"" gets executed")

        do! Async.Sleep 500

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
    let id = argv.[0] |> int
    let name = getName id
    printNameOver10 id name |> Async.RunSynchronously
    0