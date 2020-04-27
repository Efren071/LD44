using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemyPrefab : SpawnPrefab
{
	public int spawnCount;
	public int minPlayerDistance;
}

[System.Serializable]
public class SceneData
{
	public SceneObject[] objects;
	public Constraints positionConstraints;
    public Constraints rotationConstraints;
	public int playerAvoidRadius;
	public Transform parent;
}

[System.Serializable]
public class SceneObject : SpawnPrefab
{
	public int count;
	public bool positionOverride;
	public bool rotationOverride;
}

public class LevelController : MonoBehaviour
{
	public float damagePerSecond;
	public float incrementDelay;
	public float increment;
	public GameObject hudPrefab;
	public SpawnPrefab player;
	public EnemyPrefab[] enemies;
    public GameObject camera;

	public SceneData sceneData;
	protected NavMeshSurface surface;

	protected GameObject _player;
	protected CharacterHealth _playerHealth;

	protected float lastUpdate;

    // Start is called before the first frame update
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        #if !UNITY_EDITOR
		//lock mouse
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        #endif

		surface = GetComponent<NavMeshSurface>();

		//spawn player
		_player = Instantiate(player.prefab,
			RandomVec.getRandomVector(player.positionConstraints),
			Quaternion.Euler(RandomVec.getRandomVector(player.rotationConstraints)));
		_playerHealth = _player.GetComponent<CharacterHealth>();

        //populate scene
        foreach (SceneObject t in sceneData.objects)
		{
			for (int i = 0; i < t.count; i++)
			{
				Instantiate(t.prefab,
					RandomVec.getRandomPosition(t.positionOverride ? t.positionConstraints : sceneData.positionConstraints, _player.transform.position, sceneData.playerAvoidRadius),
					Quaternion.Euler(RandomVec.getRandomVector(t.rotationOverride ? t.rotationConstraints : sceneData.rotationConstraints)),
					sceneData.parent);
			}
		}
		surface.BuildNavMesh();

		//spawn enemies
		foreach (EnemyPrefab enemy in enemies)
		{
			for (int i = 0; i < enemy.spawnCount; i++)
			{
				Instantiate(enemy.prefab,
					RandomVec.getRandomPosition(enemy.positionConstraints, _player.transform.position, enemy.minPlayerDistance),
					Quaternion.Euler(RandomVec.getRandomVector(enemy.rotationConstraints)));
			}
		}

		//create hud after player
		Instantiate(hudPrefab);

		lastUpdate = Time.time;
	}

	protected void Update()
	{
		if (lastUpdate + incrementDelay < Time.time)
		{
			damagePerSecond += increment;
			lastUpdate = Time.time;
		}
		_playerHealth.Damage(damagePerSecond * Time.deltaTime, true);
	}

	public float getTimeToIncrement()
	{
		return lastUpdate + incrementDelay - Time.time;
	}

	public void spawnEnemy(float time = 0)
	{
		StartCoroutine(spawnTimer(time));
	}

	IEnumerator spawnTimer(float time)
	{
		yield return new WaitForSeconds(time);

		EnemyPrefab enemy = enemies[Random.Range(0, enemies.Length - 1)];
		Instantiate(enemy.prefab,
			RandomVec.getRandomPosition(enemy.positionConstraints, _player.transform.position, enemy.minPlayerDistance),
			Quaternion.Euler(RandomVec.getRandomVector(enemy.rotationConstraints)));
	}
}
