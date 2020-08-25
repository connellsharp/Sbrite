## Return

A block already has a return type; itself. So returning a specific value from a block is achieved by immediately navigating into the returned object's properties using a `.`. You can use any property name, but `return` makes the intention clear.

```
getCustomerEmail = customerId => {
     customer = getCustomer customerId
     return = customer.email
}.return
```

This technique isn't a special language feature. It doesn't jump out of the block as a `return` statement would do in other languages.

```
getCustomerEmail = customerId => {
     customer = getCustomer customerId
     return = customer.email

     print "This line is still executed"
}.return
```

## Yield return

"Yield return" functionality from C# can be represented as an array of functions. We can chose to accept a parameter type of `yield` to make our intentions clearer, but the parameter is never used.

```
numberWords = [
    yield => "One"
    yield => {
        doSomeStuff _
        return = "Two"
    }.return
    yield => "Three"
]
```

The `foreach` function iterates through each of these items, executes the function, then passes the return value into another function.

```
foreach numberWords numberWord => {
    print numberWord
}
```

## Async await

Awaiting a callback can be done with a sequence too. This time, a sequence of `task` objects.

> :warning: Not really sure how this can fit together. Work in progress.

```
getUser = await [
    response = client.get $"/user/{}"
    response
    json = parseJson response.body
]
```