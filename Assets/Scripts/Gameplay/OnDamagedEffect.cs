using System.Collections;
using UnityEngine;

public class OnDamagedEffect : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Material     _onDamagedMaterial;
    [SerializeField] private float        _onDamagedEffectDuration = 0.3f;

    public void Cancel()
    {
        StopAllCoroutines();
    }

    private Material _defaultMaterial;

    public void Start()
    {
        _defaultMaterial = _mesh.material;
    }

    public void TriggerOnDamageEffect()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeMaterialEffect());
    }

    private IEnumerator ChangeMaterialEffect()
    {
        _mesh.material = _onDamagedMaterial;
        yield return new WaitForSeconds(_onDamagedEffectDuration);
        _mesh.material = _defaultMaterial;
    }
}
