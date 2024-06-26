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
    [SerializeField] private Room room;
    [SerializeField] private GameObject enemy;
    private Vector3 startPos;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer spriteRendererInChildren;
    private Transform spriteTransform;
    private Vector3 previousPos;
    private Vector3 currentPos;
    private Vector3 movement;
    private bool isFlipped = false;
    private void Awake()
    {
        startPos = transform.localPosition;
        room = GetComponentInParent<Room>();
        room.informAgentSetEnemy += SetEnemy;
        room.informAgentLeaveEnemy += LeaveEnemy;
        previousPos = startPos;
        spriteRendererInChildren = GetComponentInChildren<SpriteRenderer>();
        spriteTransform = spriteRendererInChildren.transform;

    }
    private void Update()
    {
        
        FlipChildSpriteOnMovement();

    }
    //ai using transform for movement
    private void FlipChildSpriteOnMovement()
    {
        currentPos = transform.localPosition;
        movement = currentPos - previousPos;
        if (movement.x > 0 && isFlipped)
        {
            FlipChildSprite(false);
        }
        else if (movement.x < 0 && !isFlipped)
        {
            FlipChildSprite(true);
        }
    }
    //also pivot point of assets is not in the center
    //so I need to adjust the position  of sprite renderer
    private void FlipChildSprite(bool flip)
    {
        spriteRendererInChildren.flipX = flip;
        isFlipped = flip;

        Vector3 newChildPos = spriteTransform.localPosition;
        newChildPos.x *= -1;
        spriteTransform.localPosition = newChildPos;
    }
    private void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
    }
    private void LeaveEnemy()
    {
        enemy = null;
        //SetReward(-20f);
    }
    public void SetRoom(GameObject room)
    {
        this.room = room.GetComponent<Room>();
        this.room.informAgentSetEnemy += SetEnemy;
        this.room.informAgentLeaveEnemy += LeaveEnemy;
        transform.parent = room.transform;
        if(this.room.enemyList.Count > 0)
        {
            enemy = this.room.enemyList[0];
        }
    }
    public void LeaveRoom()
    {
        room.informAgentSetEnemy -= SetEnemy;
        room.informAgentLeaveEnemy -= LeaveEnemy;
        room = null;
        transform.parent = null;
        LeaveEnemy();
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
        //for if agent and enemy spawn in same pos in training
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
