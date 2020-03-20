using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


namespace SingleMagicMoba
{
    public class CharacterMovement : MonoBehaviour
    {
        

        


        #region variables

        [Header("AbilityStats")]
        [SerializeField]
        private AttackDefinition heroAttack;
        [SerializeField]
        private Transform[] HeroSpellHotSpot;
        [SerializeField]
        private Transform[] HeroSpellDirectionBuffers;
        private float timeOfHeroLastAttack = 0f;

        [Header("MeshAgent")]
        [SerializeField]
        private float speed;
        private float moveInputHorizontal = 0f;
        private float moveInputVertical = 0f;

        private Rigidbody rbody = null;
        private Animator anim = null;
        private NavMeshAgent agent = null;

        private Vector3 movement;
        #endregion

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            rbody = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            //agent.updateRotation = false;
            timeOfHeroLastAttack = float.MinValue;
        }

        private void Update()
        {
            //Player input
            moveInputHorizontal = Input.GetAxisRaw("Horizontal");
            moveInputVertical = Input.GetAxisRaw("Vertical");


            //Calculating attack Rate Test
            float timeSinceLastAttack = Time.time - timeOfHeroLastAttack;
            bool attackOnCooldown = timeSinceLastAttack < heroAttack.Cooldown;


            if (!attackOnCooldown && Input.GetKeyDown(KeyCode.Space))
            {
                agent.isStopped = true;
                timeOfHeroLastAttack = Time.time;
                anim.SetTrigger("isAttack");
            }
        }
        private void FixedUpdate()
        {
            MoveAndTurn();
        }

        //Animation attack trigger
        public void cast_finish()
        {
            if (heroAttack is Spell)
            {
                ((Spell)heroAttack).Cast(gameObject, HeroSpellHotSpot[0], HeroSpellDirectionBuffers[0].transform.position, LayerMask.NameToLayer("PlayerSpells"));
            }
            else if (heroAttack is SpellBostTriple)
            {
                ((SpellBostTriple)heroAttack).Cast(gameObject, HeroSpellHotSpot, HeroSpellDirectionBuffers, LayerMask.NameToLayer("PlayerSpells"));
            }
            agent.isStopped = false;
        }

       


        #region MovementAndTurning
        private void MoveAndTurn()
        {
            //No data
            if (moveInputHorizontal == 0 && moveInputVertical == 0)
            {
                anim.SetFloat("Speed", 0f);
                return;
            }

            //Move
            Vector3 movement = new Vector3(moveInputHorizontal, 0.0f, moveInputVertical) * speed * Time.deltaTime;
            Vector3 moveDestination = transform.position + movement;

            //Anim
            anim.SetFloat("Speed", agent.velocity.sqrMagnitude);

            //Agent move
            agent.destination = moveDestination;

            //Rotates only towards
            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            }
        }

        
        #endregion
    }

    //[System.Serializable] public class EventCstAnimComplete : UnityEvent<bool> { }

}