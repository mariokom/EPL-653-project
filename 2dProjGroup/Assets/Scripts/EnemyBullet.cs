using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	private Rigidbody bulletRigidBody;
	private float bulletSpeed;
	private GameObject target;
	//private GameRepository repository;
	private float damage ; 
	private Vector3 tarpos;
	private Vector3 norm; 
	private float creationTime;
	
	// Use this for initialization
	void Start () {

		target = GameObject.FindGameObjectWithTag("Player");
		//repository = GameRepository.Instance;

		tarpos=target.transform.position ;
		norm = (tarpos - transform.position).normalized;

		bulletRigidBody = this.GetComponent<Rigidbody> ();

		damage = 10.0f; //damage sferas 
		bulletSpeed=5.5f;

		//get creation time
		creationTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if (GameRepository.isPaused()) {
			bulletRigidBody.Sleep();
			return;
		}

		if ( (Time.time - creationTime) > 2.0f) {
			Destroy(this.gameObject);
		}

		transform.position += norm * bulletSpeed * Time.deltaTime;

	}
	
	void OnCollisionEnter(Collision other){ 
		if (other.gameObject.tag == "Player") {
			GameRepository.losePlayerLife(damage);
		}
		if (other.gameObject.tag!= "Enemy")
		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider other){

	}
}
