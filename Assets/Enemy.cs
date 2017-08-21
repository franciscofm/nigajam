using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	float hp;

	// Use this for initialization
	void Start () {
		hp = 30f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void subHp(float q) {
		hp -= - q;
		if (hp <= 0)
			die ();
	}

	private void die() {

	}
}
