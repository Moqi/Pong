using UnityEngine;
using System.Collections;

public class RightEdge : MonoBehaviour 
{
	public Main parent;
	public AudioSource lose;
	
	private void OnCollisionEnter(Collision col)
	{
		parent.UpdateLeftScore();
		
		lose.Play();
	}
}