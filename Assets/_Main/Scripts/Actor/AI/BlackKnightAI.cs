using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightAI : MonoBehaviour
{

    private ActorManager am;

    void Start()
    {
        if (am == null)
        {
            am = GetComponent<ActorManager>();
        }
    }
    #region Patrol
    public Transform[] patrolPoints;

    public float waitTime;
    [ContextMenu("Start Parol Task")]
    public void StartParolTask()
    {
        if (patrolPoints == null || patrolPoints.Length <= 0)
        {
            Debug.Log("patrol points is not set");
            return;
        }
        StartCoroutine(PatrolTask());
    }
    public void StopParolTask()
    {
        StopCoroutine(PatrolTask());
    }
    public IEnumerator PatrolTask()
    {
        while (true)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                yield return MoveToThePoint(patrolPoints[i]);
                yield return new WaitForSeconds(waitTime);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator MoveToThePoint(Transform transform)
    {
        
        Vector3 dir = transform.position - this.transform.position;
        dir.Normalize();
        this.transform.LookAt(dir);
        Move(dir, 0.7f);
        while (Vector3.Distance(this.transform.position, transform.position) > 0.03f)
        {
            yield return new WaitForEndOfFrame();
        }
        Reset();
    }

    
    public void Move(Vector3 direction, float speed = 0.7f, bool run = false)
    {
        var ac = am.ac as EnemyAC;
        ac.playerInput.Dmag = speed;
        ac.playerInput.Dforward = direction;
        ac.playerInput.run = run;
    }

    public void Attack()
    {

    }

    public void UnAttack(){
    }

    public void Roll(Vector3 direction)
    {

    }

    public void Defense()
    {

    }

    public void Reset(){

    }
    #endregion
}
