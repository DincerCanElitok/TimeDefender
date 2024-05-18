using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    
    private void Update()
    {
        //testing
        transform.position +=  new Vector3(0, Time.deltaTime,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            transform.parent = collision.gameObject.transform;
            var room = collision.GetComponent<Room>();
            room.enemyList.Add(gameObject);
            room.InformAgentEnemyInRoom();
        }
        //can be pooled
        if (collision.CompareTag("Death"))
            Destroy(gameObject);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            var room = collision.GetComponent<Room>();
            room.enemyList.Remove(gameObject);
            room.InformAgentEnemyLeaveRoom();
            transform.parent = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Agent"))
        {
            TakeDamage(10f);
        }
    }
    private void TakeDamage(float amount)
    {
        health -= amount;
        //for testing, can be pooled
        if (health < 0)
            Destroy(gameObject);
    }
}
