using UnityEngine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{   
    enum VirtualCameras
    {   
        NoCamera = -1,
        CockpitCamera = 0,
        FollowCamera
    }

    [SerializeField] List<GameObject> _virtualCameras;

    VirtualCameras CameraKeyPressed
    {
        get
        {
            for (int i = 0; i < _virtualCameras.Count; ++i)
            {
               if (Input.GetKeyDown(KeyCode.Alpha1 + i)) return (VirtualCameras)i;
               
            }

            return VirtualCameras.NoCamera;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetActiveCamera(VirtualCameras.CockpitCamera);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update Running");
        SetActiveCamera(CameraKeyPressed);
    }
    void SetActiveCamera(VirtualCameras activeCamera)
    {
        if (activeCamera == VirtualCameras.NoCamera)
        {
            Debug.Log("No Camera");
            return;
        }
        Debug.Log($"${activeCamera.ToString()}");
        foreach (GameObject cam in _virtualCameras)
        {
           cam.SetActive(cam.tag.Equals(activeCamera.ToString()));
        }
    }
}
