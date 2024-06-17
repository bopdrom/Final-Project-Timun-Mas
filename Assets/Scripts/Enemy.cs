using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool gerak = false, wall = false, tabrakan = false, bush = false, coconut = false;
    private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
	public LayerMask wallLayer, enemyLayer;
	public int hp = 2;
	public bool invisible;
	public GameObject sprite;

	Animator animator;

	void Start(){
        posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
		animator = sprite.GetComponent<Animator>();
    }

	void Update() {

		if (gerak)
		{
			if (!wall && !tabrakan)
			{
				transform.position = Vector3.Lerp(transform.position, posisiAkhir, Time.fixedDeltaTime * 6f);
				
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

		Animation();
	}

    public void Push(Vector3 direction)
    {
		//if (IsDead()) return;
		EnemyDetection(direction);
		hp--;

		if (hp <= 0)
		{
			if (gameObject.tag == "Bush")
			{
				bush = true;
			}
			if (gameObject.tag == "Coconut")
			{
				coconut = true;
			}
		}
	}

	//private bool IsDead()
	//{
	//	if (!invisible) hp--;

	//	if (hp <= 0)
	//	{
	//		if (gameObject.tag == "Bush")
	//		{
	//			bush = true;
	//		}
	//		if (gameObject.tag == "Coconut")
	//		{
	//			coconut = true;
	//		}
	//		//Destroy(gameObject);
	//		//return true;
	//	}
	//	return false;
	//}
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

	void Animation()
	{
		animator.SetBool("Bush", bush);
		animator.SetBool("Coconut", coconut);

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		if (stateInfo.IsName("E_Bush_Destroy") && stateInfo.normalizedTime >= 1)
		{
			//hancur1 = false;
			Destroy(gameObject);
		}
		if (stateInfo.IsName("E_Coconut_Destroy") && stateInfo.normalizedTime >= 1)
		{
			//hancur1 = false;
			Destroy(gameObject);
		}
	}
}
