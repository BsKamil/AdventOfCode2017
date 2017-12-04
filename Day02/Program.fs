// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open FSharp.Data;
open System;

let readData =  CsvFile.Load("data.csv", separators=";", hasHeaders=false).Rows 
                    |> Seq.map(fun r -> r.Columns |> Seq.map(fun col -> Int32.Parse(col)))

let rowDiff (lst: seq<int>) = (lst |> Seq.max)-(lst |> Seq.min)

let calculateChecksum (matrix:seq<seq<int>>) = matrix |> Seq.map(fun row -> rowDiff(row)) 
                                                      |> Seq.sum
//Part two

let findEventlyDivine (lst: seq<int>) =
    let rec doModuloForElem(elem:int, lst:int list)=
        match lst with
        | next::rest -> if elem%next=0 then elem/next else doModuloForElem(elem,rest)
        | [] -> 0

    let rec findInSorted(nums: int list)=
        match nums with
        | head::tail -> if doModuloForElem(head,tail)>0 then doModuloForElem(head,tail) else findInSorted(tail)
        | [] -> 0

    lst |> Seq.sortDescending 
        |> Seq.toList 
        |> findInSorted

[<EntryPoint>]
let main argv = 
    let result1 = readData |> calculateChecksum
    printfn "%A" result1

    let result2 = readData |> Seq.map(fun row -> findEventlyDivine(row)) 
                           |> Seq.sum
    printfn "%A" result2
    Console.ReadLine();
    0 // return an integer exit code
