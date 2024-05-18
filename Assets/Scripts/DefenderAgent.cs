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
    [SerializeField] private GameObject room;
    //we can take enemy info from room
    //for training i will choose myself
    [SerializeField] private GameObject enemy;
    private Vector3 startPos;
    
    private void Start()
    {
        startPos = transform.localPosition;
    }
    public override void OnEpisodeBegin()
    {
        //for the game itself
        //transform.localPosition = startPos;

        //for the training
        transform.localPosition = new Vector2 (UnityEngine.Random.Range(-4f,4f), UnityEngine.Random.Range(-3.5f, 3.5f));
        enemy.transform.localPosition = new Vector2(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-3.5f, 3.5f));

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(enemy.transform.localPosition);
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
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("AgentWall"))
        {
            AddReward(-2f);
            EndEpisode();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //for if agent and enemy spawn in same pos
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AddReward(10f);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("AgentWall"))
        {
            AddReward(-0.1f);
        }
    }
}
