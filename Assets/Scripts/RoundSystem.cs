using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bolt;

public enum RoundState { START, WAITING, MOVING, WON, LOST, PLAYERONE, DICE, MINIGAME, ROLLING, NEXTROUND }
public class RoundSystem : EntityBehaviour<IManager>
{
    public static RoundSystem Instance { get; set; }

    public RoundState states;
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
    float walkingSpeed = 5f;
    int diceRoll;


    public Button diceButton;
    public TMP_Text diceText;
    public TMP_Text trophiesText;

    public Animator animator;

    private float secondsCount;
    bool gameOver;
    public int nextScene;
    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetAnimator(GetComponent<Animator>());
        state.Animator.applyRootMotion = entity.IsOwner;
    }
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
            states = RoundState.WAITING;
            StartCoroutine(SetupRound());
        }
    }
    IEnumerator SetupRound()
    {
        playerOne = playerOnePrefab;
        playerOneUnit = playerOne.GetComponent<Unit>();
        playerOnePrefab.transform.position = spawnRoom.transform.position;

        yield return new WaitForSeconds(2f);

        StartCoroutine(MovePlayer());
        //playerHUD.SetHUD(playerOneUnit);

        states = RoundState.DICE;
    }
    void PlayerTurn()
    {
        if (states == RoundState.DICE)
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
        if (states == RoundState.MOVING)
        {
            diceRoll = Random.Range(1, 7);
            playerOneUnit.newPos = playerOneUnit.oldPos + diceRoll;

            diceText.text = "Rolled : " + diceRoll.ToString();
            if (playerOneUnit.newPos >= tilesPrefab.tiles.Count)
            {
                playerOneUnit.newPos = 0;
                lap++;
                trophiesText.gameObject.SetActive(true);
                PlayerInstance.instance.isVictory = true;
                trophiesText.text = "VICTORY";
                diceButton.GetComponent<Button>().interactable = false;

            }
            if (animator)
                animator.SetBool("IsMoving", true);


            StartCoroutine(
                LerpPosition
                (
                    playerOne.transform,
                    tilesPrefab.tiles[playerOneUnit.oldPos].position,
                    tilesPrefab.tiles[playerOneUnit.newPos].position,
                    2f
                )
            );

            states = RoundState.WAITING;

            yield return null;
        }
        else
        {
            if (animator)
                animator.SetBool("IsMoving", false);
        }
    }
    //playerOne.transform.position = Vector3.Lerp(tilesPrefab.tiles[playerOneUnit.oldPos].position, tilesPrefab.tiles[playerOneUnit.newPos].position, 2f * Time.deltaTime);
    public void LoadMinigame()
    {
        nextScene = Random.Range(2, 2);
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
    }
    public void MoveBack(int amount)
    {
        playerOneUnit.newPos = playerOneUnit.oldPos - amount;

        StartCoroutine(
                 LerpForward
                 (
                     playerOne.transform,
                     tilesPrefab.tiles[playerOneUnit.oldPos].position,
                     tilesPrefab.tiles[playerOneUnit.newPos].position,
                     2f
                 )
             );
        states = RoundState.WAITING;
    }
    public void MoveForward(int amount)
    {
        playerOneUnit.newPos = playerOneUnit.oldPos + amount;
        if (playerOneUnit.newPos >= tilesPrefab.tiles.Count)
        {
            playerOneUnit.newPos -= playerOneUnit.oldPos;
            lap++;
            PlayerInstance.instance.isVictory = true;

            trophiesText.gameObject.SetActive(true);
            trophiesText.text = "VICTORY";
            diceButton.GetComponent<Button>().interactable = false;

        }
        StartCoroutine(
                 LerpForward
                 (
                     playerOne.transform,
                     tilesPrefab.tiles[playerOneUnit.oldPos].position,
                     tilesPrefab.tiles[playerOneUnit.newPos].position,
                     2f
                 )
             );
        states = RoundState.WAITING;
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
        if (playerOneUnit.newPos >= tilesPrefab.tiles.Count)
        {
            
            trophiesText.gameObject.SetActive(true);
            trophiesText.text = "VICTORY";
            PlayerInstance.instance.isVictory = true;
            diceButton.GetComponent<Button>().interactable = false;
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
                int number = trophyTiles[i] + 1;
                playerOneUnit.oldPos = number;
                player.position = Vector3.Lerp(targetPosition, tilesPrefab.tiles[trophyTiles[i] + 1].transform.position, 1f);
            }
        }

        yield return new WaitForSeconds(0.5f);
        diceButton.GetComponent<Button>().interactable = true;

        //LoadMinigame();
    }
    IEnumerator LerpForward(Transform player, Vector3 startPosition, Vector3 targetPosition, float duration)
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
                int number = trophyTiles[i] + 1;
                playerOneUnit.oldPos = number;
                player.position = Vector3.Lerp(targetPosition, tilesPrefab.tiles[trophyTiles[i] + 1].transform.position, 1f);
            }
        }

        yield return new WaitForSeconds(0.5f);
    }
    public void EndTurn()
    {
        PlayerTurn();
    }
    public void RollDice()
    {
        PlayerInstance.instance.isReady = true;
        states = RoundState.MOVING;
        diceButton.GetComponent<Button>().interactable = false;

        StartCoroutine(MovePlayer());

    }
    public void CalculatePoints()
    {
        List<int> allPoints = new List<int>();
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
        if (NetworkCallback.GetPlayers()[0])
        {
            if(NetworkCallback.GetPlayers()[0].GetComponent<NetworkPlayer>().isVictory)
            {
                diceButton.GetComponent<Button>().interactable = false;
            }
        }
        if (NetworkCallback.GetPlayers()[1])
        {
            if (NetworkCallback.GetPlayers()[1].GetComponent<NetworkPlayer>().isVictory)
            {
                diceButton.GetComponent<Button>().interactable = false;
            }
        }


        /*
        Debug.LogWarning(NetworkCallback.GetPlayers()[0].GetComponent<NetworkPlayer>().transform.position);
        if(NetworkCallback.GetPlayers().Count == 2) 
            Debug.LogWarning(NetworkCallback.GetPlayers()[1].GetComponent<NetworkPlayer>().transform.position);
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
    }
    private void OnDrawGizmos()
    {    /*
        for (int i = 0; i < negativeTiles.Count - 1; i++)
        {
            Gizmos.DrawLine(tilesPrefab.tiles[negativeTiles[i]].transform.position, tilesPrefab.tiles[negativeTiles[i]].transform.position + new Vector3(0, 5, 0));
        }
        for (int i = 0; i < trophyTiles.Count - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(tilesPrefab.tiles[trophyTiles[i]].transform.position, tilesPrefab.tiles[trophyTiles[i]].transform.position + new Vector3(0, 5, 0));
        }
        */
    }
}
