https://app.pluralsight.com/course-player?clipId=50c71725-b786-4ed8-aea6-393775cbe590

#Dependency Injection in ASP.NET Core
By Steve Gordon

## IServiceProvider
1st register services using IServiceCollection
Can then use IServiceProvider.GetService(Type), or usually use constructor injection and let framework call it as needed

## ServiceDescriptor
( ImplementationType, ServiceType, LifeTime, e.t.c. e.t.c. )

```c#
var serviceDescriptor1 = new ServiceDescriptor(typeof(IWeatherForecaster),
  typeof(WeatherForecaster), ServiceLifeTime.Singleton );
  swervices.Add(serviceDescriptor1);
```

services.TryAdd() stops multiple concretes being accidently registered 

## Multiple concretes being registered
You can DI a IEnumerable<IFoo> in constructors

Assembly scanning isnt supported by dotnetcore default DI 
It does support registering open generics tho

```c#
  services.TryAddSingleton(typeof(IDistrubutedCache<>), typeof(DistriubutedCache<>))
```

serviceProvider.GetRequiredService<RoleManager<TennisBookingsRole>()
https://andrewlock.net/the-difference-between-getservice-and-getrquiredservice-in-asp-net-core/

Scrutor : Performs assembly scanning, by extending Microsoft container
And allows use of decorator pattern

