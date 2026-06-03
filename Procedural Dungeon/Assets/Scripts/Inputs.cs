using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public PlayerInput input;
    public InputAction screenshot;
    public string fileName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenshot = input.actions.FindAction("ScreenShot");
        screenshot.Enable();
        screenshot.performed += screenShotPerformed;
    }

    public void screenShotPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("screenshot taken");
        ScreenCapture.CaptureScreenshot(Application.dataPath +"/pictures/" +fileName+ ".png");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
