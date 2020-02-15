# Observer Translate

A .NET Core 3.1 Console Application. Type a word or phrase and it gets simultaneously translated into multiple languages using the Google Translate API service.

A demo project to demonstrate the below:

- Implement a simple application around the Observer pattern.

- Application of SOLID principles.

- Exception Handling and Logging.

- Thread safety.

- Asynchronous code.

  

## Todo

- ~~Add Unit Test Project~~

- ~~Add configuration via appsettings.json~~

- ~~Add Dependency Injection~~

- ~~Add HttpClientFactory for querying translation API~~

- Add Exception handling

- ~~Add multiple translation services~~

- ~~Add Observer Pattern~~

- Add Retry Policy

- Add Logging

- ~~Implement thread safety~~

- Unit test TranslateLog.GetNextColour

  

## Notes
The languages translators are driven by config.  To add an extra language add an entry to the `targetLanguages` in `appicationsettings.json` . The list of available languages can be found here: https://cloud.google.com/translate/docs/languages

For each language added a new observer is created by the IoC. See `Startup.cs`

Each  `TranslateObserver` logs their translation to the `ITranslateLog`. As this class can be called by many observables it is configured as a singleton and a lock is used to ensure thread safety in the concrete implementation.

The translate.googleapis.com site use is very limited. It only allows about 100 requests per one hour period and there after returns a **429 error (Too many requests)**.