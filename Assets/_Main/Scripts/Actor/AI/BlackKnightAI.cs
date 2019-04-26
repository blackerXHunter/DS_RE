using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlackKnightAI : MonoBehaviour
{

    private ActorManager am;
    public float speed = 1.0f;
    public bool run = true;
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
    public async void StartParolTask()
    {
        if (patrolPoints == null || patrolPoints.Length <= 0)
        {
            Debug.Log("patrol points is not set");
            return;
        }
        await PatrolTask();
    }
    public void StopParolTask()
    {
        
    }
    public async Task PatrolTask()
    {

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            await MoveToThePointAsync(patrolPoints[i]);
            Reset();
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
        }
    }

    private async Task MoveToThePointAsync(Transform transform)
    {
        while (!IsTouched(this.transform, transform) )
        {
            Vector3 dir = transform.position - this.transform.position;
            dir.Normalize();
            this.transform.LookAt(dir);
            Move(dir, speed, run);
            await Task.Delay( (int)(Time.deltaTime * 1000));
        }
        Debug.Log("OK");
        Reset();
    }

    public bool IsTouched(Transform personTrans, Transform targetTrans){
        var personPos = personTrans.position;
        var targetPos = targetTrans.position;
        Vector2 pplanePos = new Vector2(personPos.x, personPos.z);
        Vector2 tplanePos = new Vector2(targetPos.x, targetPos.z);
//        Debug.Log( Vector2.Distance(pplanePos, tplanePos));
        if (Vector2.Distance(pplanePos, tplanePos) < 0.5f)
        {
            return true;
        }
        else{
            return false;
        }
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
        var ac = am.ac as EnemyAC;
        ac.playerInput.rb = true;
    }

    public void UnAttack()
    {
        var ac = am.ac as EnemyAC;
        ac.playerInput.rb = false;
    }

    public void Roll(Vector3 direction)
    {

    }

    public void Defense()
    {
        var ac = am.ac as EnemyAC;
        ac.playerInput.lb = true;
    }

    public void Reset()
    {
        var ac = am.ac as EnemyAC;
        ac.playerInput.Dmag = 0;
        ac.playerInput.Dforward = Vector3.zero;
        ac.playerInput.run = false;
        ac.playerInput.rb = false;
        ac.playerInput.lb = false;
    }
    #endregion
}
