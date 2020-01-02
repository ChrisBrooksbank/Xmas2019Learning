# Writing High-Performance C#

[Talk](https://www.youtube.com/watch?v=NVWQRbqcXJ4)
[Slides](http://bit.ly/highperfcode90)

## Aspects of Performance
Execution Time
Throughput ( e.g. max requests per second )
Memory Allocations

Microservices

Performance should be a part of every story

Performance versus code readability.

[Benchmarking Library](https://benchmarkdotnet.org/)
Provides high precision metrics.

## Span<T>
Useful to keep parsing code high performance

In System.Memory package

Provides a read/write view over a contigous region of memory
* Heap
* Stack
* Native / unmanaged

Its threadsafe

Strings are immutable so use readonly spans for strings
Ensures reference is kept 

Slicing a span is a constant time/cost operation

Can call Slice method

Cannot ever live on the heap ( so cant be a class field for example )

## Memory<T>
Can live on the heap

Readonly struct but not a ref struct

Can call span method to get a spoan inside your method 

## String.Create
Create string and setup its value as its being created

## ArrayPool
Pool of arrays for re-use
[Blog post](https://adamsitnik.com/Array-Pool/)

ArrayPool<T>.Shared.Rent(int length) // will usually get a bigger array back then requested, may also have data in it
ArrayPool<T>.Shared.Return(t[] array,bool clearArray=false)

## System.IO.Pipelines
Created by ASP.NET to improve Kestrel requests per seconds., David Fowler has video
PipeWriter and PipeReader

IValueTaskSource

ReadOnlySequence

## JSON APIs ( JSON.NET stays )
Low level : Utf8JosnReader Utf8JsonWriter ( only if very high perf needed )
Midlevel : JsonDocument
High-Level : JsonSerializer and JsonDeserializer
Use new perf capabilities

Coming: JsonPipeWriter and JsonStreamWriter e.t.c.

