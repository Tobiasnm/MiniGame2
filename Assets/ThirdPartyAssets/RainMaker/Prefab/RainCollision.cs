﻿//
// Rain Maker (c) 2016 Digital Ruby, LLC
// http://www.digitalruby.com
//

using UnityEngine;
using System.Collections.Generic;

namespace DigitalRuby.RainMaker
{
    public class RainCollision : MonoBehaviour
    {
        private static readonly Color32 color = new Color32(255, 255, 255, 255);
        private readonly List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        public ParticleSystem RainExplosion;
        public ParticleSystem RainParticleSystem;
        public float lifetime = 0.15f;

        private void Start()
        {

        }

        private void Update()
        {

        }
        //TODO: make proper parameters
        private void Emit(ParticleSystem p, ref Vector3 pos)
        {
            int count = UnityEngine.Random.Range(2, 5);
            while (count != 0)
            {
                float yVelocity = UnityEngine.Random.Range(1.0f, 3.0f);
                float zVelocity = UnityEngine.Random.Range(-2.0f, 2.0f);
                float xVelocity = UnityEngine.Random.Range(-2.0f, 2.0f);
                // UnityEngine.Random.Range(0.25f, 0.75f);
                float size = UnityEngine.Random.Range(0.03f, 0.010f);
                ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();
                param.position = pos;
                param.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
                param.startLifetime = lifetime;
                param.startSize = size;
                param.startColor = color;
                p.Emit(param, 1);
                count--;
            }
        }

        private void OnParticleCollision(GameObject obj)
        {

            if (RainExplosion != null && RainParticleSystem != null)
            {
                int count = RainParticleSystem.GetCollisionEvents(obj, collisionEvents);
                for (int i = 0; i < count; i++)
                {
                    ParticleCollisionEvent evt = collisionEvents[i];
                    Vector3 pos = evt.intersection;
                    Emit(RainExplosion, ref pos);
                }
            }

            //TODO: Check for performance issues with colllision, do as area of effect if problematic.
            //check if player and apply damage. 
            if(obj.tag == "Player")
            {
                //obj.GetComponent<HandlePlayer>().RemoveHealth(50);
                Debug.Log("You died!");
                obj.GetComponent<Animator>().SetTrigger("Die");
            }

        }
    }
}