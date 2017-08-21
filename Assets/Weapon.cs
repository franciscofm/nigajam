using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	Player player;
	//escopeta, canon, chorro, spray
	float[] manacost;
	float[] castspeed;
	float[] damage;
	float[] cd;
	public GameObject[] bullets;
	int type;
	float speed;

	void Start(){
		player = GetComponent<Player> ();
		manacost = new float[]{ 3f, 5f, 1f, 1f };
		castspeed = new float[]{ 1f, 2f, 0.2f, 0.1f };
		damage = new float[]{ 5f, 9f, 0.3f, 0.2f };
		cd = new float[]{ 0f, 0f, 0f, 0f };
		type = 0;
		speed = 1f;
	}

	Vector3 forward;

	int shotgun_bullets = 2;
	float shotgun_stun = 0f;
	int shotgun_jump = 1;
	float shotgun_radius = .0f;
	float shotgun_subbullet_damage = 2f;

	float canon_pudle = 1f; //dano del charco
	float canon_area = 1f;	//dano de area
	float canon_area_size = 3f; //tamano de dano de area

	public void shoot(){
		//Miramos que tenga mana y se lo quitamos si es asi
		float cost = manacost [type];
		if (player.getMana() < cost) {
			noMana ();
			return;
		}
		player.subMana (cost);

		//Miramos que no este en cd el arma
		float cast = castspeed [type];
		if (cd [type] > Time.time) 
			return;
		cd [type] = Time.time + cast * speed;

		GameObject bullet = bullets [type];
		forward = -transform.up;
		shotgun_radius = (shotgun_bullets - 1) * 0.085f;

		switch (type) {
		case 0:
			//Escopeta
			GameObject[] t = new GameObject[shotgun_bullets];
			for (int i = 0; i < shotgun_bullets; ++i) {
				t [i] = GameObject.Instantiate (bullet);
				t [i].transform.position = transform.position + forward + shotgun_radius * Random.insideUnitSphere;
				float angle = Mathf.Atan2 (t [i].transform.position.y - transform.position.y, t [i].transform.position.x - transform.position.x) * Mathf.Rad2Deg;
				t[i].transform.rotation = Quaternion.Euler (0f,0f,angle);
				Bullet_Shotgun_Script bss = t [i].GetComponent<Bullet_Shotgun_Script> ();
				bss.setAll (damage [type], shotgun_stun, shotgun_jump, shotgun_subbullet_damage);
			}
			break;
		case 1:
			//Canon
			GameObject t_canon = GameObject.Instantiate (bullet);
			t_canon.transform.position = transform.position + forward;
			t_canon.transform.rotation = transform.rotation;
			Bullet_Canon_Script bcs = t_canon.GetComponent<Bullet_Canon_Script> ();
			bcs.setAll (damage [type], canon_pudle, canon_area, canon_area_size);
			break;
		case 2:
				break;
		case 3:
				break;
		}

	}

	private void noMana() {
		//Debug.Log ("No mana no party.");
	}

	public void switchWeapon(int next){
		type += next;
		type %= 4;
		Debug.Log (type);
	}
	public void setWeapon(int t) {
		if (t < 0 || 3 < t)
			return;
		type = t;
		Debug.Log (type);
	}

	public void createPerk() {

	}
}
