using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{
    public abstract class IGameObjectController : MonoBehaviour
    {
        public string myID, myName;
        public GameObject model;
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void Awake() { }
        protected virtual void Start() { }
    }
}