// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.IO

let sep=' '
let words (str:string) = str.Split(sep)
let passwordValid (w:string[]) =
     w.Length=(w |> Array.distinct |> Array.length)

[<EntryPoint>]
let main argv = 
    let input = File.ReadAllLines("data.txt")
    let result = 
        input |> Seq.filter(fun ln -> passwordValid(words ln)) 
        |> Seq.sumBy(fun i -> 1)
    
    
    printfn "%A" result
    Console.ReadLine()
    0 // return an integer exit code
