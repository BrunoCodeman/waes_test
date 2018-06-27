== * WAES TEST * ==


* Suggestions for improvement:

- Better validation
- Remove commented code
- Better treatment over HTTP statuses
- Good practices on Dependency Injection (use interfaces i.e IService<T>, create base classes to specialize from)
- Create facades to specialize core (so the business logic can be completely dettached from any asp.net web API dependencies)
- Specialize core (i.e. Create Repositories instead of use DbContext itself, split the namespace in sub namespaces, etc)
- Better configuration (remove some magic strings and hard-coded values from the code AND the tests that shoud be in config.json)

