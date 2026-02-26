using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour {
    NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Navigate(Vector2 position)
    {
        _agent.SetDestination(position);
    }

    public void NavigateRandom(Vector2 position, float distance) {
        Vector2 destination = position + Random.insideUnitCircle * distance;
        NavMeshHit hit;
        NavMesh.SamplePosition(destination, out hit, distance, NavMesh.AllAreas);
        _agent.SetDestination(hit.position);
    }

    public void Stop(Vector2 position)
    {
        _agent.isStopped = true;
        _agent.destination = position;
    }

    void Update() {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }


    // void HandleParticles(Vector3 delta) { 
    //     if (delta == Vector3.zero) {
    //         _footstepsFx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    //         return;
    //     }
        
    //     delta = delta * -1;
    //     ParticleSystem.VelocityOverLifetimeModule forceOverLifetimeModule = _footstepsFx.velocityOverLifetime;
        
    //     AnimationCurve curve = new AnimationCurve();

    //     curve.AddKey(0.0f,  delta.x);
    //     curve.AddKey(0.75f,  delta.x);
        
    //     forceOverLifetimeModule.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
        
    //     curve = new AnimationCurve();
        
    //     curve.AddKey(0.0f,  delta.y);
    //     curve.AddKey(0.75f,   delta.y);
        
    //     forceOverLifetimeModule.y = new ParticleSystem.MinMaxCurve(1.5f, curve);
    // }
}