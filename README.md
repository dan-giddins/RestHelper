# Introduction 
This Rest Helper has been created in order to provide an easy and standardized method of making REST reqeusts to an API.

# Getting Started
This libary can be installed from the UoN dotnet package feed via nuget. Once installed:

1. Use the static `AuthenticationHelper` class to create a `AuthenticationHeaderValue` object by calling the static class's methods. Currently two authetication types are supported:
    * `CreateBasicAuthHeader()` - Basic Authentication
    * `CreateTokenHeader()` - Token Authentication
1. Instantiate an object of type `IRestHelper`, passing in the base URL (the part of the URL that is consistent for all the API calls that your `RestHelper` will make), and the `AuthenticationHeaderValue` you just created.
1. Use the `RestHelper` object by awaiting its methods and passing in the relevant Types and Arguments (always the part of the URI specific to that request, and in the case of a PATCH, POST or PUT, the object to be posted).
    * Currently GET, PATCH, POST and PUT is implemented.
    * If you pass one type, the helper assumues that the response object is the same type as the input object.
    * Passing two types tells the helper that the response object is diffrent to the input object.
    * `GetOData()` is used to get an object inside an OData wrapper.
    * `...ReturnJson()` returns the raw JSON response.

# Exceptions

Every method in the `RestHelper` class throws a `UnsuccessfulRequestException` if the response is not a successful status code. This Exception contains the status code, reason phrase and response body in the enclosed `HttpRepsonse` object.

# Example Code

```c#
// create header
var header = AuthenticationHelper.CreateBasicAuthHeader(
    "username",
    "password");
// create the restHelper
IRestHelper restHelper = new RestHelper(
    "https://www.website.com/api",
    header);
// get a 'cat' object with id 79
Cat cat = await restHelper.Get<Cat>("cats/getendpoint/79");
cat.Name = 'Dan';
// post the object back in
// we don't need to specify the type here
// C# infers this from the object being posted in
Cat response = await restHelper.Post("cats/postendpoint", cat);
```
