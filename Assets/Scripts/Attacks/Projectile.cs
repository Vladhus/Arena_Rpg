using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SingleMagicMoba
{
    public class Projectile : MonoBehaviour
    {
        [Header("ProjectileVFX")]
        [SerializeField]
        private GameObject muzzleFlashVfx;
        [SerializeField]
        private GameObject hitEffectVfx;
        public List<GameObject> trails;

        [Header("ProjectileAudio")]
        public AudioClip shotSFX;
        public AudioClip hitSFX;

        private GameObject caster;

        private float speed;
        private float range;

        private Vector3 travelDirection;
        private float distanceTraveled;


        public event Action<GameObject, GameObject> ProjectileCollided;

        public void Fire(GameObject Caster, Vector3 Target, float Speed, float Range)
        {

            caster = Caster;
            speed = Speed;
            range = Range;

            //calculate travel direction
            travelDirection = Target - transform.position;
            travelDirection.y = 0f;
            travelDirection.Normalize();







            // initialize distance traveled
            distanceTraveled = 0f;
            if (muzzleFlashVfx != null)
            {
                muzzleVFXSpawn();
            }

        }

        void Update()
        {
            // move this projectile through space
            float distanceToTravel = speed * Time.deltaTime;
            if (speed != 0f)
            {

                transform.Translate(travelDirection * distanceToTravel, Space.World);
            }

            // check to see if we traveled too far, if so destroy this projectile
            distanceTraveled += distanceToTravel;
            if (distanceTraveled > range)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {


            hitVfx(collision);
            if (ProjectileCollided != null)
            {
                ProjectileCollided(caster, collision.gameObject);
            }

        }

        private void hitVfx(Collision col)
        {
            ProjectileSfxHit();
            ProjectileDeleteTrails();

            speed = 0f;

            ContactPoint contact = col.contacts[0];

            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (hitEffectVfx != null)
            {
                var hitVfx = Instantiate(hitEffectVfx, pos, rotation) as GameObject;


                var particleSystemHit = hitVfx.GetComponent<ParticleSystem>();
                if (particleSystemHit != null)
                {
                    Destroy(hitVfx, particleSystemHit.main.duration);
                }
                else
                {
                    var particleSystemHit_Child = hitVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVfx, particleSystemHit_Child.main.duration);
                }
            }
            StartCoroutine(DestrojParticle(0f));
            Destroy(gameObject);
        }
        private void muzzleVFXSpawn()
        {
            var muzzleVfx = Instantiate(muzzleFlashVfx, transform.position, Quaternion.identity);
            muzzleVfx.transform.forward = gameObject.transform.forward;


            var particleSystemMuzzle = muzzleVfx.GetComponent<ParticleSystem>();

            if (particleSystemMuzzle != null)
            {
                Destroy(muzzleVfx, particleSystemMuzzle.main.duration);
            }
            else
            {
                var particleSystemMuzzle_Child = muzzleVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVfx, particleSystemMuzzle_Child.main.duration);
            }

            ProjectileSfxSpawn();
        }

        private void ProjectileSfxSpawn()
        {
            if (shotSFX != null && GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().PlayOneShot(shotSFX);
            }
        }

        private void ProjectileSfxHit()
        {
            if (hitSFX != null && GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().PlayOneShot(hitSFX);
            }
        }

        private void ProjectileDeleteTrails()
        {
            if (trails.Count > 0)
            {
                for (int i = 0; i < trails.Count; i++)
                {
                    trails[i].transform.parent = null;
                    var ps = trails[i].GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Stop();
                        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                    }
                }
            }
        }

        private IEnumerator DestrojParticle(float waitTime)
        {
            if (transform.childCount > 0 && waitTime != 0)
            {
                List<Transform> tList = new List<Transform>();

                foreach (Transform t in transform.GetChild(0).transform)
                {
                    tList.Add(t);
                }

                while (transform.GetChild(0).localScale.x > 0)
                {
                    yield return new WaitForSeconds(0.01f);
                    transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    for (int i = 0; i < tList.Count; i++)
                    {
                        tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
            }

            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }

    }

}











