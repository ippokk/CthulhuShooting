using UnityEngine;
using System.Collections;

public class Bit : MonoBehaviour {

	int counter;
	int timer;
	int angle;
	int ofset;
	int local_angle;
	int radius;
	public Bullet bullet_01;
	public GameObject explosion;
	public GameObject guard;

	void Start () {
		counter = 0;
		radius = 0;
	}
	
	void Update () {
		Move ();
		Shot ();
		counter++;
	}

	void Move (){
		angle = (counter + local_angle + ofset) % 360;
		if (radius < 90) { radius++; }
		if (ofset != 0) { if (ofset > 0) { ofset--; } else { ofset++; } }
		transform.localPosition = new Vector2 (Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad)) * radius * 2;
		transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
	}

	void Shot () {
		if (counter%5 == 0){
			Bullet bullet_clone = Instantiate (bullet_01, transform.position, transform.rotation) as Bullet;
			bullet_clone.rigidbody2D.velocity = new Vector2(0, 1500);
			bullet_clone.setDamage(1);
		}
	}

	void OnTriggerEnter2D (Collider2D c){
		string layer_name = LayerMask.LayerToName (c.gameObject.layer);
		if (layer_name == "E_B") {
			Destroy (c.gameObject);
			Guard() ;
		}
	}

	public int getAngle(){
		return angle;
	}

	public void setAngle(int b_a, int i, int n, int d){
		local_angle = (b_a + (360 * i / n)) % 360;
		if (i == 0){ ofset = 0; } else {
			ofset += (360 * i / (n * (n - d))) * d;
		}
		counter = 0;
	}

	void Guard (){
		Instantiate (guard, transform.position, transform.rotation);
		transform.parent.transform.parent.GetComponent<Player> ().Charge ();
	} 

	public void Explosion (){
		transform.parent = null;
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	} 
}
