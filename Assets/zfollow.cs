using UnityEngine;
using System.Collections;

public class zfollow : MonoBehaviour {

	Transform target;
	float speed = 1f;
	LayerMask maskbg = 1 << 8;
	LayerMask maskplayer = 1 << 9;

	void Awake() {

	}

	void Start() {

	}

	void Update () {
		target = GameObject.Find("pc_still").transform; //target current player transform (location)
		RaycastHit2D visibility = Physics2D.Raycast(transform.position, target.position-transform.position, Mathf.Infinity, maskplayer|maskbg);
		if(visibility) {
			if (visibility.collider.tag == "Player") {
				transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
			}
		}
	}
}
