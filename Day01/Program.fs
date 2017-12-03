// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System;
open System.IO;

let readData = File.ReadAllText("data.txt") |> Seq.map(fun c -> Int32.Parse(c.ToString())) |> Seq.toList

let matchItems (prev:int, next:int) =
    match prev=next with
    | true -> next
    | false -> 0

let rec sumList (prev:int, list: int list, acc:int) =
    match list with
    | current::left -> sumList(current, left, acc + matchItems(prev, current))
    | [] -> acc

let sumCyclic (lst: int list) =
    let firstItem = lst |> List.head
    let lastItem = lst |> List.rev |> List.head
    let rest = lst |> List.tail
    sumList(firstItem, rest, 0) + matchItems(firstItem, lastItem)

    //Part 2

let findElement (currentIndex:int, listLength:int) = 
    let maxIndex = listLength-1
    let step = listLength/2
    let maybe = currentIndex+step
    
    match maybe>maxIndex with
    |true -> maybe-maxIndex-1
    |false -> maybe

let sumWithStep (lst: int list) =
    let lng = lst.Length
    let mutable sum = 0

    for i in 0..lng-1 do sum <- sum + matchItems(lst.Item(i), lst.Item(findElement(i, lng)))

    sum

[<EntryPoint>]
let main argv = 
    let data = readData
    let resultPart1 = sumCyclic(data)
    printfn "%A" resultPart1

    let resultPart2 = sumWithStep(data)
    printfn "%A" resultPart2
    Console.ReadLine();
    0 // return an integer exit code
