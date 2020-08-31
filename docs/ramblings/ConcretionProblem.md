# Concretion Problem

> :warning: This is an unsolved problem. This page is just rambling on about the problem really...

Functions that serialise a type for printing to the console or sending via an API request cannot be sure if given parameters or return values are concrete values or just subtypes. Consider this example:

```
delayedPrint = { message = string, delay = timespan } => {
    sleep delay
    print message
}

possibleNames = "Fred" | "Jane" | "Sam"

delayedPrint { message = possibleNames, delay = seconds 200 }
```

A `string` is a `[char] & array`, and `array`s have a `length` property. The value of `possibleNames.length` is `3 | 4`.

The problem also exists with intersections.

```
possibleNames = string & { isValid: s => is s.length greaterThan 5 }
```

Here `possibleNames.length` is `greaterThan 5`, but the compiler probably can't know that.

## When do things need to happen at runtime?

`is` must be a runtime concept. It checks if the actual value of `s.length` at runtime can be assigned to `greaterThan 5`.

`is` is implemented with a match, so the match `-->` operator must be fundamentally a runtime thing.

```
is = a => b => a --> (
        b => true
        _ => false
    )
```

The `print` function can be implemented as iterating the `[char]` and matching on known printable characters.

```
print str => {
    foreach str chr => chr --> (
        'a' => printCharA // magic language function ?
        // etc
    )
}
```

`foreach` calls `getIterator`, `moveNext` and `getCurrent`. How would those behave for one of these non-concrete union or intersection types? What about a different type of array union?

```
triplet = [ 1, 2, 3 ] | [ 4, 5, 6 ]
```

Arrays are just objects with `length` and the sequence properties. `getIterator` had the same signature in both sides of the union, so it still has the same signature. `triplet` appears the same to the type system as a concrete array.

## Intersection

```
possible = { propOne = 1, propTwo = 2 } & { propOne = 1, PropThree = 3 }

impossible = { propOne = 1, propTwo = 2 } & { propOne = 4, PropThree = 3 }

// because
impossible = 1 & 4
```

The compiler should somehow be aware that an intersection of 1 and 4 is not possible. No number can be *certainly* 1 and *certainly* 4 at the same time.

```
impossible = "Fred" & "Jane"
```

## What about a special language type?

One possibility is a special language 'type'. `"Fred" = string & concrete`.

This type breaks one fundamental rule: it is **not** a subtype of a union containing itself.

```
myNewType = concrete | concrete
is myNewType concrete // returns false
```

## Highest known type

```
executeRequest = (request, mediator) => {
    request --> (
        { method = post, path = startswith "/things/" } => {
            requestBody = jsonParse postThingRequest request.body
            
            mediator.send {
                messageType = createThingCommand
                name = requestBody.name
            }

            status = 200
        }
    )
}
```

When using the assignee syntax in a function parameter, the types now in scope from the parameter are the **highest known type** of its possible value. 

## Assignment

```

```