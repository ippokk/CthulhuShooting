using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int state;
	private int counter;
	private int life;
	public float speed;
//	private float width;
//	private float height;
	public Bullet bullet_01;
	public GameObject explosion;

	void Start () {
		state = 0;
		counter = 0;
		life = 10;
//		width = renderer.bounds.size.x;
//		height = renderer.bounds.size.y;
	}
	
	void Update () {
		Shot ();
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
			life -= c.GetComponent<Bullet>().getDamage();
			if(life <= 0){
				Explosion ();
			}
			break;
		}
	}

	void Explosion (){
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	} 
}
