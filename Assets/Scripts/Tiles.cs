using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] List<Transform> tiles;
    private GameObject player;
    private Transform currentPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;

        player.transform.position = tiles[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < 2; i++)
            {
                player.transform.position = Vector3.Lerp(tiles[i].transform.position, tiles[i + 1].transform.position, 5f);
            }
        }
    }
}
