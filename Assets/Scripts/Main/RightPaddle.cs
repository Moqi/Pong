using UnityEngine;
using System.Collections;

public class RightPaddle : MonoBehaviour 
{
	public AudioSource hitPaddle;
	
	private void OnCollisionEnter(Collision col)
	{
		if(col.collider.name == "Ball")
			hitPaddle.Play();
	}
}
