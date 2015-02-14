using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private int damage;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void setDamage(int d){
		damage = d;	
	}

	public int getDamage(){
		return damage;	
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
