using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS_RE
{

    public class AnimationTimeTrack : TimeTrack
    {
        [ContextMenu("play")]
        public override void Play()
        {
            base.Play();
            animatedObj.animator.Play(animationName);
        }

        public AnimatedObjectController animatedObj;
        public string animationName;
    }

}