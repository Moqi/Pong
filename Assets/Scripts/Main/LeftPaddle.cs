using UnityEngine;
using System.Collections;

public class LeftPaddle : MonoBehaviour 
{
	public AudioSource hitPaddle;
	
	private void OnCollisionEnter(Collision col)
	{
		if(col.collider.name == "Ball")
			hitPaddle.Play();
	}
}
