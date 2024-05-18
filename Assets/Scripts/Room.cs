using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public Action<GameObject> informAgentSetEnemy;
    public Action informAgentLeaveEnemy;

    public void InformAgentEnemyInRoom()
    {
        if (enemyList.Count > 0)
        {
            informAgentSetEnemy.Invoke(enemyList[0]);
        }
    }
    public void InformAgentEnemyLeaveRoom()
    {
        informAgentLeaveEnemy.Invoke();
        //for multiple enemies in same room
        if (enemyList.Count > 0)
        {
            informAgentSetEnemy.Invoke(enemyList[0]);
        }
    }
}
