﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.IO

let sep=' '
let words (str:string) = str.Split(sep)
let passwordValid (w:string[]) =
     w.Length=(w |> Array.distinct |> Array.length)

//Part 2

let passValid2 (w:String[])=
    w.Length=(w |> Array.map(fun str -> str |> Seq.sort |> Seq.toArray |> String)
                |> Array.distinct |> Array.length)

[<EntryPoint>]
let main argv = 
    let input = File.ReadAllLines("data.txt")
    let result1 = 
        input |> Seq.filter(fun ln -> passwordValid(words ln)) 
        |> Seq.sumBy(fun i -> 1)
    
    printfn "%A" result1

    let result2 = 
        input |> Seq.filter(fun ln -> passValid2(words ln)) 
        |> Seq.sumBy(fun i -> 1)

    printfn "%A" result2
    printfn "%A" (passValid2(words "abcde xyz ecdab"))

    Console.ReadLine()
    0 // return an integer exit code
