using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage{ set; get;}
	public int state;

	void Start () {
	}
	
	void Update () {
	}

	void OnBecameInvisible (){
		Destroy (gameObject);
	}
}
