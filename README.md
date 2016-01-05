# Board-Game
A simple board game framework in Unity3d

**Using the framework**
First, set up the scene by adding at least one Sprite with a GameBoardTile Monobehavior, as well as a GameObject with the GameManager behavior.

*Tiles*
Each Tile Sprite should have its TileNumber changed. Sequential tiles should be numbered as such. 
If a tile has multiple exits, their Tile Numbers should be added to the TileConnections array. 
If a tile has no connections that are sequential, it must be marked as Jump Tile, and it must have at least one tile connection out.

**Players**
Players are aded by the GameManager, through the PreparePlayers method, which must be supplied an integer. 
