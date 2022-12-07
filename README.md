# restest

## Restest is an simple cli tool for testing website response times

### Usage:
restest [mode] [seconds | count] [url]
example: restest T 5 localhost:3000

### Modes:
T: Time, sends requests to the url within the timeframe.
C: Count, sends X amount of requests to the url.
