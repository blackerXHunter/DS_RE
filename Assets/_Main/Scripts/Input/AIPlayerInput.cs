using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerInput : UserInput
{

	IEnumerator Start(){
		while (true)
		{
			Move(transform.forward, 0.7f);			
			yield return new WaitForSeconds(0.1f);
		}
	}

    public void Move(Vector3 direction, float distance, bool run = false)
    {
        Dmag = distance;
        Dforward = direction;
        this.run = run;
    }

    public void Attack()
    {
        lb = true;
    }

    public void Roll(Vector3 direction)
    {

    }

    public void Defense()
    {
		rb = true;
    }

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

    }
}
