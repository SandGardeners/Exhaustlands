using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
    public class ReversePlayback : MonoBehaviour {
        public PlayableDirector director;

     
        // Use this for initialization
        void Start() {
            director = GetComponent<PlayableDirector>();
            director.Stop();
            director.time = director.playableAsset.duration - 0.01;
            director.Evaluate();
        }
     
        // Update is called once per frame
        void Update() {
            double t = director.time - Time.deltaTime;
            if (t < 0)
                t = 0;
     
            director.time = t;
            director.Evaluate();
     
            if (t == 0) {
                
                director.Stop();
                Destroy(this);
            }
        }
    }
