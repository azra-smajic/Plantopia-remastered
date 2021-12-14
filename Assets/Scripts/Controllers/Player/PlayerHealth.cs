using planTopia.Core;
using planTopia.ScriptabileObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace planTopia.Controllers.Player
{
    public class PlayerHealth : MonoBehaviour
    {

        [SerializeField]
        [Range(1, 100)]
        private float MaxHealth = 100;
        [SerializeField]
        [Range(1, 60)]
        private float TimeOfDecreaseHealth = 2;
        [SerializeField]
        [Range(0.1f, 10)]
        private float DecrementHealth = 1;
        public float CurrentHealth;


        public Slider HealthSlider;

        public Text HealthText;

        [SerializeField]
        private SFX BackgroundSound;
        [SerializeField]
        private SFX DeathSound;
        [SerializeField]
        private SFX DecreaseHealthSound;
        [SerializeField]
        private SFX GameOverSound;

        public List<Image> Hearts;

        [SerializeField]
        private Transform GameOver;
        [SerializeField]
        private GameObject GameOverImage;
        [SerializeField]
        private CameraShake CameraShake;
        private PlayerAnimationController Animator { get; set; }
        private PlayerController Controller { get; set; }
        private Weapon Weapons { get; set; }
        public Transform StartPosition;
        private AudioManager AudioManager { get; set; }

        private float NextDecreaseHealth;
        public bool isDeath = false;
        public int CountOfLifes;

        private void Start()
        {
            Animator = this.GetComponent<PlayerAnimationController>();
            Controller = this.GetComponent<PlayerController>();
            Weapons = this.GetComponent<Weapon>();
            AudioManager = this.GetComponent<AudioManager>();
            HealthText.text = CurrentHealth.ToString() + "%";
            CountOfLifes = Hearts?.Count ?? 0;
            AudioManager.Play(BackgroundSound);
        }
        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
            NextDecreaseHealth = Time.time + TimeOfDecreaseHealth;
        }

        private void Update()
        {
            DecreaseHealthOnTime();
            if (CurrentHealth >= 0)
                HealthText.text = CurrentHealth.ToString() + "%";
            else HealthText.text = "0%";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.Water)
            {
                CurrentHealth = 0;
                CheckDeath();
            }
        }

        private void CheckLifes()
        {
            if (CountOfLifes > 1)
            {
                Hearts[CountOfLifes - 1].gameObject.SetActive(false);
                CountOfLifes--;
            }
            else
            {
                StartPosition = GameOver;
                Hearts.ForEach(x => x.gameObject.SetActive(true));
                CountOfLifes = Hearts?.Count ?? 0;
                GameOverImage.gameObject.SetActive(true);
                Invoke(nameof(SetGameOverImageFalse), 3f);
                AudioManager.Pause();
                AudioManager.Play(GameOverSound);
                Invoke(nameof(PlayBackgroundSound), 3.25f);
            }
        }

        private void PlayBackgroundSound() => AudioManager.Play(BackgroundSound);
        private void SetGameOverImageFalse() => GameOverImage.gameObject.SetActive(false);

        private void DecreaseHealthOnTime()
        {
            if (Time.time > NextDecreaseHealth && !isDeath)
            {
                CurrentHealth -= DecrementHealth;
                HealthSlider.value = CurrentHealth;
                NextDecreaseHealth = Time.time + TimeOfDecreaseHealth;
                CheckDeath();
            }
        }

        public void DecreaseHealth(float damage)
        {
            CurrentHealth -= damage;
            HealthSlider.value = CurrentHealth;
            CameraShake.StartShake();
            CheckDeath();
            AudioManager.Play(DecreaseHealthSound);
        }

        private void CheckDeath()
        {
            if (CurrentHealth <= 0 && !isDeath)
            {
                Controller.startMove = false;
                isDeath = true;
                Animator.SetTriggerDeath();
                AudioManager.Play(DeathSound);
                Invoke(nameof(CheckLifes), 1.5f);
                Invoke(nameof(RespawnPlayer), 3.5f);
            }
        }

        private void RespawnPlayer()
        {
            SetPosition();
            SetDefaultSettings();
        }

        public void SwitchLevel(Transform newLevel, bool isDeath = true)
        {
            StartPosition = newLevel;
            if (isDeath)
            {
                SetPosition();
                SetDefaultSettings();
            }
        }

        private void SetPosition()
        {
            this.transform.position = StartPosition.position;
            this.transform.rotation = StartPosition.rotation;
        }

        private void SetDefaultSettings()
        {
            CurrentHealth = MaxHealth;
            HealthSlider.value = MaxHealth;
            HealthText.text = MaxHealth.ToString() + "%";
            Animator.SetTriggerIDLE();
            Controller.startMove = true;
            isDeath = false;
            Weapons.SetGreenWeapon();
        }
    }
}
