using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.Mathematics;
using Unity.MLAgents.Sensors;

public class DefenderAgent : Agent
{
    [SerializeField] private float speed;
    public Room room;
    //we can take enemy info from room
    //for training i will choose myself
    [SerializeField] private GameObject enemy;
    private Vector3 startPos;
    //[SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    
    private void Start()
    {
        startPos = transform.localPosition;
        room = GetComponentInParent<Room>();
        room.informAgentSetEnemy += SetEnemy;
        room.informAgentLeaveEnemy += LeaveEnemy;
    }
    private void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
    }
    private void LeaveEnemy()
    {
        enemy = null;
    }
    private void SetRoom()
    {

    }
    public override void OnEpisodeBegin()
    {
        //for the game itself
        transform.localPosition = startPos;

        //for the training
        //enemy.transform.localPosition = new Vector2(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-3.5f, 3.5f));
        //transform.localPosition = new Vector2(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-3.5f, 3.5f));

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        if(enemy != null)
        {
            sensor.AddObservation(enemy.transform.localPosition);
        }
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        transform.localPosition += new Vector3 (moveX, moveY) * speed * Time.deltaTime;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            AddReward(10f);
            //for visualize the result
            //backgroundSpriteRenderer.color = Color.gray;
            EndEpisode();
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //for if agent and enemy spawn in same pos
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AddReward(10f);
            //backgroundSpriteRenderer.color = Color.gray;
            EndEpisode();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AgentWall"))
        {
            AddReward(-2f);
            //backgroundSpriteRenderer.color = Color.blue;
            EndEpisode();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("AgentWall"))
        {
            //backgroundSpriteRenderer.color = Color.blue;
            AddReward(-0.1f);
        }
    }
}
