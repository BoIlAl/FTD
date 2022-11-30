using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private Transform cam;

    protected void LateUpdate()
    {
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.back,
                cam.transform.rotation * Vector3.up);
        }
    }

    public void SetCam(Transform newCam)
    {
        cam = newCam;
    }
}
