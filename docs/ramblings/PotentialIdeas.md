# Potential Ideas I Might Still Introduce

## Match statement as a union of functions

Consider a union of functions that reveals itself as a single function with a union parameter.

```
printFull = string => print string | integer => print getFullNumberWord integer
printFull = string | integer => _ // signature
```

The expectation would be that passing this function a `string` argument would invoke the first function, but passing an `integer`  would invoke the second. This can be written as a match:

```
printFull = param => param --> (
    string => print string
    integer => print getFullNumberWord integer
)
printFull = string | integer => _ // signature
```

So perhaps a match operator doesn't need to exist, and doesn't need to accept a tuple of functions to work. Instead, the `-->` is kind of a 'pipe' operator, and you just pass the parameter into a union of functions.

For extra syntactic sugar, we could introduce some multiline union syntax where the first line is also prefixed with a `|` character.

```
condition --> 
    | true => print "Yup"
    | false => print "Nope"
```

## Interpolated string deconstruction

Like tuples and objects, interpolated strings could use their own deconstruction when used as part of the [assignee syntax](../Functions.md#assignee-snytax).

```
$"/users/{id}" = "/users/123"
print id // 123
```

A problem here that doesn't exist when using interpolation outside of the assignee syntax is using two variables without a delimiter.

```
$"/users/{id}{suffix}" = "/users/123suffix"
print id // how do we know where id stops and suffix starts?
```

Perhaps no `$` symbol is needed for interpolation. Maybe we can just concatenate strings by combining them without spaces. This makes the above problem impossible to encouter.

```
"/users/"id = "/users/123"
path = "/users/"id
```

That introduces the reverse problem in the execution syntax where we can't combine two strings without a delimeter any more. That's easily solved by using an empty string between.

```
path = "/users/"id""suffix
```

But of course this raises the original problem again. In the assignee snytax, how does it know where to split the strings?

```
"/users/"id""suffix = "/users/123suffix"
```

In that case, we could add some 'greedy' rule like Regex. The left-most variable should be assigned the most characters possible. In this case, `suffix` will be empty.