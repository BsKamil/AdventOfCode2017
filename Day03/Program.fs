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

let nextLocation(s:MemorySlot, l:Location) = {
                Location=l
                Direction=s.Direction
                LayerNumber=s.LayerNumber
                Elem=s.Elem+1
                }

let nextLayer(s:MemorySlot, l:Location)={
                Location=l
                Direction=Up
                LayerNumber=s.LayerNumber+1
                Elem=s.Elem+1
                }

let nextSlot (slot: MemorySlot)=
    match slot.Direction with
    | Up -> if layerRightUp(slot) 
              then changeDirection(slot, Left)
              else nextLocation(slot,goUp(slot.Location))
    | Left -> if layerLeftUp(slot) 
              then changeDirection(slot, Down)
              else nextLocation(slot,goLeft(slot.Location))
    | Down -> if layerLeftDown(slot) 
              then changeDirection(slot, Right)
              else nextLocation(slot,goDown(slot.Location))
    | Right -> if layerRightDown(slot) 
               then nextLayer(slot, goRight(slot.Location))
               else nextLocation(slot,goRight(slot.Location))

let rec findSlot(num:int, acc: MemorySlot)=
    if num=acc.Elem then acc
    else findSlot(num, nextSlot(acc))

let manhatanDistance(l1:Location, l2:Location)=
    Math.Abs(l1.X-l2.X)+Math.Abs(l1.Y-l2.Y)

[<EntryPoint>]
let main argv = 
    let input=265149
    let start = {Location={X=0;Y=0}; Direction=Up; LayerNumber=0; Elem=1}
    let slot = findSlot(input,start)
    let result = manhatanDistance(start.Location,slot.Location)

    printfn "%A" result
    Console.Read()
    0 // return an integer exit code
