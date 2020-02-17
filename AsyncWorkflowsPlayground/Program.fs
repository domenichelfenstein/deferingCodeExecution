open System

let (|Over10|_|) id = if id > 10 then Some Over10 else None

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
        | _ ->
            Console.WriteLine("under 10")
    }

[<EntryPoint>]
let main argv =
    let id = argv.[0] |> int
    let name = getName id
    printNameOver10 id name |> Async.RunSynchronously
    0