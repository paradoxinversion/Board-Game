using UnityEngine;
public class GameManager : MonoBehaviour {
    private static GameManager _instance = null;
    public static GameManager Get()
    {
        if (_instance == null)
        {
            _instance = (GameManager)FindObjectOfType(typeof(GameManager));
        }

        return _instance;
    }
    /// <summary>
    /// Does the player loop to start when they hit the final tile, or do they stop there?
    /// </summary>
    public bool loopingGameBoard;
    public GameObject playerPrefab;
    public Player player;
    /// <summary>
    /// Sets up player game objects and components.
    /// </summary>
    /// <param name="players">How many players are in this game?</param>
    public void PreparePlayers (int players)
    {
        for (int x = 0; x < players; x++)
        {
            GameObject playerObject = Instantiate(playerPrefab) as GameObject;
            Player playerComponent = playerObject.GetComponent<Player>();
            Placeable placeableComponent = playerObject.GetComponent<Placeable>();
            playerObject.transform.SetParent(GameObject.Find("PlayerRoot").transform);
            playerComponent.placeable = placeableComponent;
            playerComponent.playerIndex = x;
            playerComponent.currentTileNumber = 0;
            placeableComponent.PlaceAtTile(0);
            if (x == 0)
            {
                player = playerComponent;
            }
        }
    }
    public void MakeMovementRoll(int highestValue)
    {
        if (GameUtilities.gameMode == GameUtilities.GameMode.PLAYER_TURN)
        {
            int movement = Random.Range(1, highestValue + 1);
            Debug.Log("Moving " + movement + " spaces.");
            player.placeable.MoveByAmount(movement);
        }
    }

    public void MoveBySpaces(int spaces)
    {
        player.placeable.MoveByAmount(spaces); //This will cause an error if the player is already in mid-movement.
    }
    void Start () {
        PreparePlayers(1); //For testing purposes. This should be deleted for actual games.
	}
	
}
