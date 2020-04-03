using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrajectory : MonoBehaviour
{
    GameObject particle;
    GameObject user;
    float damage;
    Vector3 speed;
    Vector3 pos;
    Vector3 acceleration;

    private void Update()
    {
        speed += acceleration * Time.deltaTime;
        pos += speed * Time.deltaTime;
        particle.transform.position = pos;
        this.transform.position = pos;
    }
    public void Init(GameObject caster, GameObject target, GameObject p , float accelerationRate , float d)
    {
        speed = Vector3.zero;
        pos = caster.transform.position;
        acceleration = (target.transform.position - caster.transform.position).normalized * accelerationRate;
        particle = p;
        damage = d;
        user = caster;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == user) return;
        PlayerStates targetState = other.GetComponent<PlayerStates>();
        if (targetState == null) return;
    
        targetState.GetHit(damage);
        
        Destroy(particle);
        Destroy(this.gameObject);
    }
}
