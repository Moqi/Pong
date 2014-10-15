using UnityEngine;
using System.Collections;

public class UpperWall : MonoBehaviour 
{
	public Main parent;
	public AudioSource hitWall;
	
	private void OnCollisionEnter(Collision col)
	{
		if(col.collider.name == "RightPaddle")
			parent.UpperWallCollision(true);
			
		if(col.collider.name == "Ball")
			hitWall.Play();
	}
	
	private void OnCollisionExit(Collision col)
	{
		if(col.collider.name == "RightPaddle")
			parent.UpperWallCollision(false);
	}	
}
