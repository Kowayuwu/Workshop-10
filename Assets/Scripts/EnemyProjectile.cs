using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private ParticleSystem collisionParticles;

    [SerializeField] private int damageAmount = 50;
    [SerializeField] private string tagToDamage;
    // Start is called before the first frame update
    public void Init(Vector3 direction)
    {
        velocity = direction.normalized * 25;
    }


    private void Update()
    {
        transform.Translate(this.velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage)
        {
            // Damage object with relevant tag. Note that this assumes the 
            // HealthManager component is attached to the respective object.
            var healthManager = col.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(this.damageAmount);

            // Create collision particles in opposite direction to movement.
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;
            particles.transform.rotation =
                Quaternion.LookRotation(-this.velocity);

            // Destroy self.
            Destroy(gameObject);
        }
    }
}
