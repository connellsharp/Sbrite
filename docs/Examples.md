# Sbrite Examples

## FizzBuzz

The classic FizzBuzz kata can be achieved by assigning two [refinement types](TypeSystem.md#refinement-types), looping through a range and matching.

```
multipleOf3 = integer & { isValid = i => is modulo i 3 0 }
multipleOf5 = integer & { isValid = i => is modulo i 5 0 }

foreach [0, .., 100] i => {
    print i --> (
        multipleOf3 & multipleOf5 => "FizzBuzz"
        multipleOf3 => "Fizz"
        multipleOf5 => "Buzz"
        i => $"{i}"
    )
}
```


## Web API

A web API endpoint is just matching a request object looking and returning a response object.

```
request --> (
    { method = get, path = "/users" } => {
        status = 200

        headers = [
            ("ETag", "asdfajenuiersdfghg")
        ]

        body = {
            this = is
            basically = json
        }
    }
    { method = get, path = "/customers" } => {
        status = 200

        body = [ "Also", "can be", "an array" ]
     }
)
```

You could use another parameter to inject dependencies, for example a mediator or an MVC controller.

```
executeRequest = (request, thingsController) => {
    request --> (
        { method = post, path = "/things" } => {
            requestBody = jsonParse postThingRequest request.body
            
            newThing = thingsController.create {
                name = requestBody.name
            }

            status = 201

            headers = [
                ("Location", pathCombine [request.url, newThing.id])
            ]
        }
    )
}
```

The request and response types are defined like this:

```
request = {
    method = get | post | put | delete
    path = string
}

response = {
    headers = [ (string, string) ]
    body = _
}

response = yourFunction request
```

## HTTP API wrapper module

This example wraps up HTTP calls into a class-like object and interface.

```
thing = { name = string }
apiError = { message = string }

thingsApiClient = {
    getThing = id => task (thing | apiError)
}

thingsApiClientImpl = {
    getThing = id => await $[
        response = httpClient.get $"/things/{i}"

        response --> (
            requestError => 
                completedTask apiError & { message = "Network error getting the thing" }

            { statusCode: 200, body } => await $[
                thingJson = body.readAsString
                completedTask jsonParse thing thingJson
            ]
        )
    ]
    createThing thing => await $[
        response = httpClient.get $"/things/{i}"
        thingJson = readBody response.body
        thing = jsonParse thingJson
    ]
}
```

