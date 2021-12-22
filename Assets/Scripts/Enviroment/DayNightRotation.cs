using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace planTopia.Enviroment
{
public class DayNightRotation : MonoBehaviour
    {
        [SerializeField]
       
        private float DayDuration = 0.5f;
        [SerializeField]
       
        private float NightDuration = 1;
        [SerializeField] 
        private GameObject Enemies;
        [SerializeField]
        private GameObject SpecialObjects;
        [SerializeField]
        private Light Sun;
        [SerializeField] 
        private float MaxSunLightIntensity;
        [SerializeField]
        private Light Moon;
        [SerializeField] 
        private float MaxMoonLightIntensity;
        [SerializeField] 
        private AnimationCurve lightChangeCurve;
        [SerializeField] 
        private Color dayAmbientLight;

        [SerializeField] 
        private Color nightAmbientLight;
        private double DayDurationMillisecond => DayDuration * 60000; // there are 60000 milliseconds in a minute 
        private double NightDurationMillisecond => NightDuration * 60000; // there are 60000 milliseconds in a minute
        private Stopwatch ElapsedTime { get; set; }

        [SerializeField]
        private bool IsDay;
        private float angle;
        public Action<bool> OnTimeOfDayChanged;
        private void Start()
        {
            IsDay = true;
            ElapsedTime = new Stopwatch();
            ElapsedTime.Start();
        }
        private float t = 0;
        private void Update()
        {
            RotateSun();
            UpdateLightSettings();
        }

        private void RotateSun()
        {
            if (IsDay)
            {
                if (ElapsedTime.ElapsedMilliseconds <= DayDurationMillisecond)
                {
                    t += Time.deltaTime / (DayDuration * 60);
                }
                else
                {
                    IsDay = false;
                    ElapsedTime.Restart();
                    t = 0;
                }
            }
            else
            {
                if (ElapsedTime.ElapsedMilliseconds <= NightDurationMillisecond)
                {
                    t += Time.deltaTime / (NightDuration * 60);
                }
                else
                {
                    IsDay = true;
                    ElapsedTime.Restart();
                    t = 0;
                }
            }
            angle = Mathf.Lerp(IsDay ? 0 : 180, IsDay ? 180 : 360, t);
            Sun.transform.eulerAngles = new Vector3(angle, -30, 0);
            OnTimeOfDayChangedInvoke();
        }
        
        private void OnTimeOfDayChangedInvoke()
        {
            if (IsDay)
            {
                OnTimeOfDayChanged?.Invoke(IsDay);
                Invoke(nameof(SetEnemiesActiveFalse), 1.0f);
            }
            else
            {
                
                Enemies.SetActive(true);
                OnTimeOfDayChanged?.Invoke(IsDay);
            }
        }
        private void SetEnemiesActiveFalse() {
            Enemies.SetActive(false);
            


        }
        private void UpdateLightSettings()
        {
            float dotProduct = Vector3.Dot(Sun.transform.forward, Vector3.down);
            Sun.intensity = Mathf.Lerp(0, MaxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
            Moon.intensity = Mathf.Lerp(MaxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
            RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
        }
    }
}