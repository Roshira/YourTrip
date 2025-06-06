rontend part of the project: https://github.com/yuliia-zinchenko/yourTrip


The frontend and backend are on different computers, so to fully run it, you need to run them simultaneously and it is recommended from the same network. Or you can do this using Swagger. Run from the .Web folder by writing the dotnet run in the console.
Clean Architecture is used for easier support and adding new functionality and reducing program dependencies. Of the minuses, this solution increases the amount of code that needs to be written, especially at the beginning, the further the more justified.
Tests for services have been written.
Also, 3 laboratory Excel files were implemented called Sorted, which shows the difference between multi-threaded and sequential sorting of users with different sampling and also reading from the API of restaurants in Paris but with one selection due to the capabilities of the free API.
