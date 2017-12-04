// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.IO

let passwordValid (w:string[]) = w.Length=(w |> Array.distinct |> Array.length)

//Part 2

let passValid2 (w:String[])= 
        w.Length=(w |> Array.map(fun str -> str |> Seq.sort 
                                                |> Seq.toArray
                                                |> String)
                    |> Array.distinct 
                    |> Array.length
                 )
[<EntryPoint>]
let main argv = 
    let lines = File.ReadAllLines("data.txt") |> Array.map(fun l -> l.Split(' '))
    let result1 = lines |> Seq.filter(fun ln -> passwordValid ln) 
                        |> Seq.sumBy(fun i -> 1)
    
    printfn "%A" result1

    let result2 = lines |> Seq.filter(fun ln -> passValid2 ln)  
                        |> Seq.sumBy(fun i -> 1)

    printfn "%A" result2
    Console.ReadLine()
    0 // return an integer exit code
