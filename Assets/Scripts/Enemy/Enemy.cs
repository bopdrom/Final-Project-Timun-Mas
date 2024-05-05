using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool gerak = false;
    private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
    int darahEnemy = 10;

    void Start(){
        posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
    }

    void Update(){
        if(!gerak){
            if(Input.GetKeyDown(KeyCode.D) && KenaKiri.kena)
            {
                posisiAkhir = new Vector3(posisiAwal.x + 1f, transform.position.y, 0f);
                gerak = true;
                darahEnemy = darahEnemy - 5;
            }else if(Input.GetKeyDown(KeyCode.A) && KenaKanan.kena)
            {
                posisiAkhir = new Vector3(posisiAwal.x + -1f, transform.position.y, 0f);
                gerak = true;
                darahEnemy = darahEnemy - 5;
            }else if(Input.GetKeyDown(KeyCode.W) && KenaBawah.kena){
                posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + 1f, 0f);
                gerak = true;
                darahEnemy = darahEnemy - 5;
            }else if(Input.GetKeyDown(KeyCode.S) && KenaAtas.kena){
                posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + -1f, 0f);
                gerak = true;
                darahEnemy = darahEnemy - 5;
            }
        }

        if(darahEnemy <= 0){
            Destroy(gameObject);
            }

        if(gerak){
                transform.position = Vector3.MoveTowards(transform.position, posisiAkhir, 5 * Time.deltaTime);

                if (transform.position == posisiAkhir)
                {
                    gerak = false;
                    posisiAwal = transform.position;
                }
        }
    }
}
