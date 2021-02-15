using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum RoundState { START, WAITING, MOVING, WON, LOST, PLAYERONE, DICE, MINIGAME, ROLLING}
public class RoundSystem : MonoBehaviour
{
    private static bool GameManagerExists;

    public RoundState state;
    public int turns = 0;
    public int maxTurns = 3;

    Unit playerOneUnit;
    Unit playerTwoUnit;

    public Tiles tilesPrefab;
    public HUD playerHUD;

    public Transform spawnRoom;
    public GameObject playerOnePrefab;

    public Camera mainCamera;
    GameObject playerOne;

    float walkingSpeed=5f;

    int diceRoll;
    public TMP_Text diceText;
    // Start is called before the first frame update
    void Awake()
    {
        if (!GameManagerExists) //if GameManagerexcistst is not true --> this action will happen.
        {
            GameManagerExists = true;
            state = RoundState.WAITING;
            StartCoroutine(SetupRound());
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            state = RoundState.WAITING;
            Destroy(gameObject);
        }
       
    }
    IEnumerator SetupRound()
    {
        playerOne = Instantiate(playerOnePrefab, spawnRoom);
        playerOneUnit = playerOne.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);
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
    void MovePlayer()
    {
        if(state == RoundState.MOVING)
        { 
            diceRoll = Random.Range(1, 7);

            playerOneUnit.newPos = playerOneUnit.oldPos + diceRoll;
           
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

        //playerOne.transform.position = Vector3.Lerp(tilesPrefab.tiles[playerOneUnit.oldPos].position, tilesPrefab.tiles[playerOneUnit.newPos].position, 2f * Time.deltaTime);


    }
    public void LoadMinigame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        GameObject spawn = GameObject.Find("Spawn");

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
        state = RoundState.WAITING;

        LoadMinigame();
    }
    public void EndTurn() {
        PlayerTurn();
    }
    public void RollDice()
    {
        state = RoundState.MOVING;
        
    }
    void WaitTurn()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(state == RoundState.MOVING)
        {
            MovePlayer();
        }
    }
}
