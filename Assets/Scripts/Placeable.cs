using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Placeable : MonoBehaviour {
    /// <summary>
    /// What tile is the placeable on? It should match its player.
    /// </summary>
    int currentTileNumber;
    /// <summary>
    /// Which tile are we moving to? This should be used when a player has to choose between multiple tiles.
    /// It should be set to null immediately after it has been used.
    /// </summary>
    GameBoardTile selectedMovementTile;
    /// <summary>
    /// Which tiles are we allowd to move to? This is used when a player has to choose between multiple tiles.
    /// It should be cleared after it is no longer necessary.
    /// </summary>
    List<GameBoardTile> movementOptions = new List<GameBoardTile>();
    /// <summary>
    /// Sets the selectedMovementTile, when at a junction. This is called via the player from a clicked tile at a junction.
    /// It only allows a tile to be selected if it is in the Movement Options list.
    /// </summary>
    /// <param name="tile"></param>
    public void SelectMovementTile(GameBoardTile tile)
    {

        if (movementOptions.Count > 0)
        {
            if (movementOptions.Contains(tile))
            {
                selectedMovementTile = tile;
            }
            else
            {
                Debug.Log("The selected tile wasn't in the Movement Options list.");
            }
        }
        else
        {
            Debug.Log("No movement options were valid, this shouldn't have happened.");
        }
    }
    /// <summary>
    /// For placing players on a tile. Do not use to place tiles.
    /// </summary>
    /// <param name="tileNumber">The tile number the Player will be on.</param>
    public void PlaceAtTile(int tileNumber)
    {
        GameObject[] tileGameObjects = GameObject.FindGameObjectsWithTag("Tile");
        for (int x = 0; x < tileGameObjects.Length; x++)
        {
            if (tileGameObjects[x].GetComponent<GameBoardTile>().tileNumber == tileNumber)
            {
                
                transform.position = tileGameObjects[x].transform.position;
                break;
            } 
        }
    }

    /// <summary>
    /// Returns the GameBoardTile the placeable is currently on.
    /// </summary>
    /// <returns></returns>
    public GameBoardTile ReturnCurrentTile()
    {
        return GameUtilities.GetTileById(currentTileNumber);
    }

    /// <summary>
    /// Determines which tiles the character is allowed to move to. 
    /// Mainly for ensuring players can only choose valid tiles.
    /// </summary>
    void SetMovementOptions()
    {
        movementOptions = new List<GameBoardTile>();
        if (!ReturnCurrentTile().isJumpTile)
        {
            movementOptions.Add(GameUtilities.GetTileById(currentTileNumber + 1));
            
        }
        if (ReturnCurrentTile().tileConnections.Length > 0)
        {
            for (int x = 0; x < ReturnCurrentTile().tileConnections.Length; x++)
            {
                movementOptions.Add(GameUtilities.GetTileById(ReturnCurrentTile().tileConnections[x]));
            }
        }
    }

    /// <summary>
    /// Moves the placeable by one tile. It allows selection of different routes if necessary.
    /// </summary>
    IEnumerator MoveToNextTile()
    {
        GameBoardTile currentTile = ReturnCurrentTile();
        
        if (currentTile.isFinalTile)
        {
            currentTileNumber = 0;
        }
        else
        {
            if (!currentTile.isJumpTile)
            {
                if (currentTile.tileConnections.Length == 0)
                {
                    currentTileNumber = currentTileNumber + 1;
                }
                else
                {
                    SetMovementOptions();
                    yield return StartCoroutine("WaitForSelection");
                    currentTileNumber = selectedMovementTile.tileNumber;              
                }
            }
            else
            {
                if (currentTile.tileConnections.Length == 1)
                {
                    currentTileNumber = currentTile.tileConnections[0];
                }
                else
                {
                    SetMovementOptions();
                    yield return StartCoroutine("WaitForSelection");
                    currentTileNumber = selectedMovementTile.tileNumber;
                }
            }
        }

        movementOptions.Clear();
        GetComponent<Player>().currentTileNumber = currentTileNumber;
        PlaceAtTile(currentTileNumber);
        selectedMovementTile = null;
        yield return null;
    }

    /// <summary>
    /// Keeps the turn from continuing until the player has chosen a route at a junction.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForSelection()
    {
        GameUtilities.gameMode = GameUtilities.GameMode.PLAYER_CHOOSING_ROUTE;
        Debug.Log("Choose a Tile");
        bool validSelection = false;
        while (!validSelection)
        {
            if (movementOptions.Contains(selectedMovementTile))
            {
                validSelection = true;
                GameUtilities.gameMode = GameUtilities.GameMode.PLAYER_TURN;
            }
            yield return null;
        }
        yield return null;
    }
    /// <summary>
    /// Moves a thing a certain amount of spaces on the board, or to the last space if the amount is too high.
    /// </summary>
    /// <param name="spaces">How many spaces are we moving?</param>
    IEnumerator MoveSpaces(int spaces)
    {
        for (int x = 0; x < spaces; x++)
        {
            yield return StartCoroutine("MoveToNextTile");
        }
        
        yield return null;
    }

    /// <summary>
    /// Calls the MoveSpaces coroutine. Use this when moving players.
    /// </summary>
    /// <param name="amt"></param>
    public void MoveByAmount(int amt)
    {
        StartCoroutine(MoveSpaces(amt));
    }
 
}
