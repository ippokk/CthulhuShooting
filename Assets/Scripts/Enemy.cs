using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy : MonoBehaviour {
	// Common
	int counter;
	float speed;
	float width;
	float height;
	GameObject bullet_plant;
	public GameObject explosion;
	// Enemy
	public int life { set; get;}
	int state;
	public Bullet bullet_01;

	void Start () {
		state = 0;
		counter = 0;
		SetBulletPlant ();
	}
	
	void Update () {
//		Shot ();
		counter++;
	}

	void OnBecameVisible (){
		state = 1;
	}

	void OnBecameInvisible (){
		if (state == 1) {
			Destroy(gameObject);
		}
	}

	void Shot () {
		if (counter%300 == 0){
			for(int i = 0; i < 24; i++){
				Bullet bullet_clone = Instantiate (bullet_01, transform.position, transform.rotation) as Bullet;
				bullet_clone.transform.parent = bullet_plant.transform;
				Vector2 direction = new Vector2 (Mathf.Cos (i * 2 * Mathf.PI / 24), Mathf.Sin (i * 2 * Mathf.PI / 24)).normalized;
				float bullet_speed = 300;
				bullet_clone.rigidbody2D.velocity = direction * bullet_speed;
			}
//			bullet.rigidbody2D.velocity = new Vector2(0, -500);
		}
	}

	void OnTriggerEnter2D (Collider2D c){
		string layer_name = LayerMask.LayerToName (c.gameObject.layer);
		switch (layer_name) {
		case "P_0":
			Explosion ();
			break;
		case "P_B":
			Destroy (c.gameObject);
			life -= c.GetComponent<Bullet>().damage;
			if(life <= 0){
				Explosion ();
			}
			break;
		}
		transform.GetComponent<Animator> ().SetTrigger ("damage");
	}

	void Explosion (){
		Instantiate (explosion, transform.position, transform.rotation);
//		foreach (Transform t in bullet_plant.transform.GetComponentInChildren<Transform> ()) {
//			if(t != transform){
//				t.parent = null;
//			}
//		}
//		for (int i = 0; i < bullet_plant.transform.childCount; i++) {
//			bullet_plant.transform.GetChild(i).transform.parent = null;
//		}
		Destroy (gameObject);
	} 

	void SetBulletPlant () {
		bullet_plant = new GameObject ("BulletPlant");
		bullet_plant.transform.parent = transform;
		bullet_plant.transform.position = transform.position;
	}
}
