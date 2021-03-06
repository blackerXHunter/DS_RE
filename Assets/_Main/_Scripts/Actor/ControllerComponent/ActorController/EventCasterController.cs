﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DS_RE
{

    public class EventCasterController : MonoBehaviour
    {
        public IActorController ac;
        public enum EventCasterType
        {
            item,
            box,
            lever,
            attack
        }
        public EventCasterType ect = EventCasterType.item;
        public string eventName;
        [SerializeField]
        private bool _active;
        public bool active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (!_active)
                {
                    OnUnActive?.Invoke();
                }
            }
        }
        [SerializeField]
        public UnityEvent OnUnActive;
        public Vector3 offset = new Vector3(0, 0, 1);

        // Use this for initialization
        void Start()
        {
            if (ac == null)
            {
                ac = gameObject.GetComponentInParent<IActorController>();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
