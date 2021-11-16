using planTopia.Enemies;
using planTopia.Generators.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Generators
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField]
        private Speed speed;
        [SerializeField]
        private ParticleSystem particle;
        [SerializeField]
        private Transform Player;
        [SerializeField]
        private PositionGenerator PositionGenerator;
        [SerializeField]
        private Generator EnemyPool;
        [SerializeField]
        private GameObject Fence;


        private int counter { get; set; } = 0;
        private int NumberOfEntries { get; set; } = 0;


        private bool started { get; set; } = false;
        private float NextEnemy { get; set; } = 0;

        //calculating a scale of EnemiesPlane
        private void Start()
        {
            //particle.Stop();
           
            counter = EnemyPool.Size;


        }
        //generate enemies in wanted amount
        private void FixedUpdate()
        {
            if (started && counter > 0  /*&&!EnemyPool.CheckActivity()*/)
            {
                if (Time.time > NextEnemy)
                {

                    NextEnemy = Time.time + speed.speed;
                    GameObject enemy = EnemyPool.Next(
                        Random.Range(-PositionGenerator.range.x, PositionGenerator.range.x), PositionGenerator.range.y,
                        Random.Range(-PositionGenerator.range.z, PositionGenerator.range.z),
                            PositionGenerator.origin);
                    if (enemy != default(GameObject))
                    {
                        enemy.GetComponent<EnemyController>().Player = Player;

                        enemy.SetActive(enabled);

                        counter--;
                        //particle.Play();
                    }
                }
            }
            //if (counter == 0 && EnemyPool.CheckActivity())
           //     Fence.SetActive(false);
            //particle.Stop();
        }

        //Cheching if a player is finally entering enemies plane
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag(Constants.Tag.PLAYER))
            {
               
                    //Fence.SetActive(enabled);
                    started = true;
                  
                    

            }
        }

    }
}
