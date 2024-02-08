using System.Collections.Generic;
using System;
using UnityEngine;

public class CollisionNormalDecalSpawner : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        // If the collision occurs while the zombie is dying, it may throw the error 'Object reference not set to an instance of an object'
        try
        {
            if (other)
            {
                ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
                int numCollisionEvents = particleLauncher.GetCollisionEvents(other, collisionEvents);
                int i = 0;

                while (i < numCollisionEvents)
                {
                    //Spawns only when colliding with the world layer (World - 16, EntityAlive  - 15)
                    if (collisionEvents[i].colliderComponent.gameObject.layer == 16)
                        EmitAtLocation(collisionEvents[i]);

                    i++;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);

        splatterParticles.Emit(1);
    }
}