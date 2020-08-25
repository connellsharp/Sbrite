# Functions


```
inc = { i = integer } => {
    
}
```
## One parameter

Functions have exactly one parameter.

## Assignee Syntax

Normal tuple deconstruction looks like this:

```
( x y ) = getPoint myLocation

print x
print y
```

The same syntax works for function parameters.

```
printPoint = ( x y ) => {
    print $"The point is ({x}, {y})"
}
```

Similarly, normal object deconstruction looks like this:

```
{
    myX = x
    myY = y
} = getPoint myLocation

print myX
print myY
```

And the same syntax works for function parameters.

```
printPoint = {
    x = float
    y = float
} => {
    print $"The point is ({x}, {y})"
}
```

The left hand side of the `=>` in a function syntax is the same thing as the left hand side of the `=` operator in a normal assignment. This is the **asignee syntax**.

Calling a function can be thought of as assigning the parameter first then stepping into the block. A single parameter is therefore the same thing as assigning an alias before stepping into the block.

## Type inference

The problem with a single alias and tuple deconstruction is we have nowhere to specify a type. Blocks do this with assignments within the blocks.

The short function syntax using a simple parameter is only made possible with type inference.

```
inc = i => add ( i 1 )
```

If no type can be interred from any usage within the function, the top type `_` is used, making it very unlikely that any usage within the function will compile.

## Type names as constraints

There are occassions when you want to constrain a functions parameter type but never use the value in the function.

A typical example of this is when pattern matching.

```
myBool --> (
    true => "Yay"
    false => "Nay"
)
```

The same logic does continue. You're assentially still assigning an alias `true = true` within the scope of the function, which is within the scope overriding rules. But it doesn't matter, because its just an alias to the one with the same name in the parent scope.