using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool gerak = false, wall = false, tabrakan = false;
    private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
	public LayerMask wallLayer, enemyLayer;
	public int hp = 2;
	public bool invisible;

	void Start(){
        posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
    }

	void Update() {

		if (gerak)
		{
			if (!wall && !tabrakan)
			{
				transform.position = Vector3.Lerp(transform.position, posisiAkhir, Time.fixedDeltaTime * 6f);
				Debug.Log(posisiAkhir);
				
				if (transform.position == posisiAkhir)
				{
					transform.position = posisiAkhir;
					gerak = false;
					posisiAwal = transform.position;
				}
			}
			else
			{
				gerak = false;
			}
		}
	}

    public void Push(Vector3 direction)
    {
		if (IsDead()) return;
		EnemyDetection(direction);
    }

	private bool IsDead()
	{
		if (!invisible) hp--;

		if (hp <= 0)
		{
			Destroy(gameObject);
			return true;
		}
		return false;
	}
	void EnemyDetection(Vector3 playerDirection)
	{
        posisiAkhir = transform.position + playerDirection;
        gerak = true;

		RaycastHit2D hitWall = Physics2D.Raycast(transform.position, playerDirection, 1f, wallLayer);

		if (hitWall.collider != null)
		{
			wall = true;
		}
		else
		{
			wall = false;
		}

		RaycastHit2D hitEnemy = Physics2D.Raycast((transform.position + playerDirection * 0.7f), playerDirection, 0.7f, enemyLayer);

		if (hitEnemy.collider != null)
		{
			tabrakan = true;
		}
		else
		{
			tabrakan = false;
		}
	}
}
