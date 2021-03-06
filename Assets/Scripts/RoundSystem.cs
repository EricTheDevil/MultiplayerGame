using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum RoundState { START, WAITING, MOVING, WON, LOST, PLAYERONE, DICE, MINIGAME, ROLLING, NEXTROUND}
public class RoundSystem : MonoBehaviour
{
    public static RoundSystem Instance { get; set; }

    public RoundState state;
    public int lap = 0;
    public int maxTurns = 3;

    Unit playerOneUnit;

    public List<int> negativeTiles = new List<int>();
    public List<int> trophyTiles = new List<int>();

    public Tiles tilesPrefab;
    public HUD playerHUD;

    public Transform spawnRoom;
    public GameObject playerOnePrefab;

    public Camera mainCamera;
    public GameObject playerOne;

    public int playersReady = 0;
    float walkingSpeed=5f;
    int diceRoll;


    public Button diceButton; 
    public TMP_Text diceText;
    public TMP_Text trophiesText;

    private float secondsCount;
    bool gameOver;
  
    // Start is called before the first frame update

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("init");
            Instance = this;
            state = RoundState.WAITING;
            StartCoroutine(SetupRound());
        }     
    }
    IEnumerator SetupRound()
    {
        playerOne = playerOnePrefab;
        playerOneUnit = playerOne.GetComponent<Unit>();
        playerOnePrefab.transform.position = spawnRoom.transform.position;

        negativeTiles.Add(4);
        negativeTiles.Add(7);
        negativeTiles.Add(9);
        negativeTiles.Add(14);

        trophyTiles.Add(5);
        trophyTiles.Add(10);
        trophyTiles.Add(15);

        yield return new WaitForSeconds(2f);

        StartCoroutine(MovePlayer());
        //playerHUD.SetHUD(playerOneUnit);
 
        state = RoundState.DICE;
    }
    void PlayerTurn()
    {
        if(state == RoundState.DICE)
        {
            RollDice();
        }
    }
    public void ReturnPos()
    {
        playerOne.transform.position = tilesPrefab.tiles[playerOneUnit.oldPos].position;
    }
    IEnumerator MovePlayer()
    {
        if (state == RoundState.MOVING)
        {
            diceRoll = Random.Range(1, 7);
            playerOneUnit.newPos = playerOneUnit.oldPos + diceRoll;

            diceText.text = "Rolled : " + diceRoll.ToString();
            if (playerOneUnit.newPos >= tilesPrefab.tiles.Count)
            {
                playerOneUnit.newPos -= playerOneUnit.oldPos;
                lap++;
                PlayerInstance.instance.GetComponent<Unit>().unitScore += 3;

            }

            StartCoroutine(
                LerpPosition
                (
                    playerOne.transform,
                    tilesPrefab.tiles[playerOneUnit.oldPos].position,
                    tilesPrefab.tiles[playerOneUnit.newPos].position,
                    2f
                )
            );

            state = RoundState.WAITING;                
        }    
        yield return null;
        diceButton.GetComponent<Button>().interactable = true;

    }
        //playerOne.transform.position = Vector3.Lerp(tilesPrefab.tiles[playerOneUnit.oldPos].position, tilesPrefab.tiles[playerOneUnit.newPos].position, 2f * Time.deltaTime);
    public void LoadMinigame()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        GameObject spawn = GameObject.Find("Spawn");     
    }
    public void MoveBack(int amount)
    {
        StartCoroutine(
                 LerpPosition
                 (
                     playerOne.transform,
                     tilesPrefab.tiles[playerOneUnit.oldPos].position,
                     tilesPrefab.tiles[playerOneUnit.oldPos - amount].position,
                     2f
                 )
             );
        state = RoundState.WAITING;
    }
    public void MoveForward(int amount)
    {
        StartCoroutine(
                 LerpPosition
                 (
                     playerOne.transform,
                     tilesPrefab.tiles[playerOneUnit.oldPos].position,
                     tilesPrefab.tiles[playerOneUnit.oldPos + amount].position,
                     2f
                 )
             );
        state = RoundState.NEXTROUND;
    }
    IEnumerator LerpPosition(Transform player, Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            player.position = Vector3.Lerp(startPosition, targetPosition, time / duration);   
            time += Time.deltaTime;
            yield return null;
        }

        player.position = targetPosition;
        playerOneUnit.oldPos = playerOneUnit.newPos;
        for (int i = 0; i < negativeTiles.Count; i++)
        {
            if (targetPosition == tilesPrefab.tiles[negativeTiles[i]].transform.position)
            {
                int number = negativeTiles[i] - 1;
                playerOneUnit.oldPos = number;
                player.position = Vector3.Lerp(targetPosition, tilesPrefab.tiles[negativeTiles[i] - 1].transform.position, 1f);
            }
        }
        for (int i = 0; i < trophyTiles.Count; i++)
        {
            if (targetPosition == tilesPrefab.tiles[trophyTiles[i]].transform.position)
            {
                PlayerInstance.instance.GetComponent<Unit>().unitScore += 1; 
            }
        }
      
        yield return new WaitForSeconds(0.5f);

        //LoadMinigame();
    }
    public void EndTurn() {
        PlayerTurn();
    }
    public void RollDice()
    {
        PlayerInstance.instance.GetComponent<Unit>().isReady = false;
        int counter = 0;
        List<bool> readyCheck = new List<bool>();
        readyCheck.Clear();
        for (int i = 0; i < NetworkCallback.GetPlayers().Count; i++)
        {
            state = RoundState.MOVING;
            diceButton.GetComponent<Button>().interactable = false;
           
            StartCoroutine(MovePlayer());
        }
    }
    public void CalculatePoints()
    {
        List<int> allPoints = new List<int>() ;
        int highestPoints;
        for (int i = 0; i < NetworkCallback.GetPlayers().Count; i++)
        {
            allPoints.Add(NetworkCallback.GetPlayers()[i].GetComponent<NetworkPlayer>().GetPoints());
            // if (player.playerState != null)
            //{

            highestPoints = Mathf.Max(allPoints.ToArray());
            Debug.Log(highestPoints);
            /*
            if (player.playerState.PlayerScore >= highestScore)
                {
                    highestScore = player.playerState.PlayerScore;
                    highestScoringPlayer = player.networkPlayer;
                    highestScoringPlayerState = player.playerState;
                }
                */
            //}
        }
    }
    void WaitTurn()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        for (int i = 0; i < NetworkCallback.GetPlayers().Count; i++)
        {
            if (NetworkCallback.GetPlayers().Where(c => c.GetComponent<NetworkPlayer>().isReady).Count() == 0) {
                for (int j = 0; j < NetworkCallback.GetPlayers().Count; j++)
                {        
                    diceButton.GetComponent<Button>().interactable = true;                
                }
            }
            Debug.Log(NetworkCallback.GetPlayers().Where(c => c.GetComponent<NetworkPlayer>().isReady).Count());
        }
        */
            trophiesText.text = PlayerInstance.instance.GetComponent<Unit>().unitScore.ToString();
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < negativeTiles.Count - 1; i++)
        {
            Gizmos.DrawLine(tilesPrefab.tiles[negativeTiles[i]].transform.position, tilesPrefab.tiles[negativeTiles[i]].transform.position + new Vector3(0, 5, 0));
        }
        for (int i = 0; i < trophyTiles.Count - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(tilesPrefab.tiles[trophyTiles[i]].transform.position, tilesPrefab.tiles[trophyTiles[i]].transform.position + new Vector3(0, 5, 0));
        }
    }
}
