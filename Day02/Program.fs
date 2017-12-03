// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open FSharp.Data;
open System;

let readData = 
    let csv = CsvFile.Load("data.csv", separators=";", hasHeaders=false)
    let rows = csv.Rows

    rows |> Seq.map(fun r -> r.Columns |> Seq.map(fun col -> Int32.Parse(col)))

let rowDiff (lst: seq<int>) =
    let min = lst |> Seq.min
    let max = lst |> Seq.max
    max-min

let calculateChecksum (matrix:seq<seq<int>>) =
    let diffs = matrix |> Seq.map(fun row -> rowDiff(row))
    diffs |> Seq.sum
    

[<EntryPoint>]
let main argv = 
    let data = readData
    let result = calculateChecksum data
    printfn "%A" result

    Console.ReadLine();
    0 // return an integer exit code
