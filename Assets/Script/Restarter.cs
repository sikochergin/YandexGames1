using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public void Rest()
    {
        Progress.Instance.CurrentProgressData.AMlevel = 0;
        Progress.Instance.CurrentProgressData.PALevel = 0;
        Progress.Instance.CurrentProgressData.Walls = 0;
        Progress.Instance.CurrentProgressData.Money = 0;

        SceneManager.LoadScene(0);
    }
}
