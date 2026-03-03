using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CameraManager cameraManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (cameraManager.currentCam == cameraManager.chopCamera)
                cameraManager.SwitchCamera(cameraManager.crocCamera);
            else if (cameraManager.currentCam == cameraManager.crocCamera)
                cameraManager.SwitchCamera(cameraManager.chopCamera);
        }
    }
}