

using UnityEngine;

public abstract class IActorManagerHandler : MonoBehaviour {
    public ActorManager am;
    public abstract void Handle(string command, object[] objs);

    public abstract void DoAction();
}
