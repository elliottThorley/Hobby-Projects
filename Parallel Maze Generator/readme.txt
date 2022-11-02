THIS IS DESIGNED TO RUN ON WESTERN MICHIGAN UNIVERSITY SUPERCOMPUTER ONLY

Allows the user to randomly generate ASCII mazes of a specified size, using a specified number of threads, with the option to show the maze solution.
The maze generation algorithm is credited in theFinalDesignDoc, I added parallel computing capibility.

To Compile:  >make
To run:  >./maze (width) (height) (threads) (solved)
Example: >./maze 100 100 5 0
Example on Specific Node: >bpsh n8 ./maze 100 100 5 0
