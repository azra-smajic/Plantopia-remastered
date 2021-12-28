using planTopia;
using planTopia.Controllers.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	[SerializeField]
	private enum CollectibleTypes {NoType, Health, Life}; 
	[SerializeField]
	private CollectibleTypes CollectibleType; 
	[SerializeField]
	private bool rotate; 

	[Range(1f,10f)]
	[SerializeField]
	private float HealthIncrease=5;
	[SerializeField]
	private float rotationSpeed;
	[SerializeField]
	private AudioClip collectSound;
	

	void Start () {
		
	}
	
	void Update () {

		

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(Constants.Tag.PLAYER)) 
			Collect (other.gameObject);
		
	}

	public void Collect(GameObject player)
	{
		if (CollectibleType == CollectibleTypes.Life && player.GetComponent<PlayerHealth>().Hearts.Where(x => x.gameObject.activeInHierarchy).ToList().Count == 3)
			return;
		if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		

		if (CollectibleType == CollectibleTypes.NoType) {

			return;
		}
		if (CollectibleType == CollectibleTypes.Health) {

			var currentHealth = player.GetComponent<PlayerHealth>().CurrentHealth;
			if (currentHealth + HealthIncrease > 100)
			{
				player.GetComponent<PlayerHealth>().CurrentHealth = 100;
				player.GetComponent<PlayerHealth>().HealthText.text = "100%";
				

			}
            else
            {
				player.GetComponent<PlayerHealth>().CurrentHealth = currentHealth + HealthIncrease;
				player.GetComponent<PlayerHealth>().HealthText.text = player.GetComponent<PlayerHealth>().CurrentHealth+"%";

			}
			player.GetComponent<PlayerHealth>().HealthBar.fillAmount = player.GetComponent<PlayerHealth>().CurrentHealth/100;
		}
		if (CollectibleType == CollectibleTypes.Life)
		{
			var heartCounter = player.GetComponent<PlayerHealth>().CountOfLifes;
			player.GetComponent<PlayerHealth>().Hearts[heartCounter].gameObject.SetActive(true);
			player.GetComponent<PlayerHealth>().CountOfLifes++;

		}

		Destroy(gameObject);
		
		
	}
}
