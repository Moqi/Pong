using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	//public GameObject name;
	public GameObject ball;

	private void OnMouseDown()
	{
		// mouse left button pressed
		//if(Input.GetMouseButtonDown(0))
		//{
			if (this.gameObject.name == "1Player")
				ApplicationModel.noPlayers = 1;
			else if (this.gameObject.name == "2Players")
				ApplicationModel.noPlayers = 2;
			
			
			Debug.Log ("saved: " + ApplicationModel.noPlayers);
			Application.LoadLevel("Main");
		//}
	}	
	
	private void OnMouseEnter()
	{
		Debug.Log ("mouse over");
	
		Vector3 position = ball.transform.position;
		
		if (this.gameObject.name == "1Player")
		{
			position.y = 1.113309f;
		}
		else
		{
			position.y = -4.44136f;
		}
		
		ball.transform.position = position;
	}
	
	private void OnMouseExit()
	{
		Vector3 position = ball.transform.position;
		position.y = -1.40195f;
		ball.transform.position = position;
	}
}
