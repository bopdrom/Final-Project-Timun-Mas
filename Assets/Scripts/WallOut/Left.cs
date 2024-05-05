using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
	public static bool wallLeft;
	private void OnTriggerEnter2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallLeft = true;
		}
	}
	private void OnTriggerExit2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallLeft = false;
		}
	}
}
