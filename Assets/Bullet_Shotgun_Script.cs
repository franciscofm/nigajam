using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shotgun_Script : MonoBehaviour {

	float damage;
	float stun;
	int jumps;
	float subbullet_damage;

	float speed = 25.0f;
	float expire = 0.2f;
	float live;

	// Use this for initialization
	public void setAll(float d, float s, int j, float sd) {
		damage = d; stun = s; jumps = j; subbullet_damage = sd;
		live = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		live += Time.deltaTime;
		if (live >= expire) {
			Destroy (gameObject);
		}
		transform.position += speed * Time.deltaTime * transform.right;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag != "Enemy") 
			Destroy (gameObject);
		Enemy es = col.gameObject.GetComponent<Enemy> ();
		es.subHp (damage);
		if (jumps > 0) {
			for (int i = 0; i < 3; ++i) {
				GameObject t = GameObject.Instantiate (gameObject);
				Physics2D.IgnoreCollision (t.GetComponent<BoxCollider2D>(),col.gameObject.GetComponent<CircleCollider2D>());
				t.transform.position = transform.position + transform.right + 0.15f * Random.insideUnitSphere;
				float angle = Mathf.Atan2 (t.transform.position.y - transform.position.y, t.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
				t.transform.rotation = Quaternion.Euler (0f,0f,angle);
				Bullet_Shotgun_Script bss = t.GetComponent<Bullet_Shotgun_Script> ();
				bss.setAll (subbullet_damage, stun, jumps - 1, subbullet_damage);
			}
		}
		Destroy (gameObject);
	}
}
