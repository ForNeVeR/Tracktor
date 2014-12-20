module Tracktor.Monitor.FixView

open FSharp.Desktop.UI
open FsXaml
open System.Windows
open System.Windows.Data

type FixWindow = XAML<"FixWindow.xaml", true>

type FixView(window : FixWindow) =
    inherit View<FixViewEvent, FixModel, Window>(window.Root)

    override __.EventStreams = []
        
    override __.SetBindings model =
        Binding.OfExpression 
            <@
                window.Content.Content <- model.Commit.Revision
            @>

let create() =
    let window = FixWindow()
    FixView window
