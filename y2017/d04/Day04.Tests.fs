module Day04Tests

open Day04
open FsUnit
open Xunit

[<Fact>]
let ``[Part 1] "aa bb cc dd ee" is a valid passphrase`` ()=
    isValid "aa bb cc dd ee" |> should be True

[<Fact>]
let ``[Part 1] "aa bb cc dd aa" is not a valid passphrase`` ()=
    isValid "aa bb cc dd aa" |> should be False
   
[<Fact>]
let ``[Part 1] "aa bb cc dd aaa" is a valid passphrase`` ()=
    isValid "aa bb cc dd aaa" |> should be True