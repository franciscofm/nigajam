using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Canon_Script : MonoBehaviour {

	float damage;
	float damage_pudle;
	float damage_radius;
	float size_radius;

	public GameObject pudle;
	public GameObject radius;

	float speed = 15f;
	// Use this for initialization
	public void setAll(float d, float dp, float dr, float sr) {
		damage = d; damage_pudle = dp; damage_radius = dr; size_radius = sr;
		transform.Rotate (0f, 0f, -90f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += speed * Time.deltaTime * transform.right;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag != "Enemy") 
			Destroy (gameObject);
		Debug.Log ("Hello");
		col.gameObject.GetComponent<Enemy> ().subHp (damage);
		GameObject t;
		if (damage_pudle != 0f) {
			t = GameObject.Instantiate (pudle, transform.position, Quaternion.identity);
			t.GetComponent<Pudle_Script> ().damage = damage_pudle;
		}
		if (damage_radius != 0f) {
			t = GameObject.Instantiate (radius, transform.position, Quaternion.identity);
			t.GetComponent<Radius_Script> ().setAll (damage_radius,size_radius);
		}
		Destroy (gameObject);
	}
}
