using System.Collections;
using UnityEngine;

public class StateChecker : MonoBehaviour
{
    private bool _onFire;
    [SerializeField] private float fireDamage, fireTimer;

    [SerializeField] private Enemy enemy;
    private void FixedUpdate()
    {
        if (_onFire)
        {
            enemy.TakeDamage(fireDamage * Time.fixedDeltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_onFire)
            {
                collision.gameObject.GetComponent<StateChecker>().SetOnFire();
            }
        }
    }

    private IEnumerator OnFireStateTimer(float onFireTimer)
    {
        _onFire = true;
        yield return new WaitForSeconds(onFireTimer);
        _onFire = false;
    }
    
    public void SetOnFire()
    {
        StartCoroutine(OnFireStateTimer(fireTimer));
    }
}
