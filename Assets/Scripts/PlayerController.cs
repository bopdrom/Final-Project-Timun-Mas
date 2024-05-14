using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	bool gerak = false, wall = false, enemyKena = false;
	private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
	public LayerMask enemyLayer, wallLayer;

	Rigidbody2D rb;

	void Start()
	{
		PlayerPrefs.SetInt("Turn", 40);
		rb = GetComponent<Rigidbody2D>();
		posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
	}

	void Update()
	{
		if (!gerak)
		{
			if (Input.GetKeyDown(KeyCode.D))
			{
				EnemyDetection(Vector2.right);

				if (!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(posisiAwal.x + 1f, transform.position.y, 0f);
					gerak = true;
					Turn();
				}
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				EnemyDetection(Vector2.left);

				if (!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(posisiAwal.x + -1f, transform.position.y, 0f);
					gerak = true;
					Turn();
				}
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				EnemyDetection(Vector2.up);

				if (!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + 1f, 0f);
					gerak = true;
					Turn();
				}
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				EnemyDetection(Vector2.down);

				if (!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + -1f, 0f);
					gerak = true;
					Turn();
				}
			}
		}
		if (gerak)
		{
			transform.position = Vector3.MoveTowards(transform.position, posisiAkhir, 5 * Time.deltaTime);

			// Cek jika objek sudah mencapai posisi akhir
			if (transform.position == posisiAkhir)
			{
				gerak = false;
				posisiAwal = transform.position;
			}
		}
	}

	void EnemyDetection(Vector2 playerDirection)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, 1f, enemyLayer);

		if (hit.collider != null)
		{
			hit.collider.GetComponent<Enemy>().Push(playerDirection);
			enemyKena = true;
		}
		else
		{
			enemyKena = false;
		}

		RaycastHit2D hitTembok = Physics2D.Raycast(transform.position, playerDirection, 1f, wallLayer);

		if (hitTembok.collider != null)
		{
			wall = true;
		}
		else
		{
			wall = false;
		}
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy")
		{
			Debug.Log("Kena");
		}
	}

	void Turn()
	{
		int turn = PlayerPrefs.GetInt("Turn");
		turn = turn - 1;
		PlayerPrefs.SetInt("Turn", turn);

		print(turn);

		if (turn < 1)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
