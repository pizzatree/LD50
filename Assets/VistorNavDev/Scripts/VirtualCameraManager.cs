using UnityEngine;

public class VirtualCameraManager : MonoBehaviour
{
    public static VirtualCameraManager Instance;
    [SerializeField] GameObject _mainCamera;

    void Awake()
    {
        if (!Instance) { Instance = this; }
        else {Destroy(gameObject);}
    }

    public void PanToVisitor(GameObject visitorCam)
    {
        if (!_mainCamera.activeInHierarchy) { return;}
        visitorCam.gameObject.SetActive(true);
        _mainCamera.SetActive(false);
    }
}
