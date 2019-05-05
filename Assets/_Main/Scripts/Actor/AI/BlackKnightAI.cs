using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        partrolTaskCTS?.Cancel();
        partrolTaskCTS = new CancellationTokenSource();
        var ct = partrolTaskCTS.Token;
        using (var partrolTask = PatrolTask(true, ct))
        {
            await partrolTask;
        }
    }
    CancellationTokenSource partrolTaskCTS;
    [ContextMenu("Stop Parol Task")]
    public void StopParolTask()
    {
        if (partrolTaskCTS != null)
        {
            partrolTaskCTS.Cancel();
        }
    }
    private async Task PatrolTask(bool loop, CancellationToken cancellationToken)
    {

        do
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    loop = false;
                    return;
                }
                await MoveToThePointAsync(patrolPoints[i], cancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(waitTime));
            }
        } while (loop);

    }
    #endregion

    #region FindPlayerAndFollow
    public Transform playerTansform;
    public CancellationTokenSource findCTS;
    [ContextMenu("Start Find")]
    public async void StartFind()
    {
        findCTS?.Cancel();
        findCTS = new CancellationTokenSource();
        var ct = findCTS.Token;
        using (var task = FindTaskAsync(ct))
        {
            await task;
        }
    }

    private async Task FindTaskAsync(CancellationToken ct)
    {
        GameObject player = null;
        while (player == null)
        {
            if (ct.IsCancellationRequested)
            {
                return;
            }
            player = CheckFoundPlayer();
            await Task.Delay((int)(Time.deltaTime * 1000));
        }
        partrolTaskCTS?.Cancel();
        playerTansform = player.transform;
        await MoveToThePointAsync(playerTansform, ct);
    }
    [ContextMenu("Stop Find")]
    public void StopFind()
    {
        findCTS.Cancel();
    }
    #endregion

    #region Attack Auto
    public CancellationTokenSource autoAttackCTS;
    [ContextMenu("Start Attak")]
    public async void StartAutoAttack()
    {
        findCTS?.Cancel();
        findCTS = new CancellationTokenSource();
        await AutoAttackTask(findCTS.Token);
    }

    public async Task AutoAttackTask(CancellationToken ct)
    {
        while (true)
        {
            if (ct.IsCancellationRequested)
            {
                UnAttack();
                return;
            }
            var canAttack = CheckCanAttackPlayer();
            if (canAttack)
            {
                Attack();
            }
            else
            {
                UnAttack();
            }
            await Task.Delay((int)(1000 * Time.deltaTime));
        }
    }

    [ContextMenu("Stop Attack")]
    public void StopAutoAttack()
    {
        UnAttack();
        autoAttackCTS.Cancel();
    }
    #endregion

    private async Task MoveToThePointAsync(Transform transform, CancellationToken cancellationToken)
    {

        while (!IsTouched(this.transform, transform))
        {

            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            Vector3 dir = transform.position - this.transform.position;
            dir.Normalize();
            this.transform.LookAt(dir);
            Move(dir, speed, run);
            await Task.Delay((int)(Time.deltaTime * 1000));
            if (this == null)
            {
                return;
            }
        }
        Debug.Log("OK");
        Reset();
    }

    private GameObject CheckFoundPlayer()
    {
        var cols = Physics.OverlapBox(this.transform.position, new Vector3(0.5f, 0.5f, 5), am.ac.model.transform.rotation, LayerMask.GetMask("Player"));
        if (cols.Length > 0)
        {
            return cols[0].gameObject;
        }
        else return null;
    }

    private bool CheckCanAttackPlayer()
    {
        var cols = Physics.OverlapBox(this.transform.position, new Vector3(0.5f, 0.5f, 1), am.ac.model.transform.rotation, LayerMask.GetMask("Player"));
        if (cols.Length > 0)
        {
            return true;
        }
        else return false;
    }

    public bool IsTouched(Transform personTrans, Transform targetTrans)
    {
        var personPos = personTrans.position;
        var targetPos = targetTrans.position;
        Vector2 pplanePos = new Vector2(personPos.x, personPos.z);
        Vector2 tplanePos = new Vector2(targetPos.x, targetPos.z);
        //        Debug.Log( Vector2.Distance(pplanePos, tplanePos));
        if (Vector2.Distance(pplanePos, tplanePos) < 0.5f)
        {
            return true;
        }
        else
        {
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


    void OnDestroy()
    {
        partrolTaskCTS?.Cancel();
        findCTS?.Cancel();
    }
}
