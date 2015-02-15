using UnityEngine;
using System.Collections;

public class Player3D : MonoBehaviour {
	// Common
	int counter;
	float speed;
	GameObject bullet_plant;
	public GameObject explosion;
	// Player
	int life { set; get;}
	int power;
	GameObject bit_plant;
	GameObject core;
	GameObject body;
	public Bit bit;
	public Bullet bullet_01;
	
	void Start () {
		counter = 0;
		speed = 500;
		life = 3;
		SetBitPlant ();
		SetBulletPlant ();
		Set2DComponents ();
	}
	
	void Update () {
		Move ();
		if (counter%100 == 0){Shot ();}
//		if (power >= 5 * bit_plant.transform.childCount) {BitAdd();}
		if (counter % 300 == 0) {BitAdd();}
		counter++;
	}
	
	void Move () {
		float dx = Input.GetAxisRaw("Horizontal");
		float dy = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3 (dx, dy, 0).normalized;
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.0375f, 0.05f));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.68f, 0.72f));
		Vector3 pos = transform.position;
		pos += direction * speed * Time.deltaTime;
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		transform.position = pos;
	}
	
	void Shot () {
		Bullet bullet_clone = Instantiate (bullet_01, transform.position, transform.rotation) as Bullet;
		bullet_clone.rigidbody.velocity = new Vector3 (0, 1000, 0);
	}
	
	void BitAdd (){
		Bit bit_clone = Instantiate (bit, transform.position, transform.rotation) as Bit;
		bit_clone.transform.parent = bit_plant.transform;
		SetBitAngle (1);
	}
	
	void BitDel (){
		if (bit_plant.transform.childCount != 0) {
			bit_plant.transform.GetChild (bit_plant.transform.childCount - 1).GetComponent<Bit> ().Explosion ();
			SetBitAngle (-1);
		}
	}
	
	void SetBitAngle (int d){
		int base_angle = 0;
		for (int i = 0; i < bit_plant.transform.childCount; i++) {
			Bit bit_i = bit_plant.transform.GetChild(i).GetComponent<Bit>();
			if(i == 0){ base_angle = bit_i.getAngle(); }
			bit_i.setAngle(base_angle, i, bit_plant.transform.childCount, d);
		}
		power = 0;
	}
	
	void OnTriggerEnter (Collider c){
		string layer_name = LayerMask.LayerToName (c.gameObject.layer);
		switch (layer_name) {
		case "3D_E_B":
			life --;
			BitDel ();
			Destroy (c.gameObject);
			core.transform.GetComponent<Animator> ().SetInteger ("life", life);
			body.transform.GetComponent<Animator> ().SetTrigger ("damage");
			break;
		}
		if (life <= 0) {Destroy (gameObject);}
	}
	
	public void Charge() {
		power++;
	}
	
	void Explosion() {
		Instantiate (explosion, transform.position, transform.rotation);
		if (life <= 0) {Destroy (gameObject);}
	}

	void SetBitPlant () {
		bit_plant = new GameObject ("BitPlant");
		bit_plant.transform.parent = transform;
		bit_plant.transform.position = transform.position;
	}
	
	void SetBulletPlant () {
		bullet_plant = new GameObject ("BulletPlant");
		bullet_plant.transform.parent = transform;
		bullet_plant.transform.position = transform.position;
	}
	
	void Set2DComponents () {
		core = transform.FindChild ("2D_Core").gameObject;
		core.transform.GetComponent<Animator> ().SetInteger ("life", life);
		body = transform.FindChild ("2D_Body").gameObject;
	}
	
}
