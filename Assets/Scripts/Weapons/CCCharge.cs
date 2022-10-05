using System.Threading;
using UnityEngine;

public class CCCharge : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    [SerializeField] private float _screenshakeTime = 0.1f;
    [SerializeField] private float _screenshakeIntensity = 1;
    [SerializeField] private float _screenshakeDuration = 1;

    private float _timer;
    private float _radius;
    private int _baseDamage;
    private int _fixedDamage;

    private SoundManager _soundManager;

    public void Init(float time, float radius, int baseDamage, int fixedDamage)
    {
        _timer = time;
        _radius = radius;
        _baseDamage = baseDamage;
        _fixedDamage = fixedDamage;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        SpawnExplosionEffect();
        SoundManager.GetInstance().PlayExplosionSound();
        CheckForHits();
        Destroy(this.gameObject);
    }

    private void CheckForHits()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in collidersHit)
        {
            if (collider.gameObject.tag == "Player")
            {
                Impact impact = ImpactCalculator.GetImpact(transform.position, collider.transform.position, _radius);
                float damage = _baseDamage * impact.intensity;

                WormController hitWorm = collider.gameObject.GetComponent<WormController>();
                hitWorm.TakeDamage(((int)damage));
                hitWorm.ApplyKnockback(impact.direction, impact.intensity);
            }
            else if (collider.gameObject.tag == "Destructible")
            {
                Impact impact = ImpactCalculator.GetImpact(transform.position, collider.ClosestPoint(transform.position), _radius);
                float damage = _baseDamage * impact.intensity + _fixedDamage;
                collider.gameObject.GetComponent<Destructible>().TakeDamage(((int)damage));
            }
        }
    }

    private void SpawnExplosionEffect()
    {
        ExplosionBehaviour explosion = Instantiate(_explosion, transform.position, Quaternion.identity).GetComponent<ExplosionBehaviour>();
        explosion.Init(_screenshakeTime, _screenshakeIntensity, _screenshakeDuration);
    }
}
