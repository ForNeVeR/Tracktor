namespace Tracktor.Database

type IRepository<'Data> =
    abstract Save : 'Data -> Async<unit>
    abstract Load : int64 -> Async<'Data>
