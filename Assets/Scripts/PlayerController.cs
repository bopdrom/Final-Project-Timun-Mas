using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class PlayerController : MonoBehaviour
{
	//Animation Trigger
	bool gerak = false;
	bool wall = false;
	bool enemyKena = false;
	bool kick = false;
	bool moveUp = false;
	bool moveDown = false;
	bool death = false;
	bool beforeFinish = false;
	float moveHorizontal;

	//Movement
	public bool movePlayer;
	public int turn;
	bool habis = false;
	private Vector3 posisiAwal;
	private Vector3 posisiAkhir;

	//Object Detection
	public LayerMask enemyLayer, wallLayer;

	//Panels
	public GameObject ornament, deathScene;
	public TMP_Text turnText;

	//Animator
	Animator animator;
	Animator transition;
	AudioManager audioManager;

	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
		transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
	}

	void Start()
	{
		PlayerPrefs.SetInt("Turn", turn);
		turnText.text = turn.ToString();
		animator = GetComponent<Animator>();
		posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
		movePlayer = true;
	}

	void Update()
	{
		Movement();
		Animation();
		Transition();
	}

	void Movement()
	{
		if (!gerak && !habis && !movePlayer)
		{
			if (Input.GetKeyDown(KeyCode.D))
			{
				EnemyDetection(Vector2.right);

				if (!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(posisiAwal.x + 1f, transform.position.y, 0f);
					gerak = true;
					Turn();
					transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
					audioManager.PlaySFX(audioManager.walk);
				}
				if (enemyKena)
				{
					audioManager.PlaySFX(audioManager.kick);
					kick = true;
					Turn();
					transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);

				}
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				EnemyDetection(Vector2.left);

				if (!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(posisiAwal.x + -1f, transform.position.y, 0f);
					gerak = true;
					transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);
					audioManager.PlaySFX(audioManager.walk);
					Turn();
				}
				if (enemyKena)
				{
					audioManager.PlaySFX(audioManager.kick);
					kick = true;
					Turn();
					transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);

				}
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				EnemyDetection(Vector2.up);

				if (!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + 1f, 0f);
					gerak = true;
					moveUp = true;
					audioManager.PlaySFX(audioManager.walk);
					Turn();
				}
				if (enemyKena)
				{
					audioManager.PlaySFX(audioManager.kick);
					kick = true;
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
					moveDown = true;
					audioManager.PlaySFX(audioManager.walk);
					Turn();
				}
				if (enemyKena)
				{
					audioManager.PlaySFX(audioManager.kick);
					kick = true;
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

	void Animation()
	{
		moveHorizontal = Mathf.Abs(transform.position.x - posisiAkhir.x);

		animator.SetFloat("Move_Horizontal", moveHorizontal);
		animator.SetBool("Move_Up", moveUp);
		animator.SetBool("Move_Down", moveDown);
		animator.SetBool("Kick", kick);
		animator.SetBool("Death", death);

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

		if (stateInfo.IsName("P_Kick") && stateInfo.normalizedTime >= 1)
		{
			kick = false;
		}

		if (stateInfo.IsName("P_Move_Up") && stateInfo.normalizedTime >= 1)
		{
			moveUp = false;
		}

		if (stateInfo.IsName("P_Move_Down") && stateInfo.normalizedTime >= 1)
		{
			moveDown = false;
		}

		if (stateInfo.IsName("Ibu_Kick") && stateInfo.normalizedTime >= 1)
		{
			kick = false;
		}

		if (stateInfo.IsName("Ibu_Move_Up") && stateInfo.normalizedTime >= 1)
		{
			moveUp = false;
		}

		if (stateInfo.IsName("Ibu_Move_Down") && stateInfo.normalizedTime >= 1)
		{
			moveDown = false;
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

	void Transition()
	{
		AnimatorStateInfo stateInfo2 = transition.GetCurrentAnimatorStateInfo(0);

		if (stateInfo2.normalizedTime >= 1.0f)
		{
			movePlayer = false;
		}
	}

	void Turn()
	{
		turn = PlayerPrefs.GetInt("Turn");
		turn = turn - 1;
		PlayerPrefs.SetInt("Turn", turn);

		print(turn);
		turnText.text = turn.ToString();

		if (!beforeFinish)
		{
			if (turn < 1)
			{
				death = true;
				audioManager.PlaySFX(audioManager.defeat);
				habis = true;
				StartCoroutine(OnDeath());
			}
			else
			{
				habis = false;
			}
		}
		else
		{
			if (turn < 1)
			{
				habis = true;
			}
			else
			{
				habis = false;
			}
		}
	}

	IEnumerator OnDeath()
	{
		yield return new WaitForSeconds(2f);
		deathScene.SetActive(true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "BeforeFinish")
		{
			beforeFinish = true;
		}

		if (collision.tag == "Finish")
		{
			habis = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "BeforeFinish")
		{
			beforeFinish = false;
		}
	}
}