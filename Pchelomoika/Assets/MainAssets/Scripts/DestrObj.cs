using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrObj : MonoBehaviour
{
	public ParticleSystem leafParticle;

	[SerializeField] private Transform deadPart;

	public float life = 3;

	[SerializeField] private AudioSource damageAudio;
	[SerializeField] private AudioSource deathAudio;	

	[Header("Отталкивание")]

	private float shakeDuration = 0f;

	private float shakeMagnitude = 0.125f;

	private float dampingSpeed = 0.5f;

	Vector3 initialPosition;


	[Header("Отталкивание Rigidbody2D")]

	[SerializeField] private bool isRig;

	[SerializeField] private float knockSpeedX;

	private Rigidbody2D rb;

	private float knockSpeedY = 1f;

	private PlayerMovement pl;



	private void Start()
    {
		if (isRig) rb = gameObject.GetComponent<Rigidbody2D>();

		pl = GameObject.FindFirstObjectByType<PlayerMovement>();

	}


	void Awake()
	{
		initialPosition = transform.localPosition;
	}


	void Update()
	{
		CheckDamage();
	}

	private void CheckDamage()
    {
		if (life <= 0)
		{
			Destroy(gameObject);

			deadPart.parent = null;
			deathAudio.enabled = true;
			deathAudio.Play();
        }

        else if (shakeDuration > 0 && !isRig)
		{
			transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

			shakeDuration -= Time.deltaTime * dampingSpeed;
		}

		else if (shakeDuration > 0 && isRig)
		{
			if (pl.isRight) rb.velocity = new Vector2(knockSpeedX, knockSpeedY);

			else rb.velocity = new Vector2(-knockSpeedX, knockSpeedY);

			transform.localPosition +=  Random.insideUnitSphere * shakeMagnitude;

			shakeDuration -= Time.deltaTime * dampingSpeed;
		}

		else if (!isRig)
		{
			shakeDuration = 0f;

			transform.localPosition = initialPosition;
		}
	}

	public void Damage(float damage)
	{
		life -= Mathf.Abs(damage);
		shakeDuration = 0.05f;
		Instantiate(leafParticle, transform.position, Quaternion.identity);
		damageAudio.Play();
	}
}
