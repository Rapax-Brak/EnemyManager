/* 
Author: Noah Wilson
Title: EnemyManager for Game Design

use Fantasy Monster: Skeleton Asset

Set up Animator 
*/
using System.Collections
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	
	public int damage;
	public int health;
	public bool boss, miniboss, tutorial;
	public GameObject text;
	public Transform player;
	static Animator anim;
	GameObject player = GameObject.Find("Player");
	
	void Start() {
		anim = GetComponent<Animator>();
		SetStats();
	}
	
	void Update(){
		text.GetComponent<TextMesh>().text = health.ToString("00") + " %";
		
			Vector3 direction = player.position - this.transform.position;
			float angle = Vector3.Angle(direction, this.transform.forward);
			if(Vector3.Distance(player.position, this.transform.position) < 10 && angle < 30){
			
			direction.y = 0;
			
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
			
			anim.SetBool("isIdle", false);
			if (direction.magnitude > 5){
				this.transform.Translate(0,0,0.05f);
				anim.SetBool("isWalking",true);
				anim.SetBool("isAttacking", false);
			}
			else{
				anim.SetBool("isAttacking",true);
				anim.SetBool("isWalking", false);
				GiveDamage();
			}
		}
		else {
			anim.SetBool("isIdle",true);
			anim.SetBool("isWalking",false);
			anim.SetBool("isAttacking",false);
		}
	}
	public void TakeDamage(int amount){
		health -= Amount;
		if (health <= 0){
			Destroy();
		}
	}
		void Destroy(){
			Destroy(this.gameObject);
		}
		
	public void SetStats() {
		if (boss){
			damage = 30;
			health = 200;
		}else if (miniboss){
			damage = 30; 
			health = 150;
		}else if (tutorial){
			damage = 10;
			health = 30;
		} else { 
			damage = 10;
			health = 100;
		}
	}
	
	void Attack() {
		Target target = player.GetComponent<MeleeManager>;
		target.TakeDamage(damage);
	}
}
