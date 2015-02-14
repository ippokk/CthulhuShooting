using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnAnimationFinish (){
		Destroy (gameObject);
	}
}
