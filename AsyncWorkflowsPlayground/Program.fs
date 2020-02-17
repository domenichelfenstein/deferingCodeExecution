open System
open System.Threading.Tasks

let getName (id : int) =
    async {
        Console.WriteLine(@"""GetName"" gets executed")

        Task.Delay(500) |> Async.AwaitTask |> ignore

        return "Hans Maulwurf (id: " + id.ToString() + ")"
    }

let printNameOver10 id (name : Async<string>) =
    async {
        match id with
        | x when x > 10 ->
            let! nameValue = name
            Console.WriteLine(nameValue)

        | _ ->
            Console.WriteLine("under 10")
    }

[<EntryPoint>]
let main argv =
    let id = argv.[0] |> int
    let name = getName id
    printNameOver10 id name |> Async.RunSynchronously
    0