# Building an Async API with ASP.NET Core

## Why

To avoid a service becoming slow or unavailable under high traffic levels. Typically returning a HTTP error status of 503 to clients, indicating that a server is temporarily unable to handle the request.

By supporting vertical scalability on the server. To support more requests per second by adding server resources such as processing power.

## How
Synchronous code reduces scalability, it gets a thread from the thread pool, but doesnt release it until the request has been fully processed. A significent amount of this time may be spent waiting for a IO operation ( e.g. filesystem, database or network call) to complete. This is time the thread isnt available to the threadpool, for other requests.

Async code returns threads to the threadpool ASAP. This improves the vertical scalability at server level. 

The thread is returned to the threadpool whilst the IO request is running, a thread is only required from the thread pool when the IO request has been complete.

*note*
This session looks at IO bound work, not computational bound work. Async should not be used on the server for computational-bound work. This could reduce scalability.

## Code

### Terms
A thread is a basic unit of CPU utilization.

A thread thats handling an async request is freed up to handle other requests. It doesnt wait idly for an I/O operation to finish.

Multi threading means a single CPU or CPU core can execute multiple threads concurrently.

Concurrency is a condition that exists when at least two threads are making progress.

Parallelism means that at least two threads are excuting simulataneously. Achieved on API if webserver has a multi core processor.

### Data access layer
Start at the bottom with your data access layer

#### async & await
Mark a method with the async modifier.
This allows us to use the await keyword within that method.
It also allows this method to be awaited.

The compiler transforms the method into a state machine

Await is an operator. It tells the compiler that the async method cant continue until the awaited asyc process is complete. Control returns to caller, potentially all the way back up to the thread being freed.

[Example code](https://github.com/KevinDockx/BuildingAsyncAPIAspNetCore/blob/master/Finished%20sample/Books/Books.Api/Services/BooksRepository.cs)

```c#
public async Task<Book> GetBookAsync(Guid id)
{
    return await _context.Books.Include(b => b.Author)
        .SingleOrDefaultAsync(b => b.Id == id);
}
```

#### Task and Task<T>
How do we know when a awaited method has completed ? 
By use of the Task or Task<T> type returned by the async method.
( or type with an accessible GetAwaiter method, or which implements ICriticalNotifyCompletion )

Tasks have properties such as Status, IsCancelled, IsCompleted, IsFaulted.

### Controllers
Having started added async to bottom layer of code, data access, this section shows adding async support at the top level, the controller.

```c#
[HttpGet]
[BooksResultFilter]
public async Task<IActionResult> GetBooks()
{
    var bookEntities = await _booksRepository.GetBooksAsync();
    return Ok(bookEntities);
}
```

(BooksResultFilter is where this course implments mapping from entities to models)

[ExampleCode](https://github.com/KevinDockx/BuildingAsyncAPIAspNetCore/blob/master/Finished%20sample/Books/Books.Api/Controllers/BooksController.cs)

## Measuring with load testing
If you want to do your own load testing, e.g. to measure the improvements of using async then the tool referenced in this course is the free, West Wind WebSurge.

You can define requests, the amount of time to fire requests for, the number of threads to use to send requests for that amount of time. It shows you statistics at end of run.

[VideoDemo](https://app.pluralsight.com/course-player?clipId=83cb7ef8-3614-4434-a3d9-56958d4ba1b7)

The course work uses some temporary code to throttle threads to help see benefits of async code versus sync code in local load tests :

```C#
// throttle the thread pool (set available threads to amount of processors)
// for purposes of local load testing only, dont check in !!
// https://github.com/KevinDockx/BuildingAsyncAPIAspNetCore/blob/master/Finished%20sample/Books/Books.Api/Program.cs
ThreadPool.SetMaxThreads(Environment.ProcessorCount, 
    Environment.ProcessorCount);
```

[Thread throttling code in situ](https://github.com/KevinDockx/BuildingAsyncAPIAspNetCore/blob/master/Finished%20sample/Books/Books.Api/Program.cs)

It also adds an artificial delay for same reason, to simulate a slow IO task :

```c#
public async Task<IEnumerable<Book>> GetBooksAsync()
{
    // Dont check this artifical delay in !
    await _context.Database.ExecuteSqlCommandAsync("WAITFOR DELAY '00:00:02';");
    return await _context.Books.Include(b => b.Author).ToListAsync();
}
```

[Slow IO simulation code in situ](https://github.com/KevinDockx/BuildingAsyncAPIAspNetCore/blob/master/Finished%20sample/Books/Books.Api/Services/BooksRepository.cs)

### Asynchronously Manipulating Resources
This section of course looks at improving scalability by supporting inserting multiple resources using a single request.

#### Waiting for multiple tasks to complete
Use Task.WhenAll() or Task.WhenAny() when executing multiple tasks in parallel.

#### Cancelling Tasks
Cancelling tasks frees up threads, improving scalability.

If one task fails you may want to cancel any related tasks.

See CancellationTokenSource and CancellationToken types.

[Example Code](https://github.com/KevinDockx/BuildingAsyncAPIAspNetCore/blob/master/Finished%20sample/Books/Books.Api/Services/BooksRepository.cs)

```c#
 private CancellationTokenSource _cancellationTokenSource;
```

Pass the _cancellationTokenSource.Token into to your async methods :
```c#
var downloadBookCoverTasksQuery =
     from bookCoverUrl
     in bookCoverUrls
     select DownloadBookCoverAsync(httpClient, bookCoverUrl, _cancellationTokenSource.Token);
```

If a task fails, trigger the cancellation :
```c#
var response = await httpClient.GetAsync(bookCoverUrl, cancellationToken);

if (response.IsSuccessStatusCode)
{
    var bookCover = JsonConvert.DeserializeObject<BookCover>(
        await response.Content.ReadAsStringAsync());
    return bookCover;
}

_cancellationTokenSource.Cancel();
```

Cleanup cancellation token sources ( as with datacontexts ) as required 
```c#
protected virtual void Dispose(bool disposing)
{
    if (disposing)
    {
        if (_context != null)
        {
            _context.Dispose();
            _context = null;
        }
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
```

#### Handling exceptions
Catch OperationCancelledException which exposes the CancellationToken.

## Avoiding Common Pitfalls
[Clip](https://app.pluralsight.com/course-player?clipId=11580530-8b04-44f4-ba2d-887a7034050c)

e.g.
* Dont wrap synchronous, computational bound, code with async methods. Await is optimised for I/O bound work, not computational bound.
* Dont block async code, e.g. by returning Task.Result from a method, or calling .Wait() on task.
* Dont modify shared state, its not threadsafe and can cause difficult to detect bugs.

## Further information

Building an Async API with ASP.NET Core pluralsight course by Kevin Dockx
[Course](https://app.pluralsight.com/library/courses/building-async-api-aspdotnet-core/)
[CourseCode](https://github.com/KevinDockx/BuildingAsyncAPIAspNetCore)

