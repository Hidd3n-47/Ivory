using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddDontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private float _time = 2.0f;

    private bool _done = false;

    public void Add()
    {
        if (_done) return;
        _done = true;

        GameObject go = new GameObject(_name);

        DontDestroyOnLoad(go);

        StartCoroutine(Return());
    }

    private IEnumerator Return()
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene("LevelSelection");
    }
}
