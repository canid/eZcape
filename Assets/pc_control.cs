using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pc_control : MonoBehaviour {
	private Animator animator;
	public float speed = 0f;
	protected float stamina = 1f;
	protected float health = 1f;
	public GameObject zomble;
	public List<GameObject> zombles;
	public RectTransform guihealth;
	public RectTransform guistamina;

	void Start()
	{
		animator = this.GetComponent<Animator>();
	}
	
	// Our updater
	void Update()
	{
		speed = 3f;
		bool quitme = Input.GetKey(KeyCode.Escape);
		bool sprint = Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		if (health <= 0f) {
			Application.Quit();
		}

		if (quitme) {
			Application.Quit();
		}

		guihealth.sizeDelta = new Vector2((120*health), 12);
		guistamina.sizeDelta = new Vector2((120*stamina), 12);

		if (sprint) {
			if (stamina >= 0.01f) stamina -= 0.005f;
			speed = speed + (2 * stamina);
		}

		if(Input.GetKeyDown(KeyCode.C)) { //spawn zome zombles
			StartCoroutine(spawnz());
		}

		if (vertical > 0) {
			animator.SetInteger("direction", 1);
			transform.position = new Vector2(transform.position.x, transform.position.y+speed);
			if (horizontal > 0) {
				transform.position = new Vector2(transform.position.x+speed, transform.position.y);
			} else if (horizontal < 0) {
				transform.position = new Vector2(transform.position.x-speed, transform.position.y);
			}
		} else if (vertical < 0) {
			animator.SetInteger("direction", 2);
			transform.position = new Vector2(transform.position.x, transform.position.y-speed);
			if (horizontal > 0) {
				transform.position = new Vector2(transform.position.x+speed, transform.position.y);
			} else if (horizontal < 0) {
				transform.position = new Vector2(transform.position.x-speed, transform.position.y);
			}
		} else if (horizontal > 0) {
			animator.SetInteger("direction", 3);
			transform.position = new Vector2(transform.position.x+speed, transform.position.y);
			if (vertical > 0) {
				transform.position = new Vector2(transform.position.x, transform.position.y+speed);
			} else if (vertical < 0) {
				transform.position = new Vector2(transform.position.x, transform.position.y-speed);
			}
		} else if (horizontal < 0) {
			animator.SetInteger("direction", 4);
			transform.position = new Vector2(transform.position.x-speed, transform.position.y);
			if (vertical > 0) {
				transform.position = new Vector2(transform.position.x, transform.position.y+speed);
			} else if (vertical < 0) {
				transform.position = new Vector2(transform.position.x, transform.position.y-speed);
			}
		} else if (vertical == 0 && horizontal == 0) {
			animator.SetInteger("direction", 0);
			if (stamina < 1) stamina += 0.01f;
		}
	}

	void OnCollisionEnter2D(Collision2D hit) {
		if (hit.collider.tag == "zomble") {
			health -= 0.1f;

		} else if (hit.collider.tag == "bandaid" && health <= 0.8) {
			health += 0.2f;
		}
	}

	void OnCollisionStay2D(Collision2D loiter) {
		if (loiter.collider.tag == "zomble") {
			health -= 0.0001f;
		}
	}

	IEnumerator spawnz() {
		for (int z=0; z<10; z++) {
			zombles.Add ((GameObject)Instantiate (zomble, new Vector3 (100, -100, 0), Quaternion.identity));
			yield return new WaitForSeconds (0.25f);
		}
	}
}