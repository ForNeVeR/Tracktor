namespace Tracktor.Database

type IRepository<'Data> =
    abstract Save : 'Data -> int64 Async
    abstract Load : int64 -> 'Data Async
