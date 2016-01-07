Each Prefab required for the famework is included in the project. For a quick reference of how to set up scenes, check out the included test scenes.

The minimum requirements for each Scene are:
1. An Object with the GameManager Script
2. A Prefab with the Player and Placeable scripts (This prefab should be set as the GameManagers Player Prefab)
3. At least two tiles (for actual functionality) with the GameBoard Tile Script. Each tile must have an individual tileNumber, and should be sequential, except in the case of Junctions and Jump Tiles.
4. Two Empty, Centered GameObjects named 'PlayerRoot' and 'TileRoot', respectively.

Setting Up Tiles
Each tile should have a TileNumber set, beginning with 0. 
Each Tile should be numbered sequentially, unless it is a Junction or it is a Jump Tile. 
The last tile should be marked as Final Tile.

The board below is an example of how each tile should be numbered in a linear board. There are no Junctions or Jump Tiles.

[10][ 9][ 8][ 7]
[11]        [ 6]
[12]        [ 5]
[13]        [ 4]
[ 0][ 1][ 2][ 3]

Junctions are simply tiles from which a player has more than one option to move. 
A junction is created by adding a Tile's number to the Tile Connections array. 
Since plain tiles always allow sequential movement (unless at the end tile) Junctions simply adds options. 
Whenever a Player hits a Junction, the framework waits for the player to select a valid Tile, and then continues moving. 

Jump tiles are special. They never lead a to sequential tile, and they must have at least one Tile Connection.

Below, two more tiles have been added to the board. 
Tile 5 is a Junction tile, and has only one entry, the integer 14 in its TileConnections array. 
If the player has to move past Tile 6, they can choose to move to either tile 7 or 14.

If they move to 14, they would then proceed to 15 (as 14 is a plain tile). 
Tile 15, hoever, must be a Jump Tile (or final tile) since there is no sequential tile to proceed to afterward. 
If Tile 15 had Tile 12 in its TileConnections, it would effectively allow the Player to move in a smaller loop.
Since Tile 12 is a plane tile, does not have to option of moving to tile 15. 

A Jump Tile with no TileConnections will function in the same way as a Plain Tile, but it allows non-sequential movement. This is very important to remember when creating alternate routes for players.

[10][ 9][ 8][ 7]
[11]        [ 6]
[12][15][14][ 5]
[13]        [ 4]
[ 0][ 1][ 2][ 3]

*Moving the Player*
There is a method provided to move players (GameManager.MakeMovementRoll), which takes an integer representing the amount of sides on a die rolled.

If you wish to directly choose how many spaces a player moves, make a call to GameManager.Get().player.placeable.MoveByAmount. If using this method, be sure to check that the gameMode in GameUtilities has been set to PLAYER_TURN. This will prevent any other movement calls being made while the player is still moving.