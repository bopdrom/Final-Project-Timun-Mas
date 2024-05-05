using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
	public static bool wallTop;
	private void OnTriggerEnter2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallTop = true;
		}
	}
	private void OnTriggerExit2D(Collider2D Col)
	{
		if (Col.tag == "Player")
		{
			wallTop = false;
		}
	}
}
