# Old Ideas I've Thrown Away

## Function params as a normal block

The parameter block is just a normal block. The types within the parameter block's scope can be accessed from the return block.

And a block is just a normal type too. Instead of using a block for a parameter, you could make an alias.

```
incParams = { i = integer }

inc = incParams => {
    
}
```

This leads to interesting behaviour when using a primitive type, such as a `string` as the parameter.

```
printLength = string => {
    print $"The length of that string is {length}"
}

getLength = string => length
```


## Return operator `<-`

The `<-` operator replaces the type of the containing block with the given value.

In an imperitive block, this can be thought of as something like the `return` keyword used in C-like languages.

```
{
    doWork _
    <- "Done"
}
```

When used in an assignment, it can be used to descope 

```
age = {
    <- 42
}
```

This is the same as going this

```
age = {
    return = 42
}.return
```