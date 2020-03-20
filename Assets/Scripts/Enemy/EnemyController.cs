using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;



namespace SingleMagicMoba
{
    public class EnemyController : MonoBehaviour
    {
        private Animator npcAnim;
        private NavMeshAgent zombieAgent;

        [Header("AttackStats")]
        public AttackDefinition attack;

        [SerializeField]
        private Transform SpellHotSpot;

        [Header("Navigation")]
        public Transform[] waypoints;
        public float aggroRange = 10;
        public float patrolTime = 10;

        private int index;
        private float speed, agentSpeed;
        private float timeOfLastAttack;
        private Transform player;

        [SerializeField]
        private MobType mobType;

        private void Awake()
        {
            npcAnim = GetComponent<Animator>();
            zombieAgent = GetComponent<NavMeshAgent>();
            agentSpeed = zombieAgent.speed;
            player = GameObject.FindGameObjectWithTag("Player").transform;

            InvokeRepeating("Tick", 0, 0.5f);

            if (waypoints.Length > 0)
            {
                InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
            }

            timeOfLastAttack = float.MinValue;
        }

        void Update()
        {
            //anim lerp
            speed = Mathf.Lerp(speed, zombieAgent.velocity.magnitude, Time.deltaTime * 10);
            npcAnim.SetFloat("Speed", speed);


            //calculating attack Rate
            float timeSinceLastAttack = Time.time - timeOfLastAttack;
            bool attackOnCooldown = timeSinceLastAttack < attack.Cooldown;

            zombieAgent.isStopped = attackOnCooldown;


            //calculating distance
            float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            bool attackInRange = distanceFromPlayer < attack.Range;

            if (!attackOnCooldown && attackInRange)
            {
                transform.LookAt(player.transform);
                timeOfLastAttack = Time.time;
                npcAnim.SetTrigger("ZombieAttack");
            }
        }

        public void Hit()
        {
            if (attack is Weapon)
            {

                ((Weapon)attack).ExecuteAttack(gameObject, player.gameObject);
            }

            else if (attack is Spell)
            {
                ((Spell)attack).Cast(gameObject, SpellHotSpot, player.transform.position, LayerMask.NameToLayer("EnemySpells"));
            }
        }

        void Patrol()
        {
            index = index == waypoints.Length - 1 ? 0 : index + 1;
        }

        void Tick()
        {
            zombieAgent.destination = waypoints[index].position;
            zombieAgent.speed = agentSpeed / 2;

            if (player != null && Vector3.Distance(transform.position, player.transform.position) < aggroRange)
            {
                zombieAgent.speed = agentSpeed;
                zombieAgent.destination = player.position;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
    public enum MobType
    {
        Caster,
        Melee
    }
}