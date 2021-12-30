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


        public Image HealthBar;

        public Text HealthText;
        
        [SerializeField]
        private SFX DeathSound;
        [SerializeField]
        private SFX DecreaseHealthSound;
        [SerializeField]
        private SFX GameOverSound;
        [SerializeField]
        private ParticleSystem HitEffect;
        [SerializeField]
        private Text Hearts;
        [SerializeField]
        private Transform GameOver;
        [SerializeField]
        private GameObject GameOverImage;
        [SerializeField]
        private CameraShake CameraShake;
        [SerializeField]
        private AudioManager AudioManager;
        [SerializeField]
        private int CountOfLifes;
        public int CurrentCountOfLifes;
        private PlayerAnimationController Animator { get; set; }
        private PlayerController Controller { get; set; }
        private Weapon Weapons { get; set; }
        public Transform StartPosition;

        private float NextDecreaseHealth;
        public bool isDeath = false;

        private void Start()
        {
            Animator = this.GetComponent<PlayerAnimationController>();
            Controller = this.GetComponent<PlayerController>();
            Weapons = this.GetComponent<Weapon>();
            HealthText.text = CurrentHealth.ToString() + "%";
            Hearts.text = $"x{CountOfLifes}";
            CurrentCountOfLifes = CountOfLifes;
        }
        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
            NextDecreaseHealth = Time.time + TimeOfDecreaseHealth;
        }

        private void Update()
        {
            if (CurrentHealth >= 0)
                HealthText.text = CurrentHealth.ToString() + "%";
            else HealthText.text = "0%";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.Water))
            {
                CurrentHealth = 0;
                CheckDeath();
            }
        }

        private void CheckLifes()
        {
            if (CountOfLifes > 1)
            {
                CountOfLifes--;
                Hearts.text = $"x{CountOfLifes}";
            }
            else
            {
                StartPosition = GameOver;
                CountOfLifes = 3;
                Hearts.text = $"x{CountOfLifes}";
                CurrentCountOfLifes=CountOfLifes;
                GameOverImage.gameObject.SetActive(true);
                Invoke(nameof(SetGameOverImageFalse), 3f);
                AudioManager.Play(GameOverSound);
            }
        }


        private void SetGameOverImageFalse() => GameOverImage.gameObject.SetActive(false);
        
        public void DecreaseHealth(float damage)
        {
            CurrentHealth -= damage;
            HealthBar.fillAmount = CurrentHealth/100;
            CameraShake.StartShake();
            CheckDeath();
            AudioManager.Play(DecreaseHealthSound);
            HitEffect.Play();
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
            HealthBar.fillAmount = MaxHealth/100;
            HealthText.text = MaxHealth.ToString() + "%";
            Animator.SetTriggerIDLE();
            Controller.startMove = true;
            isDeath = false;
            Weapons.SetGreenWeapon();
        }
    }
}
