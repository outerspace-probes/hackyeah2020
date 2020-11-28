using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    public ParticleSystem particleSys = default;
    [SerializeField] GameObject exploPrefab = default;
    [SerializeField] GameObject spawnedExploParent = default;

    GameObject exploHandler = null;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>(5);

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSys.GetCollisionEvents(other, collisionEvents);
        if (numCollisionEvents > 0)
        {
            SpawnExplosion(collisionEvents[0].intersection);
        }
    }

    private void SpawnExplosion(Vector3 exploPos)
    {
        exploHandler = Instantiate(exploPrefab, exploPos, Quaternion.identity);
        exploHandler.transform.parent = spawnedExploParent.transform;
        Destroy(exploHandler, 2f);
    }
}