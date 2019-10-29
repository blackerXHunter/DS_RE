using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{

    public class Timeline : MonoBehaviour
    {
        public TimeTrack[] timeTracks;

        public void Play()
        {
            foreach (var tt in timeTracks)
            {
                tt.Play();
            }
        }
    }

}