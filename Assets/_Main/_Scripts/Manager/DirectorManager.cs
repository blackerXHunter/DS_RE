using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DS_RE
{
    public class DirectorManager : Singleton<DirectorManager>
    {
        void Start()
        {

        }

        private void PlayFrontStab(AnimatedObjectController attacker, AnimatedObjectController victim)
        {

        }

        public bool IsPlaying()
        {
            return false;
        }

        public void Play(string eventName, AnimatedObjectController attacker, AnimatedObjectController victim)
        {

            if (eventName == "frontStab")
            {
            }

            else if (eventName == "treasureBox")
            {

            }

            else if (eventName == "leverUp")
            {
            
            }
            else if (eventName == "item")
            {
                return;
            }
        }
    }

}
