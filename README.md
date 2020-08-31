# Eventshuffle backend API

Eventshuffle is an application to help scheduling events with friends.
An event is created by posting a name and suitable dates to the backend,
events can be queried from the backend and participants can submit dates suitable for them.

## Requirements
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Build and run
To install SQL Server, you can use Docker with the Linux image/container, but on Windows,
you can of course also use the installation tool.

### Run SQL Server

#### Docker
For the docker way, I included a docker-compose. Please change the password in the yml and run:

```
docker-compose up -d
```

To keep the data of the databases, don't 'down' it,
but use ```docker-compose stop``` and ```docker-compose start``` to stop and start the container.

#### Non-docker
Start SQL Server if it is not already running.

### API
NOTE: In the instructions below, replace ```<YourStrong@Passw0rd>``` with your chosen password.

In the project root run:

```
dotnet tool install --global dotnet-ef

cd src/Infrastructure

dotnet user-secrets set "ConnectionStrings:EventshuffleDbContext" "Server=tcp:localhost,1433;Initial Catalog=Eventshuffle;Persist Security Info=False;User ID=SA;Password=<YourStrong@Passw0rd>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30"

dotnet ef database update

cd ../Api

dotnet user-secrets set "ConnectionStrings:EventshuffleDbContext" "Server=tcp:localhost,1433;Initial Catalog=Eventshuffle;Persist Security Info=False;User ID=SA;Password=<YourStrong@Passw0rd>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30"

dotnet build
dotnet run --launch-profile Development
```

Open Swagger at https://localhost:5001/swagger/index.html for documentation.

## API design
In contrast to the example, I used plural nouns for the endpoints instead of the singular style from the example: 
(GET event/list => GET events), because IMHO it is a best practise in API design.

## Architecture
For this scope, a simple MVC approach probably would have been sufficient (KISS),
but I used several patterns to help scaling up and of course show things :).

In a simple Controller -> Model approach I have seen that over time the controller gets very fat and it is hard to maintain.
A better version is the Controller -> Service -> Repository approach,
but the Services also tend to grow very big and often have injected a lot of other services.
The responsibility of these services get unclear and all the dependencies make it harder to maintain.
Also repositories often tend to grow to a huge query collection. Every time a new query is added, the interface is changed.

One pattern I have used to address the problems mentioned above and decouple things is CQRS with Mediator.
Of course it also has some drawbacks, like more classes, less DRY and some 'magic',
but the modularity it gives is a big plus IMHO. It also enables isolating fast SQL in query objects.

In this project I wanted to show this pattern (although some CQRS rules are broken),
combined with some aspects from DDD and Clean Architecture (layering, rich entities)
I have seen and used in previous projects.

## TODO
- More tests / integration tests
- More validation
- Add fields to database tables like 'created'
- Add logging. SeriLog and / or use ApplicationInsights
