using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	protected GameObject _player;
	protected CharacterHealth _playerHealth;
	protected PlayerCharacter _playerCharacter;
	protected Gun _playerGun;
	protected Slider _healthbar;
	protected Text _healthText;

	protected LevelController _levelController;
	protected Text _damageTimer;
	protected Text _dps;
	protected Text _kills;
	protected Text _grenadeCount;

    // Start is called before the first frame update
    protected void Start()
    {
		_player = GameObject.FindGameObjectWithTag("Player");
		_levelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();

		_playerHealth = _player.GetComponent<CharacterHealth>();
		_playerCharacter = _player.GetComponent<PlayerCharacter>();
		_playerGun =_player.GetComponentInChildren<Gun>();

		GameObject health = GameObject.Find("Health");
		_healthbar = health.GetComponentInChildren<Slider>();
		_healthText = health.GetComponentInChildren<Text>();

		_damageTimer = GameObject.Find("Increment Timer").GetComponentInChildren<Text>();

		_dps = GameObject.Find("DPS").GetComponentInChildren<Text>();

		_kills = GameObject.Find("Kills").GetComponentInChildren<Text>();

		_grenadeCount = GameObject.Find("GrenadeCount").GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    protected void Update()
    {
		if (_playerHealth && (_playerHealth.Health != _healthbar.value || _playerHealth.maxHealth != _healthbar.maxValue))
		{
			_healthbar.maxValue = _playerHealth.maxHealth;
			_healthbar.value = _playerHealth.Health;
			_healthText.text = _playerHealth.ToString();
		}

		if (_levelController && _damageTimer)
			_damageTimer.text = _levelController.getTimeToIncrement().ToString("0.00");

		if (_levelController && _dps)
			_dps.text = _levelController.damagePerSecond.ToString("0.00") + " dps";

		if (_levelController && _kills)
			_kills.text = _playerCharacter.getEnemiesKilledUI();

		if (_playerGun && _grenadeCount)
			_grenadeCount.text = _playerGun.grenadeCount.ToString();
	}
}
