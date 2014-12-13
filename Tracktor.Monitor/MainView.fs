module Tracktor.Monitor.MainView

open FSharp.Desktop.UI
open FsXaml
open System.Windows

type MainWindow = XAML<"MainWindow.xaml", true>

type MainView(window : MainWindow) =
    inherit View<MainViewEvent, ApplicationModel, Window>(window.Root)

    override __.EventStreams = [
        window.ExitButton.Click |> Observable.mapTo Exit
        window.Root.Closing |> Observable.map (fun event -> event.Cancel <- true; Exit)
    ]
        
    override __.SetBindings model = ()

let create() =
    let window = MainWindow()
    MainView window
