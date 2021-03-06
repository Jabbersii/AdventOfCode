module Day09

let rec scoreImpl (str: char list) (score: int) (acc: int) (garbage: int) =
        match str with
        | [] -> (acc, garbage)
        | '{'::tail -> scoreImpl tail (score + 1) (acc) garbage
        | '<'::tail -> ignoreGarbage tail score acc garbage
        | '}'::tail -> scoreImpl tail (score - 1) (acc + score) garbage
        | ','::tail -> scoreImpl tail score acc garbage
    and ignoreGarbage (str: char list) (score: int) (acc: int) (garbage: int)=
        match str with 
        | '>'::tail -> scoreImpl tail score acc garbage
        | '!'::tail -> ignoreGarbage (List.tail tail) score acc garbage
        | _::tail -> ignoreGarbage tail score acc (garbage + 1)

let score (input: string): int = scoreImpl (input |> Seq.toList) 0 0 0 |> fst

let score2 (input: string): int = scoreImpl (input |> Seq.toList) 0 0 0 |> snd

module Tests =
    open FsUnit
    open Xunit

    [<Fact>]
    let ``[Part 1] {} scores 1`` ()=
        score "{}" |> should equal 1

    [<Fact>]
    let ``[Part 1] {{{}}} scores 6`` ()=
        score "{{{}}}" |> should equal 6

    [<Fact>]
    let ``[Part 1] {{},{}} scores 5`` ()=
        score "{{},{}}" |> should equal 5

    [<Fact>]
    let ``[Part 1] {{{},{},{{}}}} scores 16`` ()=
        score "{{{},{},{{}}}}" |> should equal 16

    [<Fact>]
    let ``[Part 1] {<a>,<a>,<a>,<a>} scores 1`` ()=
        score "{<a>,<a>,<a>,<a>}" |> should equal 1

    [<Fact>]
    let ``[Part 1] {{<ab>},{<ab>},{<ab>},{<ab>}} scores 9`` ()=
        score "{{<ab>},{<ab>},{<ab>},{<ab>}}" |> should equal 9

    [<Fact>]
    let ``[Part 1] {{<!!>},{<!!>},{<!!>},{<!!>}} scores 9`` ()=
        score "{{<!!>},{<!!>},{<!!>},{<!!>}}" |> should equal 9

    [<Fact>]
    let ``[Part 1] {{<a!>},{<a!>},{<a!>},{<ab>}} scores 3`` ()=
        score "{{<a!>},{<a!>},{<a!>},{<ab>}}" |> should equal 3

    [<Fact>]
    let ``[Part 2] <> has 0 garbage`` ()=
        score2 "<>" |> should equal 0

    [<Fact>]
    let ``[Part 2] <random characters> has 17 garbage`` ()=
        score2 "<random characters>" |> should equal 17

    [<Fact>]
    let ``[Part 2] <<<<> has 3 garbage`` ()=
        score2 "<<<<>" |> should equal 3

    [<Fact>]
    let ``[Part 2] <{!>}> has 2`` ()=
        score2 "<{!>}>" |> should equal 2
      
    [<Fact>]
    let ``[Part 2] <!!> has 0 garbage`` ()=
        score2 "<!!>" |> should equal 0

    [<Fact>]
    let ``[Part 2] <!!!>> has 0 garbage`` ()=
        score2 "<!!!>>" |> should equal 0

    [<Fact>]
    let ``[Part 2] <{o"i!a,<{i<a> has 10 garbage`` ()=
        score2 """<{o"i!a,<{i<a>""" |> should equal 10