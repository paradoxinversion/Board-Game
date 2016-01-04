using UnityEngine;

public static class GameUtilities
{
    public enum GameMode
    {
        PLAYER_TURN,                //the player has yet to act this round
        PLAYER_CHOOSING_ROUTE      //the player must select a route
    }
    public static GameMode gameMode = GameMode.PLAYER_TURN;
    public static GameBoardTile GetTileById(int id)
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int x = 0; x < tiles.Length; x++)
        {
            if (tiles[x].GetComponent<GameBoardTile>().tileNumber == id)
            {
                return tiles[x].GetComponent<GameBoardTile>();
                
            }
        }
        return null;
    }
    
}

