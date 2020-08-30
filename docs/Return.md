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

A normal sequence is executed when assigned, like a tuple or block.

```
numberWords = [
    "One"
    doSomeStuff _
    "Two"
    "Three"
]
```

This will create an array of `_` types because the second value is in fact the return value of `doSomeStuff _`.

With the `$` operator the execution is deferred until the sequence is iterated.

```
numberWords = $[
    "One"

    doSomeStuff _
    "Two"
    
    "Three"
]
```

This is syntactic sugar and compiles into a sequence of functions, each passing their assigned variables into the next.

The `foreach` function iterates through each of these items, executes the function, then passes the return value into another function.

```
foreach numberWords numberWord => {
    print numberWord
}
```

## Async await

Awaiting a callback can be done with a sequence too. It is another yield return, but this time, a sequence of `task` objects.

> :warning: Not really sure how this can fit together. Work in progress.

```
getUser = await $[
    response = client.get $"/user/{}"
    json = parseJson response.body
]
```