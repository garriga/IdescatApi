# IdescatApi
Contains a .net client for using the IDESCAT API (https://www.idescat.cat/dev/api/?lang=en).

## Usage
Now the code only implements a client for the Population service.
This service contains two actions, sug and cerca (look  at IDESCAT web page for more information). 
The sug service can be used:
```c#
var client = new IdescatApiClient();
// retrieve entities of type Municipality whose name starts with 'sa':
string[] names = await client.Population.SugAsync("sa", TerritorialEntity.Municipality);
// retrieve a parsed json of the response with population data:
var parsedJson = await client.Population.CercaAsync("sa");
var entities = parsedJson.GetEntities();
```
The entities do not contain its type yet, this has to be implemented.
