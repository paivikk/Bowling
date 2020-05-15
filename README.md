# Bowling
## Build
Open the solution file in Visual Studio (version 2017 Pro used in implementation), and select to build the solution with the preferred configuration.

Or build on commandline e.g. as follows:
MSBuild.exe Bowling.sln

Readily built release executable is also available in this repository.

## Execution
The executable expects input in the format defined for the use cases in the specification:
[a1|b1] [a2|b2] [a3|b3] ... [an|bn|cn]

Examples:
Bowling "[7|3] [5|5] [0|10] [1|1] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0]"
Bowling "[5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5|5]"

The executable outputs the score of the game on the console.
