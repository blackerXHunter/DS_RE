﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerInput : UserInput {

	// Use this for initialization
	IEnumerator Start () {
        while (true) {
            rb = true;
            yield return 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
