using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public IActorController ac;
    private IActorManagerHandler handler;
    [Header("=== Auto Gen If Null ===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;
    // Use this for initialization
    private void Awake()
    {
        ac = GetComponent<IActorController>();
        if (ac == null)
        {
            Debug.LogError("The Actor Manager " + this.name + "'s ActorContoller is Null!");
        }
        handler = GetComponent<IActorManagerHandler>();
        if (handler == null)
        {
            Debug.LogError("The Actor Manager " + this.name + "'s Handler is Null!");
        }
        else
        {
            handler.am = this;
        }
        ac.OnAction += handler.DoAction;
        //ac.OnAction += DoAction;
    }
    public void SendCommand(string command,params object[] objs)
    {
        handler.Handle(command, objs);
    }
    public void SendCommand(string command)
    {
        handler.Handle(command);
    }


    public T Bind<T>(GameObject obj) where T : IActorManager
    {
        if (obj == null)
        {
            return null;
        }
        T iacM = obj.GetComponent<T>();
        if (iacM == null)
        {
            iacM = obj.AddComponent<T>();
        }
        iacM.am = this;
        return iacM;
    }

}
