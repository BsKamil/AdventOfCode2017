open System.Net
open System

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

type Location = {X:int;Y:int;}
type Direction = 
    | Down
    | Left
    | Right
    | Up

type MemorySlot = {
    Location:Location
    Direction: Direction
    LayerNumber: int
    Elem: int
    }

let goRight loc={X=loc.X+1;Y=loc.Y;}
let goDown loc={X=loc.X;Y=loc.Y-1;}
let goLeft loc={X=loc.X-1;Y=loc.Y;}
let goUp loc={X=loc.X;Y=loc.Y+1;}

let layerRightDown slot = slot.Location={X=slot.LayerNumber;Y=(-slot.LayerNumber)}
let layerLeftDown slot = slot.Location={X=(-slot.LayerNumber);Y=(-slot.LayerNumber)}
let layerLeftUp slot =slot.Location={X=(-slot.LayerNumber);Y=slot.LayerNumber}
let layerRightUp slot =slot.Location={X=slot.LayerNumber;Y=slot.LayerNumber}

let changeDirection (s:MemorySlot, d:Direction)={
                Location=s.Location
                Direction=d
                LayerNumber=s.LayerNumber
                Elem=s.Elem
                }

let nextLocation(s:MemorySlot, l:Location, e:int)= {
                Location=l
                Direction=s.Direction
                LayerNumber=s.LayerNumber
                Elem=s.Elem+e
                }

let nextLayer(s:MemorySlot, l:Location, e:int)={
                Location=l
                Direction=Up
                LayerNumber=s.LayerNumber+1
                Elem=s.Elem+e
                }

let nextSlot (slot: MemorySlot, e:int)=
    match slot.Direction with
    | Up -> if layerRightUp(slot) 
              then changeDirection(slot, Left)
              else nextLocation(slot,goUp(slot.Location), e)
    | Left -> if layerLeftUp(slot) 
              then changeDirection(slot, Down)
              else nextLocation(slot,goLeft(slot.Location), e)
    | Down -> if layerLeftDown(slot) 
              then changeDirection(slot, Right)
              else nextLocation(slot,goDown(slot.Location),e)
    | Right -> if layerRightDown(slot) 
               then nextLayer(slot, goRight(slot.Location),e)
               else nextLocation(slot,goRight(slot.Location),e)

let rec findSlot(num:int, acc: MemorySlot)=
    if num=acc.Elem then acc
    else findSlot(num, nextSlot(acc,1))

let manhatanDistance(l1:Location, l2:Location)=
    Math.Abs(l1.X-l2.X)+Math.Abs(l1.Y-l2.Y)

// Part2
let isAdjacent(s1:MemorySlot, s2:MemorySlot)= manhatanDistance(s1.Location,s2.Location)>0 && manhatanDistance(s1.Location,s2.Location)<=2

let appendList(lst:MemorySlot list, elem:MemorySlot)=
    if lst |> List.map(fun m -> m.Location) |> List.contains(elem.Location) 
    then lst
    else elem::lst

let sumAdjacents(lst:MemorySlot list, curr:MemorySlot) = lst |> List.filter(fun x -> isAdjacent(x,curr)) |> List.sumBy(fun x -> x.Elem)

let rec calculateFirstBigger(num:int, all:MemorySlot list, acc:MemorySlot)=
    if num<acc.Elem then acc.Elem
    else calculateFirstBigger(num, appendList(all,acc), nextSlot(acc, sumAdjacents(all,acc)))

[<EntryPoint>]
let main argv = 
    let input=265149
    let start = {Location={X=0;Y=0}; Direction=Up; LayerNumber=0; Elem=1}
    let slot = findSlot(input,start)
    let result = manhatanDistance(start.Location,slot.Location)
    let result2 = calculateFirstBigger(input, [], start)
    printfn "%A" result
    printfn "%A" result2
    Console.Read()
    0 // return an integer exit code
