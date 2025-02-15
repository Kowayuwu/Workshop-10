﻿// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameObject projectilePrefab;

    private MeshRenderer _renderer;
    Vector3 direction = Vector3.back;

    private void Awake()
    {
        this._renderer = gameObject.GetComponent<MeshRenderer>();
        var projectile = Instantiate(this.projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.GetComponent<EnemyProjectile>().Init(direction);
    }

    // This method listens to HealthManager "onHealthChanged" events. The actual
    // event listening is set up within the editor interface. This is purely for
    // visuals currently, and takes a fractional value between 0 and 1.
    public void UpdateHealth(float frac)
    {
        this._renderer.material.color = Color.red * frac;
    }

    // Same as above, but listens to onDeath events.
    public void Kill()
    {
        var particles = Instantiate(this.deathEffect);
        particles.transform.position = transform.position;
    }
}
