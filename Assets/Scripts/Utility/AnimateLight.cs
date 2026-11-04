using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Splines;

public class AnimateLight : MonoBehaviour
{
    [SerializeField] private float _timeToAnimate  = 3.0f;
    [SerializeField] private float _timeToReset    = 3.0f;
    [SerializeField] private float _startIntencity = 0.0f;
    [SerializeField] private float _endIntencity   = 10.0f;

    private SplineInstantiate _parent;
    private Light             _light;

    public void Start()
    {
        _parent = GetComponentInParent<SplineInstantiate>();
        _light  = GetComponent<Light>();

        Activate();
    }

    public void Deactivate()
    {
        StopAllCoroutines();
    }

    public void Activate()
    {
        Deactivate();

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float timer = 0.0f;

        while(timer < _timeToAnimate)
        {
            timer += Time.deltaTime;

            float percentage = timer / _timeToAnimate;

            _light.intensity = percentage * (_endIntencity - _startIntencity);
            yield return null;
        }

        timer = 0.0f;

        while (timer < _timeToReset)
        {
            timer += Time.deltaTime;

            float percentage = timer / _timeToAnimate;

            _light.intensity = (1.0f - percentage) * (_endIntencity - _startIntencity);
            yield return null;
        }

        _parent.enabled = false;
    }
}
