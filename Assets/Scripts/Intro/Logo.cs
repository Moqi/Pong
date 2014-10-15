using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

	// Use this for initialization
	private void Start () 
	{
		StartCoroutine(YourFunction());
	}	
		
	private IEnumerator  YourFunction() 
	{
		yield return new WaitForSeconds(1);
		
		audio.Play();
		
		yield return new WaitForSeconds(2);
		
		Application.LoadLevel("Intro");
	}	
	
	private void OnMouseDown()
	{
		Application.OpenURL("http://www.opreagames.com");
	}
}
