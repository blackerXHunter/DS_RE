

using UnityEngine;

public abstract class IActorManagerHandler : MonoBehaviour
{
    public ActorManager am;
    public abstract void Handle(string command,object[] objs);
    public virtual void Handle(string command){
        Handle(command, null);
    }

    public abstract void DoAction();
}
