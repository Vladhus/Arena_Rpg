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
        #region Events-Static
        Events.EventChangeTheAbilitySpell changeMePls = new Events.EventChangeTheAbilitySpell();
        #endregion

        #region variables

        [Header("AbilityStats")]
        [SerializeField]
        private AttackDefinition heroAttack;

        [SerializeField]
        private Transform[] HeroSpellHotSpot;

        [SerializeField]
        private Transform[] HeroSpellDirectionBuffers;

        [Header("MeshAgent")]
        [SerializeField]
        private float speed;
        private float moveInputHorizontal = 0f;
        private float moveInputVertical = 0f;
        private Vector3 movement = Vector3.zero;

        private Rigidbody rbody = null;
        private Animator anim = null;
        private NavMeshAgent agent = null;

  
        private CharacterStats stats;

        [Header("AttackStats")]
        private float timeOfHeroLastAttack = 0f;
        #endregion

        private void Start()
        {
            //Get ref
            stats = GetComponent<CharacterStats>();
            agent = GetComponent<NavMeshAgent>();
            rbody = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            
            //Initialize Spell slot with current Spell in CharacterStats_SO
            heroAttack = stats.GetCurrentSkill().itemDefinition.attackDefinition1;

            //Add listener to ChangeMePls Event (SetNextAbilityListener)
            changeMePls.AddListener(stats.SetNextAbilityListener);
            stats.characterDefinition.skillIsChangedByPickUpItem.AddListener(IhearAboutIt);

            //agent.updateRotation = false;
            timeOfHeroLastAttack = float.MinValue;
        }

        private void Update()
        {
            //Player input
            moveInputHorizontal = Input.GetAxisRaw("Horizontal");
            moveInputVertical = Input.GetAxisRaw("Vertical");

            //Next Spell activate
            if (Input.GetKeyDown(KeyCode.Q))
            {

                //Event Invoke => CharacterStats (SetNextAbilityListener)
                if (changeMePls != null)
                {
                    changeMePls.Invoke(true);
                }

                //Check for new Spell
                heroAttack = stats.GetCurrentSkill().itemDefinition.attackDefinition1;
            }

            //Calculating attack Rate Test
            float timeSinceLastAttack = Time.time - timeOfHeroLastAttack;
            bool attackOnCooldown = timeSinceLastAttack < heroAttack.Cooldown;

            //Do attack
            if (!attackOnCooldown && Input.GetKeyDown(KeyCode.Space))
            {
                agent.isStopped = true;
                timeOfHeroLastAttack = Time.time;

                //Call the attack animation
                anim.SetTrigger("isAttack");
            }
        }

        private void FixedUpdate()
        {
            MoveAndTurn();
        }

        //Recieve attack animation trigger
        public void cast_finish()
        {
            //Check heroAttack slot and get the type of current spell 
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

        public void IhearAboutIt(bool isSkillChanged)
        {
            if (isSkillChanged)
            {
                heroAttack = stats.GetCurrentSkill().itemDefinition.attackDefinition1;
            }
           
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

    

}