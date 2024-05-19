using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    public Slider healthBarSlider;
    public float takeDamageAmount;
    public float speed;
    public GameController controller;
    private void Start()
    {
        healthBarSlider.value = health;
        healthBarSlider.maxValue = health;

    }

    private void Update()
    {
        //testing
        transform.position +=  new Vector3(0, speed * Time.deltaTime,0);
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
        {
            controller.currentTime += 10f;
            Destroy(gameObject);
        }
            
        if (collision.CompareTag("Return"))
            speed = -speed;

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
            TakeDamage(takeDamageAmount);            
        }
    }
    private void TakeDamage(float amount)
    {
        health -= amount;
        healthBarSlider.value = health;
        //for testing, can be pooled
        if (health <= 0)
            Destroy(gameObject);
    }
    
}
