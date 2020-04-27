using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public float Health = 1f;
	public float playerHeal = 20f;

	[HideInInspector]
	public float maxHealth;

	protected PlayRandomSound _randomSound;
	protected Knockback _knockback;

	public void Start()
	{
		maxHealth = Health;

		_randomSound = GetComponent<PlayRandomSound>();
		_knockback = GetComponent<Knockback>();
	}

	public override string ToString()
	{
		return Mathf.Ceil(Health) + "/" + Mathf.Ceil(maxHealth);
	}

    public void Damage(float value, bool ambient = false)
    {
		if (!gameObject.activeSelf) return;

        Health = Math.maxf(Health - value, 0);

		if (gameObject.CompareTag("Player") && !ambient)
			_randomSound.play();

        if (Health <= 0.0f)
        {
            Kill();
        }
    }

	public void heal(float value)
	{
		Health = Math.minf(Health + value, maxHealth);
		//if (Health > maxHealth) maxHealth = Health;
	}

	protected void Kill()
    {
		NavMeshAgent nav = GetComponent<NavMeshAgent>();
		if (nav)
		{
			GetComponent<NavMeshAgent>().enabled = false;
		}

		if (gameObject.CompareTag("Player"))
		{
			GetComponent<PlayerCharacter>().playDeathSound();
			// Send to Game Over Scene
            SceneManager.LoadScene("GameOver");
		}
		else
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<CharacterHealth>().heal(playerHeal);
			player.GetComponent<PlayerCharacter>().enemyKilled();
			Debug.Log("enemy killed");

			//spawn another enemy
			GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>().spawnEnemy(1);

			Destroy(transform.gameObject);
		}
    }
}