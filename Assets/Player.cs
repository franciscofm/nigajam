using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D rb2;
	CircleCollider2D cc2;
	Weapon weapon;

	int level;
	int currentExp, maxExp;

	float speedmovement;

	float hp;
	float hpmax;
	float hpregen;

	float mana;
	float manamax;
	float manaregen;

	Vector2 movement;
	int lucky;

	// Use this for initialization
	void Start () {
		rb2 = GetComponent<Rigidbody2D> ();
		cc2 = GetComponent<CircleCollider2D> ();
		weapon = GetComponent<Weapon> ();

		movement = new Vector2 (0f, 0f);
		lucky = 1;

		level = 1;
		currentExp = 0;
		maxExp = 200;
		speedmovement = 2.0f;
		hpmax = hp = 25f;
		manamax = mana = 50f;
		manaregen = 2f;
		hpregen = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//hp&mana 
		if(hp < hpmax)
			hp = Mathf.Min (hpmax, hp + hpregen * Time.deltaTime);
		if(mana < manamax)
			mana = Mathf.Min (manamax, mana + manaregen * Time.deltaTime);
		//movement
		movement.x = Input.GetAxisRaw ("HorizontalWASD");
		movement.y = Input.GetAxisRaw ("VerticalWASD");
		movement.Normalize();
		movement *= speedmovement;
		rb2.velocity = movement;

		//shooting
		movement.x = Input.GetAxisRaw ("HorizontalKEY");
		movement.y = Input.GetAxisRaw ("VerticalKEY");
		if (!movement.Equals (Vector2.zero)) {
			movement.Normalize ();
			float angle = Mathf.Atan2 (movement.x, movement.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0f,0f,angle);
			weapon.shoot ();
		}

		//change weapon
		if (Input.GetKeyDown (KeyCode.E))
			weapon.switchWeapon (1);
		if (Input.GetKeyDown (KeyCode.Q))
			weapon.switchWeapon (-1);
		if (Input.GetKeyDown (KeyCode.Alpha1))
			weapon.setWeapon (0);
		if (Input.GetKeyDown (KeyCode.Alpha2))
			weapon.setWeapon (1);
		if (Input.GetKeyDown (KeyCode.Alpha3))
			weapon.setWeapon (2);
		if (Input.GetKeyDown (KeyCode.Alpha4))
			weapon.setWeapon (3);
	}

	//use negative for healing
	public void subHp(float q) {
		hp = Mathf.Min (hpmax, hp - q);
		if (hp <= 0)
			die ();
	}
	public void subMana(float q) {
		mana = Mathf.Min (manamax, mana - q);
		if (mana < 2) {
			if (Random.Range (0, 100) < lucky)
				mana = manamax;
		}
	}
	public float getMana() {
		return mana;
	}

	public void getExp(int q){
		currentExp += q;
		if(currentExp > maxExp){
			++level;
			currentExp = 0;
			maxExp = (int) (maxExp * 1.2f);
		}
	}

	private void die() {

	}
}
