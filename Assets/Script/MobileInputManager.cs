using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mobileInputContainer;
    public bool testInIspector = false;
    private bool activeContainer;
    private bool activeFireContainer = false;
    private bool mobile = false;
    private int firstCheckMobile = 0;

    public bool isMobile = false;


    public void ActiveMobileContainer()
    {
        activeContainer = !activeContainer;
        if (IsMobileDevice())
        {
            mobileInputContainer.alpha = activeContainer ? 1 : 0;
            mobileInputContainer.blocksRaycasts = activeContainer ? true : false;
            if (mobileInputContainer.blocksRaycasts)
            {
                isMobile = true;
            }
            else
            {
                isMobile = false;
            }
        }
    }


    private void Start()
    {
        // Вызывает JavaScript для проверки сенсорности устройства
        Application.ExternalEval("checkForTouchDevice()");
        ActiveMobileContainer();
    }

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();

#endif
    public bool IsMobileDevice()
    {
        if (firstCheckMobile == 1)
            return mobile;
        firstCheckMobile = 1;


        var isMobile = false;

#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif


        if (isMobile)
        {
            mobile = true;
            return true;
        }
        else
        {
            if (testInIspector)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            mobile = testInIspector;
            return testInIspector;
        }
    }
}