using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int counter;
	public int life;
	int power;
	public float speed;
//	float width;
	float height;
	GameObject bit_store;
	GameObject core;
	public Bit bit;
	public GameObject bullet_01;
	public GameObject explosion;

	void Start () {
		counter = 0;
		CreateBitStore ();
		CreateCore ();
//		width = renderer.bounds.size.x;
		height = renderer.bounds.size.y;
	}
	
	void Update () {
		Move ();
//		if (counter%10 == 0){Shot ();}
		if (power >= 5 * bit_store.transform.childCount) {BitAdd();}
		counter++;
	}

	void Move () {
		float dx = Input.GetAxisRaw("Horizontal");
		float dy = Input.GetAxisRaw("Vertical");
		Vector2 direction = new Vector2 (dx, dy).normalized;
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		Vector2 pos = transform.position;
		pos += direction * speed * Time.deltaTime;
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y + height/2, max.y - height/2);
		transform.position = pos;
	}

	void Shot () {
		Vector2 pos = transform.position;
		pos.y += height/4;
		GameObject bullet_clone = Instantiate (bullet_01, pos, transform.rotation) as GameObject;
		bullet_clone.rigidbody2D.velocity = new Vector2(0, 1000);
	}

	void BitAdd (){
		Bit bit_clone = Instantiate (bit, transform.position, transform.rotation) as Bit;
		bit_clone.transform.parent = bit_store.transform;
		SetBitAngle (1);
	}

	void BitDel (){
		bit_store.transform.GetChild (bit_store.transform.childCount - 1).GetComponent<Bit> ().Explosion ();
		SetBitAngle (-1);
	}

	void SetBitAngle (int d){
		int base_angle = 0;
		for (int i = 0; i < bit_store.transform.childCount; i++) {
			Bit bit_i = bit_store.transform.GetChild(i).GetComponent<Bit>();
			if(i == 0){ base_angle = bit_i.getAngle(); }
			bit_i.setAngle(base_angle, i, bit_store.transform.childCount, d);
		}
		power = 0;
	}

	void OnTriggerEnter2D (Collider2D c){
		string layer_name = LayerMask.LayerToName (c.gameObject.layer);
		switch (layer_name) {
			case "E_0":
				Destroy (c.gameObject);
				break;
			case "E_B":
				Destroy (c.gameObject);
				break;
		}
		life --;
		BitDel ();
		Explosion ();
		core.transform.GetComponent<Animator> ().SetInteger ("life", life);
		transform.GetComponent<Animator> ().SetTrigger ("damage");
	}

	public void Charge() {
		power++;
	}

	void Explosion() {
		Instantiate (explosion, transform.position, transform.rotation);
		if (life <= 0) {Destroy (gameObject);}
	}

	void CreateBitStore() {
		bit_store = transform.FindChild ("BitStore").gameObject;
		bit_store.transform.parent = transform;
		bit_store.transform.position = transform.position;
	}

	void CreateCore() {
		life = 3;
		core = transform.FindChild ("Core").gameObject;
		core.transform.GetComponent<Animator> ().SetInteger ("life", life);
	}

}
