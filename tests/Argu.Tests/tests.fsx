﻿#I "../../bin/net40"
#r "Argu.dll"
#r "Argu.Tests.dll"

open Argu
open Argu.Tests

type PushArgs =
    | Remote of name:string
    | Branch of name:string
with
    interface IArgParserTemplate with
        member this.Usage = "push"

[<CliPrefix(CliPrefix.Dash)>]
type CleanArgs =
    | D
    | F
    | X
with
    interface IArgParserTemplate with
        member this.Usage = "clean"

[<CliPrefix(CliPrefix.Empty)>]
type GitArgs =
    | Push of ParseResult<PushArgs>
    | Clean of ParseResult<CleanArgs>
with 
    interface IArgParserTemplate with 
        member this.Usage = "git"

let parser = ArgumentParser.Create<GitArgs>()

let result = parser.Parse [|"clean" ; "-fdfx"|]

result.GetResult(<@ Clean @>).GetAllResults() // [F; D; F; X]