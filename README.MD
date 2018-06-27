== * WAES TEST * ==

* Suggestions for improvement:

- Better validation
- More/better automated test (improve coverage)
- Remove commented code
- Logs (like error logs, etc)
- Better treatment over HTTP statuses
- Good practices on Dependency Injection (use interfaces i.e IService<T>, create base classes to specialize from)
- Create facades to specialize core (so the business logic can be completely dettached from any asp.net web API dependencies)
- Specialize core (i.e. Create Repositories instead of use DbContext itself, split the namespace in sub namespaces, etc)
- Better configuration (remove some magic strings and hard-coded values from the code AND the tests that shoud be in config.json)

== Usage:

After cloning the repo if you just want to run the tests, go to .tests/ and run <i> dotnet test</i>. 

If you want to run the API, just execute <i> dotnet run</i> on the main folder.

How it works?

Expected JSON structure to create a comparison:

{Id: int, Left: string, Right: string}

URIS: /v1/diff/{id:int}/left, /v1/diff/{id:int}/right and /v1/diff/{id:int}/

POSTs to the first two listed above will create or update an entity with it's right or left part.

GETs to the first two listed above will return it's right or left part.

A GET to the last URI listed above will return the differences between the strings.

