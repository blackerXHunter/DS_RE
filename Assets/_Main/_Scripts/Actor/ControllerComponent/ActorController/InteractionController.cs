using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DS_RE
{
    public class InteractionController : MonoBehaviour
    {
        public CapsuleCollider interaCol;
        public List<EventCasterController> ecastControllerList = new List<EventCasterController>();
        // Use this for initialization
        void Start()
        {
            interaCol = GetComponent<CapsuleCollider>();
        }
        public UnityAction OnECMEnter, OnECMExit, OnECMStay;


        private void OnTriggerEnter(Collider other)
        {
            EventCasterController[] ecms = other.GetComponents<EventCasterController>();
            foreach (var ecm in ecms)
            {
                if (!ecastControllerList.Contains(ecm))
                {
                    ecastControllerList.Add(ecm);
                    OnECMEnter?.Invoke();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            EventCasterController[] ecms = other.GetComponents<EventCasterController>();
            if (ecms != null && ecms.Length > 0)
            {
                OnECMStay?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            EventCasterController[] ecms = other.GetComponents<EventCasterController>();
            foreach (var ecm in ecms)
            {
                if (ecastControllerList.Contains(ecm))
                {
                    ecastControllerList.Remove(ecm);
                    OnECMExit?.Invoke();
                }
            }
        }
    }
}
