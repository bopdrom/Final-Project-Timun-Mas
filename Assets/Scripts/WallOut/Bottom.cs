using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
	public static bool wallBottom;
	private void OnTriggerEnter2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallBottom = true;
		}
	}
	private void OnTriggerExit2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallBottom = false;
		}
	}
}
