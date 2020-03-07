module AsyncLib
    open System
    open System.Threading
    open System.Threading.Tasks

    // https://medium.com/jettech/f-async-guide-eb3c8a2d180a
    let replay a: Async<'a> =
          let tcs = TaskCompletionSource<'a>()
          let state = ref 0
          async {
           if (Interlocked.CompareExchange(state, 1, 0) = 0) then
             Async.StartWithContinuations(
               a, 
               tcs.SetResult, 
               tcs.SetException, 
               (fun _ -> tcs.SetCanceled()))
           return! tcs.Task |> Async.AwaitTask }
          
    // sample for .Net Task function
    let delayTask ms =
        Task.Delay(TimeSpan.FromMilliseconds ms)

