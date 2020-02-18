# Observer Translate

A .NET Core 3.1 Console Application. Type a word or phrase and it gets simultaneously translated into multiple languages using the Google Translate API service.

A spike project to demonstrate the below:

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

- ~~Add Exception handling~~

- ~~Add multiple translation services~~

- ~~Add Observer Pattern~~

- ~~Add Retry Policy~~

- ~~Add Logging~~

- ~~Implement thread safety~~

- ~~Unit test TranslateLog.GetNextColour~~

  

## Notes
I wanted the language translators (observers) to be driven by config.  To add an extra translator, add an entry to the `targetLanguages` array in `appicationsettings.json` . The list of available languages can be found here: https://cloud.google.com/translate/docs/languages

The IoC is responsible for subscribing the observers (`TranslateObserver`) to the observable (`TranslateObservable`). See `ApplicationIoC.cs`

Each `TranslateObserver` outputs their translation to the `ITranslateOutputter`. As this class can be called by many observables it is configured as a singleton and a lock is used to ensure thread safety in the concrete implementation.

The user of translate.googleapis.com site is very limited. It only allows about 100 requests per one hour period and there after returns a **429 error (Too many requests)**. So if you start to get errors wait an hour or so :)

I added Serilog to provider structured logging. It's currently configured to write json structured logs to a daily text file `logYYYYMMDD.txt`.

Added a Polly retry policy to the Http Client Factory to retry calls to translate.googleapis.com.