using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage{ set; get;}

	void Start () {
		Destroy (gameObject, 3.0f);
	}
	
	void Update () {
	}

	void OnTriggerEnter (Collider c){
//		Destroy (gameObject);
	}
//	void OnBecameInvisible (){
//			Destroy (gameObject);
//	}
}
