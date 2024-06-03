using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openSide;

    public RoomTemplates roomTemplates;
    private int rand;
    private bool spawned = false;

    private GameObject grid;

    void Start()
    {
        if(transform.position.x == 0 && transform.position.y == 0)
        {
            Destroy(gameObject);
        }
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        grid = GameObject.FindGameObjectWithTag("Grid");
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openSide == 1)
            {
                //Bottom
                rand = Random.Range(0, roomTemplates.bottomRooms.Length);
                Instantiate(roomTemplates.bottomRooms[rand], transform.position, roomTemplates.bottomRooms[rand].transform.rotation).transform.parent = grid.transform;
            }
            else if (openSide == 2)
            {
                //Top
                rand = Random.Range(0, roomTemplates.topRooms.Length);
                Instantiate(roomTemplates.topRooms[rand], transform.position, roomTemplates.topRooms[rand].transform.rotation).transform.parent = grid.transform;
            }
            else if (openSide == 3)
            {
                //Left
                rand = Random.Range(0, roomTemplates.leftRooms.Length);
                Instantiate(roomTemplates.leftRooms[rand], transform.position, roomTemplates.leftRooms[rand].transform.rotation).transform.parent = grid.transform;
            }
            else if (openSide == 4)
            {
                //Right
                rand = Random.Range(0, roomTemplates.rightRooms.Length);
                Instantiate(roomTemplates.rightRooms[rand], transform.position, roomTemplates.rightRooms[rand].transform.rotation).transform.parent = grid.transform;
            }

            spawned = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint") && !collision.CompareTag("Destruction"))
        {
            if(collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity).transform.parent = grid.transform;
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

    public void Init()
    {

    }

}
