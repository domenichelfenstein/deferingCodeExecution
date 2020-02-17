using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace TaskPlayground
{
    class Program
    {
        #region Unoptimized

        static async Task Main(string[] args)
        {
            var id = Convert.ToInt32(args.Single());
            var name = await GetName(id);
            PrintNameOver10(
                id,
                name);
        }

        static void PrintNameOver10(
            int id,
            string name)
        {
            if (id > 10)
            {
                Console.WriteLine(name);
            }
            else
            {
                Console.WriteLine("under 10");
            }
        }
        
        #endregion
        
        #region Just defer the task execution, stupid!
        
        // static async Task Main(string[] args)
        // {
        //     var id = Convert.ToInt32(args.Single());
        //     var nameTask = GetName(id);
        //     await PrintNameOver10(
        //         id,
        //         nameTask);
        // }
        //
        // static async Task PrintNameOver10(
        //     int id,
        //     Task<string> nameTask)
        // {
        //     if (id > 10)
        //     {
        //         var name = await nameTask;
        //         Console.WriteLine(name);
        //     }
        //     else
        //     {
        //         Console.WriteLine("under 10");
        //     }
        // }
        
        #endregion

        #region Time for Observables to shine...
        
        // static async Task Main(string[] args)
        // {
        //     var id = Convert.ToInt32(args.Single());
        //     var nameObs = GetName(id)
        //         .ToObservable();
        //     await PrintNameOver10(
        //         id,
        //         nameObs);
        // }
        //
        // static async Task PrintNameOver10(
        //     int id,
        //     IObservable<string> nameObs)
        // {
        //     if (id > 10)
        //     {
        //         var name = await nameObs;
        //         Console.WriteLine(name);
        //     }
        //     else
        //     {
        //         Console.WriteLine("under 10");
        //     }
        // }
        
        #endregion
        
        #region Doing it right!
        
        // static async Task Main(string[] args)
        // {
        //     var id = Convert.ToInt32(args.Single());
        //     var nameObs = Observable
        //         .FromAsync(() => GetName(id));
        //     await PrintNameOver10(
        //         id,
        //         nameObs);
        // }
        //
        // static async Task PrintNameOver10(
        //     int id,
        //     IObservable<string> nameObs)
        // {
        //     if (id > 10)
        //     {
        //         var name = await nameObs;
        //         Console.WriteLine(name);
        //     }
        //     else
        //     {
        //         Console.WriteLine("under 10");
        //     }
        // }
        
        #endregion
        
        #region Reusable?
        
        // static async Task Main(string[] args)
        // {
        //     var id = Convert.ToInt32(args.Single());
        //     var nameObs = Observable
        //         .FromAsync(() => GetName(id));
        //     var name = await nameObs;
        //     await PrintNameOver10(
        //         id,
        //         nameObs);
        // }
        //
        // static async Task PrintNameOver10(
        //     int id,
        //     IObservable<string> nameObs)
        // {
        //     if (id > 10)
        //     {
        //         var name = await nameObs;
        //         Console.WriteLine(name);
        //     }
        //     else
        //     {
        //         Console.WriteLine("under 10");
        //     }
        // }
        
        #endregion
        
        #region Sure!
        
        // static async Task Main(string[] args)
        // {
        //     var id = Convert.ToInt32(args.Single());
        //     var nameObs = Observable
        //         .FromAsync(() => GetName(id))
        //         .Replay();
        //     // var name = await nameObs; // Comment out for Test with small id!
        //     await PrintNameOver10(
        //         id,
        //         nameObs);
        // }
        //
        // static async Task PrintNameOver10(
        //     int id,
        //     IObservable<string> nameObs)
        // {
        //     if (id > 10)
        //     {
        //         var name = await nameObs;
        //         Console.WriteLine(name);
        //     }
        //     else
        //     {
        //         Console.WriteLine("under 10");
        //     }
        // }
        
        #endregion
        
        static async Task<string> GetName(int id)
        {
            Console.WriteLine(@"""GetName"" gets executed");
            await Task.Delay(500);

            return $"Hans Maulwurf (id: {id})";
        }
    }
}
