using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Update()
    {
        //testing
        transform.position +=  new Vector3(0, Time.deltaTime,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            var room = collision.GetComponent<Room>();
            room.enemyList.Add(gameObject);
            room.InformAgentEnemyInRoom();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            var room = collision.GetComponent<Room>();
            room.enemyList.Remove(gameObject);
            room.InformAgentEnemyLeaveRoom();
        }
    }
}
