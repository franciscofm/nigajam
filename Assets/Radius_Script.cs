using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius_Script : MonoBehaviour {
	float damage;
	float size;
	float lifetime;
	float duration = 0.2f;
	// Use this for initialization
	public void setAll(float d, float s){
		damage = d;
		size = s/duration;
		lifetime = 0f;
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		lifetime += Time.deltaTime;
		if (lifetime > duration)
			Destroy (gameObject);
		transform.localScale += Time.deltaTime * Vector3.one * size;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag != "Enemy")
			return;
		col.gameObject.GetComponent<Enemy> ().subHp (damage);
	}

}