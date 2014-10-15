using UnityEngine;
using System.Collections;

public class LeftEdge : MonoBehaviour 
{
	public Main parent;
	
	private void OnCollisionEnter(Collision col)
	{
		parent.UpdateLeftScore();
	}
}