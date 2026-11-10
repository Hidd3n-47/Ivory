using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] Transform _l1;
    [SerializeField] Transform _l1D;
    [SerializeField] Transform _l3;
    [SerializeField] Transform _l3D;
    [SerializeField] Transform _l5;
    [SerializeField] Transform _l5D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject go = GameObject.Find("DontDestroyOnLoad");

        if(GameObject.Find("Level1"))
        {
            _l1.gameObject.SetActive(false);
            _l1D.gameObject.SetActive(true);
        }
        if (GameObject.Find("Level3"))
        {
            _l3.gameObject.SetActive(false);
            _l3D.gameObject.SetActive(true);
        }
        if (GameObject.Find("Level5"))
        {
            _l5.gameObject.SetActive(false);
            _l5D.gameObject.SetActive(true);
        }
    }
}
