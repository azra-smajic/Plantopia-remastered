using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;


namespace planTopia.Enemies
{
    public class BossAI: MonoBehaviour
    {
        [SerializeField] 
        private Transform player;
        [SerializeField] 
        private LayerMask whatIsPlayer;
        [SerializeField] 
        private float health=100;
        [SerializeField] 
        [Range(1,10)]
        private float healthIncrease=2f;
        [SerializeField] 
        [Range(1,10)]
        private float timeHealthIncrease=3;
        [SerializeField]
        private ParticleSystem[] particles;


        private float timeDeltaTime;
        private NavMeshAgent agent { get; set; }
        
        //Patroling
        [SerializeField] 
        private List<GameObject> patrolingPositions;
        [SerializeField] 
        float patrolingSpeed;
        private Vector3 walkPoint;
        private bool walkPointSet;
        private Vector3 DistanceToWalkPoint;
        

        //Attacking
        [SerializeField] 
        private float timeBetweenAttacks;
     
        private bool alreadyAttacked;
        [SerializeField]
        private EnemyShooting EnemyShooting;
        //States
        [SerializeField] 
        private float sightRange, attackRange;
        [SerializeField] 
        private bool playerInSightRange, playerInAttackRange;




        [SerializeField]
        private float phase2Speed=1f;

     


        private Collider[] colliders;
        private RaycastHit info;
        private Ray ray;

        private void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();
            timeDeltaTime = Time.time;
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            
            if (health <= 50 && health>=30) Phase1();
            if (health < 30) Phase2();
       
            if(!playerInSightRange&&!playerInAttackRange) Patroling();
            else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            else if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }

        private void Phase2()
        {
            
            timeBetweenAttacks = 0.5f;
            if (Time.time > timeDeltaTime)
            {

                health += healthIncrease;
                timeDeltaTime = Time.time + timeHealthIncrease;
            }
            agent.speed = phase2Speed;
        }

        private void Phase1()
        {
            timeBetweenAttacks = 1f;
            attackRange = 10;

        }

        private bool IsOnDestination(Vector3 currentPosition, Vector3 nextPosition)
        {
            return (currentPosition - nextPosition).magnitude < 1f ? true : false;
        }
   

       

        private void Patroling()
        {
            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
                agent.speed = patrolingSpeed;
            }
            else NextWalkPoint();
            
            if (IsOnDestination(transform.position, walkPoint))
                walkPointSet = false;

            
          
        }
        
        private void NextWalkPoint()
        {
            walkPoint = patrolingPositions[UnityEngine.Random.Range(0, patrolingPositions.Count)].transform.position;
            walkPointSet = true;
        }
        private void AttackPlayer()
        {
          
            agent.SetDestination(transform.position);
            // transform.LookAt(Player);
            LookAtPlayer();
            
                EnemyShooting.OnStartFiring(timeBetweenAttacks);
            
        }

        private void DestroyProjectile(GameObject obj)
        {
            Destroy(obj);
        }

        private void ResetAttack()=>alreadyAttacked = false;
        private void ChasePlayer()
        {

            
            agent.SetDestination(player.position);
            // transform.LookAt(Player);
            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            Vector3 direction = (player.position - transform.position).normalized;
            
            Quaternion lookRoration=Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation=Quaternion.Slerp(transform.rotation, lookRoration, Time.deltaTime*5f);
        }
        public void TakeDamage(float damage)
        {
            particles[0].Play();
            particles[1].Play();
            particles[2].Play();
            health -= damage;

            if (health <= 0)
            {
                particles[3].Play();
                Invoke(nameof(DestroyEnemy), 2f);
            }
        }

        private void DestroyEnemy()
        {
            gameObject.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color=Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
            
        }
    }
}
