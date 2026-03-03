using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    
    public CinemachineCamera[] cameras;

    public CinemachineCamera chopCamera;
    public CinemachineCamera crocCamera;

    public CinemachineCamera startCamera;
    public CinemachineCamera currentCam;

    private void Start()
{
    if (startCamera == null)
    {
        Debug.LogError("Start camera not assigned!");
        return;
    }

    currentCam = startCamera;

    for (int i = 0; i < cameras.Length; i++)
    {
        if (cameras[i] == null)
        {
            Debug.LogWarning($"Camera at index {i} is null!");
            continue;
        }

        cameras[i].Priority = (cameras[i] == currentCam) ? 20 : 10;
    }
}
  
   public void SwitchCamera(CinemachineCamera newCam)
{
    if (newCam == null)
    {
        Debug.LogError("Trying to switch to a null camera!");
        return;
    }

    currentCam = newCam;

    for (int i = 0; i < cameras.Length; i++)
    {
        if (cameras[i] != null)
            cameras[i].Priority = (cameras[i] == currentCam) ? 20 : 10;
    }
}
}