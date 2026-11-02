using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

internal class AttackState : IState
{
    [SerializeField] private Transform      _projectilePrefab;
    [SerializeField] private float          _attackCooldown   = 1;
    [SerializeField] private float          _projectileSpeed  = 25.0f;
    [SerializeField] private TargetSelector _targetSelector;

    private bool _canAttack = true;

    public override void StateUpdate()
    {
        if (_canAttack && _targetSelector.Target)
        {
            var projectileObject = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

            Vector3 direction = Vector3.Normalize(_targetSelector.Target.position - transform.position);
            projectileObject.GetComponent<Rigidbody>().AddForce(direction * _projectileSpeed, ForceMode.Impulse);

            StartCoroutine(Cooldown());
        }
    }

    public override void OnStateExit()
    {
        StopAllCoroutines();
        _canAttack = true;
    }

    IEnumerator Cooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
