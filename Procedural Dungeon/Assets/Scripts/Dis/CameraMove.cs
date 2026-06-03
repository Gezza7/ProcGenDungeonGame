using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    private bool isLocked;
    float xRotation = 0f;
    float yRotation = 0f;
    public Vector2 look;
    public GameObject player;
    public GameObject playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = look.x * mouseSensitivity * Time.deltaTime;
        float mouseY = look.y * mouseSensitivity * Time.deltaTime;

        // Rotate camera 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // prevent flipping
        yRotation += mouseX;
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        player.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f); 

        look = new Vector2(0, 0);
        Camera.main.transform.position = player.transform.position;
    }
}
