using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float minX, maxX;
    public Slider cameraSlider;
    private void Start()
    {
        float percent = (transform.position.x - minX) / (maxX - minX);
        cameraSlider.value = percent;
    }

    public void SetCameraPosition(float percent)
    {
        float xPos = minX + (maxX - minX) * percent;
        transform.position = new Vector3(xPos, transform.position.y, transform.localPosition.z);
    }
}
