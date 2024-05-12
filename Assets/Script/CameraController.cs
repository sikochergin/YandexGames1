using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float sensitivityPanelRotate = 1; // Чувствительность мыши
    public float maxYAngle = 80.0f; // Максимальный угол вращения по вертикали
    public CameraControllerPanel cameraControllerPanel;
    private float rotationX = 0.0f;

    private void Update()
    {
        float mouseX = 0;
        float mouseY = 0;
        if (!IsMobileDevice())
        {
            if (cameraControllerPanel.pressed)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.fingerId == cameraControllerPanel.fingerId)
                    {
                        if (touch.phase == TouchPhase.Moved)
                        {
                            mouseY = touch.deltaPosition.y * sensitivityPanelRotate;
                            mouseX = touch.deltaPosition.x * sensitivityPanelRotate;
                        }

                        if (touch.phase == TouchPhase.Stationary)
                        {
                            mouseY = 0;
                            mouseX = 0;
                        }
                    }
                }
            }
        }
        else
        {
            mouseX = Input.GetAxis("Mouse X") * sensitivity;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        }

        // Вращаем персонажа в горизонтальной плоскости
        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        // Вращаем камеру в вертикальной плоскости
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }

    private bool IsMobileDevice()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            return true;
        }
        else
        {
            return false;
        }
        //    string deiveUser = YandexGame.EnvironmentData.deviceType;
        //    if (deiveUser == "mobile" || deiveUser == "table")
        //     {
        //        return true;
        //    }
        //    else
        //    {
        //       return false;
        //   }
    }
    
    
}