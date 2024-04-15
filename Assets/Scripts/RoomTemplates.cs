using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public GameObject boss;
    public GameObject enemies;

    private void Start()
    {
        Invoke("SpawnEnemies", 3.0f);
    }

    void SpawnEnemies()
    {
        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);

        for(int i = 0; i < rooms.Count-1; i++)
        {
            Instantiate(enemies, rooms[i].transform.position, Quaternion.identity); 
        }
    }
}
