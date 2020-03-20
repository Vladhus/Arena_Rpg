using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SingleMagicMoba
{
    [CreateAssetMenu(fileName = "Spell.asset", menuName = "Attack/SpellBoostTriple")]
    public class SpellBostTriple : AttackDefinition
    {
        public Projectile ProjectileToFire1;

        public float ProjectileSpeed;





        public void Cast(GameObject Caster, Transform[] HotSpot, Transform[] Targets, int Layer)
        {

            for (int i = 0; i < 3; i++)
            {
                // Instantiate Projectile #1
                Projectile projectile = Instantiate(ProjectileToFire1, HotSpot[i].position, HotSpot[i].rotation) as Projectile;
                projectile.transform.forward = Targets[i].position - HotSpot[i].transform.position;

                // Fire Projectile #1 at target
                projectile.Fire(Caster, Targets[i].position, ProjectileSpeed, Range);

                // Set Projectile's  #1 collision layer
                projectile.gameObject.layer = Layer;

                // Listen to Projectile Collided Events
                projectile.ProjectileCollided += OnProjectileCollided;
            }
            //// Instantiate Projectile #1
            //Projectile projectile = Instantiate(ProjectileToFire1, HotSpot[0].position , HotSpot[0].rotation) as Projectile;
            //projectile.transform.forward = Targets[0].position - HotSpot[0].transform.position;


            //// Fire Projectile #1 at target
            //projectile.Fire(Caster, Targets[0].position, ProjectileSpeed, Range);

            //// Set Projectile's  #1 collision layer
            //projectile.gameObject.layer = Layer;


            //Projectile projectile2 = Instantiate(ProjectileToFire1, HotSpot[1].position, HotSpot[1].rotation) as Projectile;
            //projectile2.transform.forward = Targets[1].position - HotSpot[1].transform.position;

            //projectile2.Fire(Caster, Targets[1].position, ProjectileSpeed, Range);

            //projectile2.gameObject.layer = Layer;

            //Projectile projectile3 = Instantiate(ProjectileToFire1, HotSpot[2].position, HotSpot[2].rotation) as Projectile;
            //projectile3.transform.forward = Targets[2].position - HotSpot[2].transform.position;

            //projectile3.Fire(Caster, Targets[2].transform.position, ProjectileSpeed, Range);

            //projectile3.gameObject.layer = Layer;

            //// Listen to Projectile Collided Events
            //projectile.ProjectileCollided += OnProjectileCollided; //For Projectile 1
            //projectile2.ProjectileCollided += OnProjectileCollided; // For Projectile 2 
            //projectile3.ProjectileCollided += OnProjectileCollided; // For Projectile 3
        }



        private void OnProjectileCollided(GameObject Caster, GameObject Target)
        {

            // Attack landed on target, create attack and attack the target

            // Make sure both the Caster and Target are still alive
            if (Caster == null || Target == null)
                return;

            // create the attack
            var casterStats = Caster.GetComponent<CharacterStats>();
            var targetStats = Target.GetComponent<CharacterStats>();

            var attack = CreateSpellAttack(casterStats, targetStats);

            // Send attack to all attackable behaviors of the target
            var attackables = Target.GetComponentsInChildren(typeof(IAttackable));
            foreach (IAttackable a in attackables)
            {
                a.OnAttack(Caster, attack);
            }
        }

    }

}