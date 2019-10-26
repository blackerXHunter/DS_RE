using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{
    public abstract class IActorController : MonoBehaviour
    {
        public string myID, myName;
        public GameObject model;
        [Header("===== Controller ======")]
        public EventCasterController eventCaster;
        public InteractionController interactionController;
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void DoAction() { }
        public virtual void SendCommand(string command, params object[] objs)
        {

        }
    }
}