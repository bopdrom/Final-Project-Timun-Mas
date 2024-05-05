using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
	public static bool wallRight;
	private void OnTriggerEnter2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallRight = true;
		}
	}
	private void OnTriggerExit2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallRight = false;
		}
	}
}
