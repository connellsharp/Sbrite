# Advanced Type System

## Everything is a type

Types are types, but so are concrete values, and so are functions. Objects are types, tuples are types, even parameters blocks are types.

What matters is the type hierarchy. `7` is an `integer`, an `integer` is a `number`, and a `number` is a `_`. Everything is a `_`.

## Scoping

Types can be overridden in a sub scope only to make them more specific.

```
name = string

{
    name = "Fred" // valid because "Fred" is a subtype of string

    print name // will print "Fred"
}

{
    name = 123 // invalid because 123 is not a subtype of string
}
```

## Refinement Types

Intersection types create more restricted types; the new type must be assignable to all the types in the intersection.

This idea can be combined with a predicate function to create a refinement type. To do this, we create an intersection of any type with an object containing an `isValid` function.

```
multipleOf5 = integer & { isValid = i => modulo i 5 == 0 }
```

The type `multipleOf5` can only accept numbers that are multiples of 5. `10` is a valid `multipleOf5`.