using UnityEngine;

public class Player : MonoBehaviour {
    /// <summary>
    /// Which player is this?
    /// </summary>
    public int playerIndex = -1;
    /// <summary>
    /// Which tile is this player on?
    /// </summary>
    public int currentTileNumber = -1;
    public Placeable placeable;

    /// <summary>
    /// Selects a direction for the player to go when they must choose.
    /// </summary>
    /// <param name="tile">The tile that has been selected</param>
    public void SelectRoute(GameBoardTile tile)
    {
        placeable.SelectMovementTile(tile);
    }
}
