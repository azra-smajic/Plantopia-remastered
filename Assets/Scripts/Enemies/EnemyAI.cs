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
    public class EnemyAI: MonoBehaviour
    {
        [SerializeField] 
        private Transform player;
        [SerializeField] 
        private LayerMask whatIsPlayer;
        [SerializeField] 
        private float health=100;
        [SerializeField] 
        [Range(1,10)]
        private float helathIncrease;
        [SerializeField] 
        [Range(1,10)]
        private float timeHealthIncrease;

        public bool onDestination=false;

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
        [SerializeField]
        private EnemyShooting EnemyShooting;
        //Attacking
        [SerializeField] 
        private float timeBetweenAttacks;
        
        private bool alreadyAttacked;

        //States
        [SerializeField] 
        private float sightRange, attackRange;
        [SerializeField] 
        private bool playerInSightRange, playerInAttackRange;

        private bool isHidden = false;
        
        //Hide
        [SerializeField] 
        private List<GameObject> hiddenPlaces;
        [SerializeField] 
        private float escapeSpeed;

        private Vector3 nextHiddenPlace;
        private bool hiddenPlaceSet;
        private Vector3 distanceToFleePoint;


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
            
            if (health <= 20) Hide();
            else if (onDestination&&health <= 35) Healed();
            else if(!playerInSightRange&&!playerInAttackRange) Patroling();
            else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            else if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }

        private bool IsOnDestination(Vector3 currentPosition, Vector3 nextPosition)
        {
            return (currentPosition - nextPosition).magnitude < 1f ? true : false;
        }
        private void Healed()
        {
            if (onDestination&&Time.time > timeDeltaTime)
            {
                health += helathIncrease;
                timeDeltaTime = Time.time + timeHealthIncrease;
            }
        }

        private void Hide()
        {
            if(!hiddenPlaceSet)
                NextFleePoint();
            
            if (playerInAttackRange&&hiddenPlaceSet)
            {
                agent.SetDestination(nextHiddenPlace);
                agent.speed = escapeSpeed;
                onDestination = false;
            }

            if (IsOnDestination(transform.position, nextHiddenPlace))
            {
                hiddenPlaceSet = false;
                onDestination = true;
            }

            Healed();
        }
        private void NextFleePoint()
        {
            nextHiddenPlace = hiddenPlaces[UnityEngine.Random.Range(0, hiddenPlaces.Count)].transform.position;
            hiddenPlaceSet = true;
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

            onDestination = IsOnDestination(transform.position, walkPoint);
            if(health<100)
                Healed();
        }
        
        private void NextWalkPoint()
        {
            walkPoint = patrolingPositions[UnityEngine.Random.Range(0, patrolingPositions.Count)].transform.position;
            walkPointSet = true;
        }
        private void AttackPlayer()
        {
            onDestination = false;
            agent.SetDestination(transform.position);
            // transform.LookAt(Player);
            LookAtPlayer();
            if (!alreadyAttacked)
            {
              EnemyShooting.OnStartFiring(timeBetweenAttacks);
            }
        }

        private void DestroyProjectile(GameObject obj)
        {
            Destroy(obj);
        }

        private void ResetAttack()=>alreadyAttacked = false;
        private void ChasePlayer()
        {
            onDestination = false;

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
            health -= damage;
            
            if(health<=0)
                Invoke(nameof(DestroyEnemy), .5f);
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
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
