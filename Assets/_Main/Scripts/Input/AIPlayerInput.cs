using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerInput : UserInput
{

	// IEnumerator Start(){
	// 	while (true)
	// 	{
    //         yield return new WaitForSeconds(1f);
    //         Attack();
    //         yield return new WaitForSeconds(0.3f);
    //         UnAttack();
	// 	}
	// }

    public void Move(Vector3 direction, float distance, bool run = false)
    {
        Dmag = distance;
        Dforward = direction;
        this.run = run;
    }

    public void Attack()
    {
        rb = true;
    }

    public void UnAttack(){
        rb = false;
    }

    public void Roll(Vector3 direction)
    {

    }

    public void Defense()
    {
		lb = true;
    }

    public void Reset(){
        Dmag = 0;
        Dforward = Vector3.zero;
        run = false;
        rb =  false;
        lb = false;
    }

}
