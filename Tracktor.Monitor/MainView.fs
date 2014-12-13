namespace Tracktor.Monitor

open FSharp.Desktop.UI
open FsXaml
open System.Windows

type MainWindowEvent = Exit

type MainWindow = XAML<"MainWindow.xaml">

type MainView() =
    inherit View<MainWindowEvent, MainWindowModel, Window>(MainWindow().Root)

    override __.EventStreams = []
    override __.SetBindings model = ()
