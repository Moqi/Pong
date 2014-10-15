using UnityEngine;
using System.Collections;

public class RightEdge : MonoBehaviour 
{
	public Main parent;
	
	private void OnCollisionEnter(Collision col)
	{
		parent.UpdateRightScore();
	}
}