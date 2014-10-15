using UnityEngine;
using System.Collections;

public class LeftEdge : MonoBehaviour 
{
	public Main parent;
	public AudioSource lose;
	
	private void OnCollisionEnter(Collision col)
	{
		parent.UpdateRightScore();
		
		lose.Play();
	}
}