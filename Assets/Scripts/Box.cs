using UnityEngine;

public class Box : MonoBehaviour {

	GameObject box;
	public GameObject explosion;
	Camera c3d;
	Camera c2d;
	int life{ set; get;}

	void Start () {
		box = transform.parent.gameObject;
		box.rigidbody.velocity = new Vector3 (0, 100, 0);
		Physics.gravity *= 3;
		c3d = GameObject.Find("3DCamera").GetComponent<Camera>();
		c2d = GameObject.Find("2DCamera").GetComponent<Camera>();
		life = 30;
	}
	
	float speed = 5.0f;
	
	void Update (){
		Vector3 c3d_position = c3d.WorldToViewportPoint (box.transform.position);
		Vector2 c2d_position = c2d.ViewportToWorldPoint (c3d_position);
		transform.position = c2d_position;
		Rotate ();
	}

	void Rotate (){
		box.transform.Rotate(new Vector3(-30, -30, -30) * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D c){
		Destroy (c.gameObject);
		life--;
		if (life <= 0) {Explosion();}
	}

	void Explosion(){
		Quaternion rot = Quaternion.Euler (0, 0, 0);
		Instantiate (explosion, transform.position, rot);
		Destroy (box.gameObject);
	}
}