using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundState { START, WAITING, MOVING, WON, LOST, PLAYERONE, DICE }
public class RoundSystem : MonoBehaviour
{
    public RoundState state;
    public int turns = 0;
    public int maxTurns = 3;

    Unit playerOneUnit;
    Unit playerTwoUnit;

    public Tiles tilesPrefab;
    public HUD playerHUD;

    public Transform spawnRoom;
    public GameObject playerOnePrefab;

    GameObject playerOne;

    float walkingSpeed=5f;
    // Start is called before the first frame update
    void Start()
    {
        state = RoundState.WAITING;
        StartCoroutine(SetupRound());
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
    void MovePlayer()
    {
        Random e = new Random();

        int diceRoll = Random.Range(1, 7);
        float step = walkingSpeed * Time.deltaTime;

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

        //playerOne.transform.position = Vector3.Lerp(tilesPrefab.tiles[playerOneUnit.oldPos].position, tilesPrefab.tiles[playerOneUnit.newPos].position, 2f * Time.deltaTime);

        playerOneUnit.oldPos = playerOneUnit.newPos;
        Debug.Log(playerOneUnit.newPos);

        if (Input.GetKeyDown(KeyCode.P))
        {
            Random.Range(-10.0f, 10.0f);

        }
        state = RoundState.WAITING;
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
    }
    public void EndTurn() {
        PlayerTurn();
    }
    public void RollDice()
    {
        /*if (state != RoundState.DICE)
            return;
            */
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
