# Core Libraries

All of the 'keywords' are actually functions defined in core libraries.

## Types

```
true = _
false = _
bool = true | false

minInt = -2147483648
maxInt = 2147483647
int = minInt | .. | maxInt

char = 'a' | .. | 'z'
string = char[]

year = 0 | .. | 9999
month = january | february | march | april | may | june | july | august | september | october | november | december
day = 1 | .. | 31
date = (year, month, day)

hour = 0 | .. | 23
minute = 0 | .. | 59
second = 0 | .. | 59
millisecond = 0 | .. | 999
time = (hour, minute, second, millisecond)

datetime = (date, time)

iterator = {
    moveNext = _ => bool
    getCurrent = _ => _
}
sequence = {
    getIterator = _ => iterator
}
array = sequence & { length = number }
```

## Imperitive functions

### not

`not` is just a simple match that reverses a `bool`.

```
not = b => b --> (
    true => false
    false => true
)
```
```
isAsleep = not isAwake
```

### if then

`if` is a curried function that accepts a `condition`, then a `func`.

`then` is a unit type that is passed into the `func` just to make it look nice. It's a little language trick.

```
then = _

if = condition => func => {
    condition --> (
        true => func then
        false => _
    )
}
```

```
if conditionIsSatisfied then => {
    
}
```

### loop until

Similarly, the `loop` function accepts a function that accepts `until` as a trick parameter.

```
until = _

loop = func => {
    if (func until) then => {
        // scary magic to goto start of the outer block
    }
}
```
```
loop until => {
    print "Tick";
    sleep seconds 1
    stop = greaterThanOrEqual seconds 60
}.stop
```

### while then

```
while = condition => func => {
    loop until => func then
}
```

```
while conditionIsSatisfied then => {

}
```

### do while

`do while` works exactly the same as `loop until`.

The `while` "keyword" here is the same function used in `while then`, but its just passed in as a parameter and never executed.

```
do = func => {
    loop until => (func while)
}
```

### for

The `for` loop iterates through a sequence and executes a function with each item passed in as the parameter.

```
for = sequence => forFunction => {
    iterator = sequence.getIterator _
    loop until => {
        hasMoved = iterator.moveNext _
        if hasMoved {
            forFunction current.getCurrent _
        }
    }.hasMoved
}
```
```
for [1, .., 99] i => {
    print $"The number is {i}";
}
```

### foreach

The `foreach` loop is similar, but expects a sequence of functions.

```
foreach = sequence => forFunction => {
    for sequence getItem => {
        item = getItem _
        forFunction item
    }
}
```
```
foreach myItems item => {
    print "I have an item";
}
```

### switch

```
switch x case 
```

### yield

This one might have to be a language feature.

### async await

