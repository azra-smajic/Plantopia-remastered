using System;
using System.Collections;
using System.Collections.Generic;
using planTopia.Core;
using planTopia.Enemies;
using UnityEngine;
using TMPro;


namespace planTopia.Enviroment
{
    public class DayAndNightController : MonoBehaviour
    {
        [SerializeField] 
        private float dayDurationInMin;

        [SerializeField] 
        private float startHour;

        [SerializeField] 
        private TextMeshProUGUI timeText;

        [SerializeField] 
        private Light sunLight;

        [SerializeField] 
        private float sunriseHour;

        [SerializeField] 
        private float sunsetHour;

        [SerializeField] 
        private Color dayAmbientLight;

        [SerializeField] 
        private Color nightAmbientLight;

        [SerializeField] 
        private AnimationCurve lightChangeCurve;

        [SerializeField] 
        private float maxSunLightIntensity;

        [SerializeField] 
        private Light moonLight;

        [SerializeField] 
        private float maxMoonLightIntensity;
        
      
        public bool isDay;
        
        [SerializeField] 
        private GameObject Enemies;


        private DateTime currentTime;

        private TimeSpan sunriseTime;

        private TimeSpan sunsetTime;

        private float nextTime;

        private double percentage;

        private TimeSpan sunriseToSunsetDuration => CalculateTimeDifference(sunriseTime, sunsetTime);
        private TimeSpan sunsetToSunriseDuration => CalculateTimeDifference(sunsetTime, sunriseTime);

        public Action<bool> OnTimeOfDayChanged;
        
        
        void Start()
        {
            currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
            nextTime = Time.time;

            sunriseTime = TimeSpan.FromHours(sunriseHour);
            sunsetTime = TimeSpan.FromHours(sunsetHour);
        }
        
        void Update()
        {
            UpdateTimeOfDay();
            RotateSun();
            UpdateLightSettings();
        }

        private void UpdateTimeOfDay()
        {
            if (Time.time>=nextTime)
            {
                currentTime = currentTime.AddSeconds(1440/dayDurationInMin);
                nextTime =Time.time+ 1;

                if (timeText != null)
                {
                    timeText.text = currentTime.ToString("HH")+" hours";
                }
            }
        }

        private bool IsEventTriggered = false;


        private double lastPercentage { get; set; } = 0;
        private float sunLightRotation { get; set; }= 0;

        private void RotateSun()
        {

            if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
            {
                TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

                percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
               
                // this.InvokeRepeating(() =>
                // {
                //     percentage.Fade(0, 1, 0.2f * Time.deltaTime);
                //     sunLightRotation = Mathf.Lerp(4, 140, percentage);
                // }, 0, 10);
                //
                //Debug.Log(percentage);
                
                sunLightRotation = Mathf.Lerp(4, 140, (float)percentage);
                
                isDay = true;
                lastPercentage = percentage;
                
            }
            else
            {
                TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

                percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

                sunLightRotation = Mathf.Lerp(140, 364, (float) percentage);
                
                isDay = false;
            }


            if (isDay)
            {

               
                Enemies.SetActive(false);
               


            }
            else {
                
                Enemies.SetActive(true);
                

            }
            OnTimeOfDayChanged?.Invoke(isDay);

            sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        }
     
        private IEnumerator WaitAndPrint(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }

        private void UpdateLightSettings()
        {
            float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
            sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
            moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
            RenderSettings.ambientLight =
                Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
        }

        private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
        {
            TimeSpan difference = toTime - fromTime;

            if (difference.TotalSeconds < 0)
            {
                difference += TimeSpan.FromHours(24);
            }

            return difference;
        }
    }
}