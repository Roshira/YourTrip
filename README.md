Frontend part of the project: https://github.com/yuliia-zinchenko/yourTrip
Documentation wiht Doxigen: https://roshira.github.io/YourTrip/
Video demonstration backend part: loading...

The frontend and backend are on different computers, so to fully run it,
you need to run them simultaneously and it is recommended from the same network.
Or you can do this using Swagger. Run from the .Web folder by writing the dotnet run in the console.

Clean Architecture is used for easier support and adding new functionality
and reducing program dependencies. Of the minuses, this solution increases
the amount of code that needs to be written, especially at the beginning, the further the more justified.

Tests for services have been written.

Also, 3 laboratory Excel files were implemented called "3LabAndSort", which shows the difference between multi-threaded and
sequential sorting of users with different sampling and also reading from the API of restaurants
in Paris but with one selection due to the capabilities of the free API.

Also in this project used Design Patterns:

Generative Patterns:
Factory Pattern (DesignTimeDbContextFactory):
Encapsulates the logic of object creation.
Example: YourTripsDbContextFactory is used by EF Core tools to create a DbContext instance at design time (e.g. for migrations).

Structural Patterns:
Facade Pattern: Provides a simplified interface to a complex subsystem.
Example: UserManager, SignInManager with ASP.NET Core Identity simplify complex authentication and user management operations.
AmadeusAuthService can be considered as a facade for authentication with Amadeus. SavDelJSONModel is a facade for saving/deleting different types of JSON data.
Adapter Pattern:
Allows objects with incompatible interfaces to work together.
Example:
Services that interact with external APIs
(AmadeusFlightSearchService, BookingApiService, GooglePlacesService) act as adapters. They translate requests from your application
into the format expected by the external service, and vice versa.

Behavioral Patterns:
Strategy Pattern: allows you to define a family of algorithms, encapsulate each of them, and make them interchangeable.
Example: IUserSortingService and IRestaurantSorter with their implementations (UserSortingService, ParisRestaurantSorter).
Although one implementation is shown here, the very existence of an interface for sorting implies the possibility of replacing algorithms.

Generative Patterns:
Dependency Injection (DI) / Inversion of Control (IoC):
Reduces coupling, makes it easier to test and replace components.
Example: Almost everywhere. ServiceCollectionExtensions.cs (both in Web and Infrastructure) heavily uses services.AddScoped, services.AddHttpClient, etc. Controllers and services get dependencies through constructors.
Structural Patterns:

Options Pattern: The standard way to work with configuration in ASP.NET Core.
Example: services.Configure<SmtpSettings>(config.GetSection("SmtpSettings")) and IOptions<SmtpSettings> injection.
Mapper Pattern: Reduces the amount of routine code for copying data between objects in different layers.
Example: AutoMapper (MappingProfile.cs) for converting between Entities and DTOs (e.g. Route and RouteDto).
Behavioral Patterns:
Controller Pattern (MVC/API): Process HTTP requests, validate input data, and delegate business logic to services.
Example: All *Controller.cs files in YourTrips.Web.Controllers.
