# Sbrite Programming Language

## What is this

An experimental programming language aiming to minimise language features in favour of reusing similar concepts.

## Ground rules

1. **There are no keywords.** All language operators are made up of special characters. Common 'keywords' such as `if` and `foreach` are in fact functions defined in [core libraries](Core.md).
2. **Values are just derived types.** A concrete value can be thought of as a subtype or an implementation of an interface. [Everything is a type](TypeSystem.md).
3. **Functions have exactly one parameter.** Multiple parameters can be thought of as a single object or tuple.
4. **No semi colons**. Breaking up statements is done by either a new line or a comma; the two are interchangable.

## Language Operators

### Assignment `=`

Values can be assigned to aliases using the `=` operator.

```
i = 1
name = "Fred"
```

This looks and behaves like assigning a variable in other languages, but it can also be thought of as assigning new types. `name` is a new type; a subtype of `"Fred"`.

The same operator can assign aliases, like defining a type alias.

```
username = string
email = string
```

In this case `username` is a `string`, but a `string` is not necessarily a `username`. `username` is a subtype of `string`.

### Discard `_`

The `_` is the type right at the very top of the hierarchy that all other types inherit.

This has several uses. One being a way to declare unit types.

```
unit = _
ack = _
```

### Unions `|`

The `|` operator defines union types. A union can be thought of as an 'or' type.

```
identifier = username | email
suit = hearts | clubs | diamonds | spades
```

An `identifier` can be either a `username` or an `email`. Both `username` and `email` are now subtypes of `identifier`.

### Intersections `&`

Similarly, the `&` operator defines intersection types.

```
boat = vehicle & floater
```

A `boat` is both a `vehicle` and a `floater`. It is a subtype of both types.

### Tuples `( )`

The `( )` parenths create tuples: immutable groups of types.

```
point = (float, float)
origin = (0, 0)
```

The comma looks nice here, but for cases when they don't, commas are always interchangable with new lines.

```
bigTuple = (
    longerTypeName
    anotherLongType
)
```

### Blocks `{ }`

The `{ }` brackets can wrap up several types together, like an object.

```
person = {
    firstName = name
    lastName = name
}
```

In fact, this is just an imperitve block of code, assigning two new types within a scoped block. You can add other lines of code to the block and it works the same.

```
person = {
    firstName = name
    lastName = name

    print "Hello World"
}
```

The `print` function will execute once when the assignment is made. The `person` type signature will look like the previous example.

### Navigation `.`

Navigating into a property of an object is done with the `.` character.

```
person = {
    firstName = name
    lastName = name
}

personsLastName = person.lastName
```

### Functions `=>`

The `=>` denotes a function. As in many other languages, the arrow is considerred a 'goes to' operator.

Functions are first-class citizens and can be assigned like anything else.

```
inc = i => i+1
```

Instinctively, you can return a block from a function.

```
doWork = i => {
}
```

Functions always have one parameter. Sometimes its useful to use an `_` to discard the parameter or a tuple to mimic multiple parameters.

```
doWork = _ => {
}

plotPoint = (x, y) => {
}
```

Or perhaps a curried approach.

```
getPoint = x => y => {
    
}
```

### Matching `-->`

The `-->` operator passes a type into a tuple of functions. The operator will look for a function that can have the type assigned to its parameter.

```
condition --> (
    true => print "Yup"
    false => print "Nope"
)
```

Order matters. The first function with a matching parameter expression will be invoked and those below that won't be checked.

```
command --> (
    "--help" => print "Help is on the way!"
    string => print "I don't know that command..."
)
```

### Sequences `[ ]`

Similarly to a block and a tuple, `[ ]` brackets can wrap up several statements into one.

Like a tuple, assignments need not be made for the values to be exposed.

```
arr = [ 1, 2, 3 ]
```

Unlike a tuple, a common subtype of all sequence members is found and the type `arr` is a subtype of `[number]`.

As with everything, commas can be replaced with new lines.

```
arr = [
    "How about an array of longer sentences?"
    "Or what if they're all questions?"
    "Would you prefer statements?"
    "Am I really still asking questions?"
]
```

Assignments can be made in a sequence and will behave the same as an imperitive block of code. Assigned types will be returned as part of the sequence.

```
arr = [
    $"Game playing: {currentGame.name}"
    gameStats = getStats currentGame.id
    $"Total wins: {gameStats.totalWins}"
]
```

### Range `..`

The `..` operator includes several numbers in a sequence with every integer between the number before and after in the sequence.

```
topTen = [1, .., 10]
topTen = [1, 3, 7, 10, .., 99]
```

It can also be used within unions to create a union of a range.

```
hour = 0 | .. | 23
```

