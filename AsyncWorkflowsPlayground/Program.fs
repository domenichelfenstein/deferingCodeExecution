open System

let (|Over10|UpTo10|) id = if id > 10 then Over10 else UpTo10

let getName (id : int) =
    async {
        Console.WriteLine(@"""GetName"" gets executed")

        do! Async.Sleep 500

        return "Hans Maulwurf (id: " + id.ToString() + ")"
    }

let printNameOver10 id (name : Lazy<Async<string>>) =
    async {
        match id with
            | Over10 ->
                let! value = name.Force()
                Console.WriteLine(value)
            | UpTo10 ->
                Console.WriteLine("under 10")
    }
    
let replay task =
    let mutable memorizedValue = null
    match memorizedValue with
        | null ->
            async {
                let! result = task
                memorizedValue <- result
                return result
            }
        | _ -> async { return memorizedValue }

[<EntryPoint>]
let main argv =
    async {
        let id = 20
        let name = lazy(getName id |> replay)

        let! n = name.Force()    // comment in to show that getName is executed only once

        do! printNameOver10 id name
    } |> Async.RunSynchronously
    0