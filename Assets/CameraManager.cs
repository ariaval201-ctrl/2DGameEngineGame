using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera[] cameras;

    public CinemachineCamera chopCamera;
    public CinemachineCamera crocCamera;

    public CinemachineCamera startCamera;
    public CinemachineCamera currentCam;
    public CinemachineCamera hookCamera;

    void Start()
    {
        if (startCamera == null)
            return;

        currentCam = startCamera;
        UpdatePriorities();
    }

    public void SwitchCamera(CinemachineCamera newCam)
    {
        if (newCam == null)
            return;

        currentCam = newCam;
        UpdatePriorities();
    }

    private void UpdatePriorities()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == null)
                continue;

            cameras[i].Priority = (cameras[i] == currentCam) ? 20 : 10;
        }
    }
}