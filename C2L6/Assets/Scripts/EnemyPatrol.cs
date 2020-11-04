using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

	public float speed = 1f;
	public float minX;
	public float maxX;
	public float waitingTime = 1f;
	public float ShootWaitingTime = 0.5f;
	
	private Weapon _weapon;
	private Weapon _weaponComponent;
	protected GameObject _target;
	private Animator _animator;

	void Awake()
    {
		_animator = GetComponent<Animator>();
		_weaponComponent = GetComponentInChildren<Weapon>();
	}

	void Start()
	{
		UpdateTarget();
		StartCoroutine("PatrolToTarget");
	}

	void Update()
	{

	}

	private void UpdateTarget()
	{
		// If first time, create target in the left
		if (_target == null)
		{
			_target = new GameObject("Target");
			_target.transform.position = new Vector2(minX, transform.position.y);
			transform.localScale = new Vector3(-1, 1, 1);
			return;
		}

		if (_target.transform.position.x == minX)
		{
			_target.transform.position = new Vector2(maxX, transform.position.y);
			transform.localScale = new Vector3(1, 1, 1);
		}

		// If we are in the right, change target to the left
		else if (_target.transform.position.x == maxX)
		{
			_target.transform.position = new Vector2(minX, transform.position.y);
			transform.localScale = new Vector3(-1, 1, 1);
		}

	}

	private IEnumerator PatrolToTarget()
	{
		while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
		{

			//Update Animator
			_animator.SetBool("IsWalking",true);

			// let's move to the target
			Vector2 direction = _target.transform.position - transform.position;
			float xDirection = direction.x;

			transform.Translate(direction.normalized * speed * Time.deltaTime);

			// IMPORTANT
			yield return null;
		}

		// At this point, i've reached the target, let's set our position to the target's one
		transform.position = new Vector2(_target.transform.position.x, transform.position.y);
		
		_animator.SetBool("IsWalking", false);

		//Update target (Rotate Y)
		UpdateTarget();
		yield return new WaitForSeconds(waitingTime); // IMPORTANT

		//Shoot
		_animator.SetTrigger("Shoot");
		_weaponComponent.Shoot();
		yield return new WaitForSeconds(ShootWaitingTime); // IMPORTANT
		_animator.SetTrigger("Shoot");
		_weaponComponent.Shoot();
		yield return new WaitForSeconds(ShootWaitingTime); // IMPORTANT
		_animator.SetTrigger("Shoot");
		_weaponComponent.Shoot();
		yield return new WaitForSeconds(ShootWaitingTime); // IMPORTANT

		//Start again
		StartCoroutine("PatrolToTarget");
	}

	
}