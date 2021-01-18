using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] List<Transform> tiles;
    private GameObject player;
    private Transform currentPlayerPos;

    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;

        player.transform.position = tiles[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        player.transform.position = Vector3.MoveTowards(player.transform.position, tiles[2].position, step);

        if (Input.GetKeyDown(KeyCode.P))
        {
            Random.Range(-10.0f, 10.0f);
            for (int i = 0; i < 2; i++)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, tiles[i].position, step);

                //player.transform.position = Vector3.Lerp(tiles[i].transform.position, tiles[i + 1].transform.position, 10f);
            }
        }
    }

}
