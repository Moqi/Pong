using UnityEngine;
using System.Collections;

public class LowerWall : MonoBehaviour 
{
	public Main parent;
	public AudioSource hitWall;
	
	private void OnCollisionEnter(Collision col)
	{
		if(col.collider.name == "RightPaddle")
			parent.LowerWallCollision(true);
			
		if(col.collider.name == "Ball")
			hitWall.Play();
	}
	
	private void OnCollisionExit(Collision col)
	{
		if(col.collider.name == "RightPaddle")
			parent.LowerWallCollision(false);
	}	
}
