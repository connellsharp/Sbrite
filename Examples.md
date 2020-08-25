# Sbrite Examples

## FizzBuzz

The classic FizzBuzz kata can be achieved by assigning two refinement types, looping through a range and matching.

```
multipleOf3 = integer & { isValid = i => modulo i 3 == 0 }
multipleOf5 = integer & { isValid = i => modulo i 5 == 0 }

foreach [0, .., 100] i => {
    print i --> (
        multipleOf3 & multipleOf5 => "FizzBuzz"
        multipleOf3 => "Fizz"
        multipleOf5 => "Buzz"
        i => $"{i}"
    )
}
```


## Web Controller

```
request --> (
    { method = get, path = startswith "/users/" } => {
        status = 200

        headers = [
            ("ETag", "asdfajenuiersdfghg")
        ]

        body = {
            this = is
            basically = json
        }
     }
)

```
