namespace Tracktor.Database

type IRepository<'Data> =
    abstract Save : 'Data -> Async<unit>
