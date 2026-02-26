using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] Projectile _projecitle;
    [SerializeField] float _launchDistance = 2f;
    Rigidbody _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = GameObject.Instantiate(_projecitle.gameObject);
            projectile.GetComponent<Projectile>().Launch(transform.position + -transform.forward * _launchDistance, -transform.forward);
        }
    }
}
