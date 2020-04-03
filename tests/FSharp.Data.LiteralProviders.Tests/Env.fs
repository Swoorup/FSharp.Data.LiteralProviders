module Tests.Env

open NUnit.Framework
open FSharp.Data.LiteralProviders

[<Test>]
let ``OS is either Windows_NT or unix`` () =
    Assert.Contains(Env.OS.Value, [|"Windows_NT"; "unix"|])

type ``OS without default`` = Env<"OS">
type ``OS with default`` = Env<"OS", "Invalid">
type ``Random var from env file`` = Env<"RANDOM_VAR_FROM_ENV_FILE">

[<Test>]
let ``Random var from env file is set`` () =
    Assert.IsTrue(``Random var from env file``.IsSet)

[<Test>]
let ``OS without default is set`` () =
    Assert.IsTrue(``OS without default``.IsSet)

[<Test>]
let ``OS with default is set`` () =
    Assert.IsTrue(``OS with default``.IsSet)

[<Test>]
let ``OS without default is either Windows_NT or unix`` () =
    Assert.Contains(``OS without default``.Value, [|"Windows_NT"; "unix"|])

[<Test>]
let ``OS with default is either Windows_NT or unix`` () =
    Assert.Contains(``OS with default``.Value, [|"Windows_NT"; "unix"|])

type ``Garbage without default`` = Env<"SomeGarbageVariableThatShouldntBeSet">
type ``Garbage with default`` = Env<"SomeGarbageVariableThatShouldntBeSet", "some default value">

[<Test>]
let ``Garbage variable without default is not set`` () =
    Assert.IsFalse(``Garbage without default``.IsSet)

[<Test>]
let ``Garbage variable without default is empty string`` () =
    Assert.AreEqual("", ``Garbage without default``.Value)

[<Test>]
let ``Garbage variable with default is not set`` () =
    Assert.IsFalse(``Garbage with default``.IsSet)

[<Test>]
let ``Garbage variable with default is default`` () =
    Assert.AreEqual("some default value", ``Garbage with default``.Value)
