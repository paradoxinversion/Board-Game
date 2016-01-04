﻿using UnityEngine;
public class GameBoardTile : MonoBehaviour
{
    /// <summary>
    /// All colors the tile may use. Should be at least as large as the TileColors enum.
    /// The first color should be blank (white)
    /// </summary>
    public Color32[] colorArray;
    public enum TileColors
    {
        UNINITIALIZED,
        GREEN,
        BLUE,
        RED
    }
    /// <summary>
    /// The tile's number on the game board. Also serves as th etile's id.
    /// </summary>
    public int tileNumber;
    /// <summary>
    /// What number tiles can be entered from this one, aside from the next sequential one.
    /// If no tile connections exist, te only movement is sequentially forward.
    /// If isJumpTile is true, there MUST be at least one connection. 
    /// The first tile in the array is assumed to be the tile to jump to.
    /// </summary>
    public int[] tileConnections;
    public TileColors tileColor = TileColors.UNINITIALIZED;
    /// <summary>
    /// Does this tile proceed to a specific, non-sequential tile? THERE MUST BE AT LEAST ONE TILE CONNECTION.
    /// </summary>
    public bool isJumpTile;
    /// <summary>
    /// Is this the 'last' tile on the board?
    /// </summary>
    public bool isFinalTile;
    // Use this for initialization
    void Start()
    {
        tileColor = (TileColors)Random.Range(1, 4);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = colorArray[(int)tileColor];
    }

    public void OnMouseDown()
    {
        //Selects this tile if the player has to choose a route.
        if (GameUtilities.gameMode == GameUtilities.GameMode.PLAYER_CHOOSING_ROUTE)
        {
            GameManager.Get().player.SelectRoute(this);
        }
    }
}
