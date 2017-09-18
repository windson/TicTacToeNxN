# TicTacToeNxN
A Tic Tac Toe of order NxN C#


A war tactician in making, Desmond, is studying battles between the two generals Altair and Ezio. 
He is running simulations of various battles across different battlefields between the generals. 
The battlefields are always of n x n dimension divided into equal sized 1 x 1 sectors. 
The sectors are numbered row-wise from 1 to n*n starting from top left to right bottom 
e.g. a 3x3 battlefield would have sectors numbered as follows:

-------------------
|  1  |  2  |  3  |
-------------------
|  4  |  5  |  6  |
-------------------
|  7  |  8  |  9  |
-------------------

Each general can tactically capture only one sector before conceding one to the rival general, and so on. 
Once a sector is captured by a general, the rival general cannot capture that sector. 
As Desmond has a keen eye for tactics, he observes that whichever general captures n sectors in the same row, column or diagonal first invariably wins the battle.
He also observes that whenever any of the generals failed to capture n sectors in the aforementioned fashion, the battle would end up in a stalemate.

Each simulation has the information about the dimension n of the battlefield. 
The simulation also has a series of sectors which were captured by Altair and Ezio (in that order) alternatively. 
As mentioned before, when one general captures a sector, the rival general will capture the next sector invariably 
e.g. if the series of sectors was 3, 1, 2, 4 
then it means that Altair captured sector 3, then Ezio captured sector 1, then Altair captured sector 2 and finally Ezio captured sector 4.

Write to program to help Desmond determine if a battle simulation was won by Altair, Ezio or was a stalemate.

Input:
The dimension of the battlefield n ( 3 < n < = 20), followed by n*n integers a such that 1 <= a <= n*n

Output:
“Altair” if general Altair wins the battle, “Ezio” if general Ezio wins the battle and “Stale” in case of a stalemate.
Note: Although the simulation provides all the sectors captured, the winner could have been determined before all the sectors have been captured.

-----------------------------------------
# Input
3
5 9 3 7 8 2 4 6 1

# Output
Stale
-----------------------------------------
# Input
3
3 1 5 7 9 4 6 8 2

# Output
Ezio
-----------------------------------------
# Input
3
3 1 6 9 5 7 4 8 2

# Output
Altair
-----------------------------------------
