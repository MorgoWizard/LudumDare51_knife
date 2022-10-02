using System.Collections;
using UnityEngine;

public class StateChecker : MonoBehaviour
{
    private bool _onFire;
    [SerializeField] private float fireDamage;

    [SerializeField] private Enemy enemy;
    private void FixedUpdate()
    {
        if (_onFire)
        {
            enemy.TakeDamage(fireDamage * Time.fixedDeltaTime);
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
        StartCoroutine(OnFireStateTimer(3f));
    }
}
