using UnityEngine;

public class Box : MonoBehaviour {

	public GameObject explosion;
	int life{ set; get;}

	void Start () {
		rigidbody.velocity = new Vector3 (0, 100, 0);
		life = 30;
	}

	void Update (){
		Move ();
		Rotate ();
	}

	void Move (){
		rigidbody.AddForce (new Vector3 (0, -30, 0));
	}

	void Rotate (){
		transform.Rotate(new Vector3(-30, -30, -30) * Time.deltaTime);
	}

	void OnTriggerEnter (Collider c){
		Destroy (c.gameObject);
		life--;
		if (life <= 0) {Explosion();}
	}
//
	void Explosion(){
		Quaternion rot = Quaternion.Euler (0, 0, 0);
		Instantiate (explosion, transform.position, rot);
		Destroy (gameObject);
	}
}