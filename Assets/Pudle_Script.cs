using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pudle_Script : MonoBehaviour {

	public float damage;
	float lifetime;
	// Use this for initialization
	void Start () {
		lifetime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		lifetime += Time.deltaTime;
		if (lifetime > 5f)
			Destroy (gameObject);
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag != "Enemy")
			return;
		col.gameObject.GetComponent<Enemy> ().subHp (damage * Time.deltaTime);
	}
}
