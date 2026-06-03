using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float max;
    public float tiltScale;
    public GameObject arrow;

    // Update is called once per frame
    void Update()
    {
        /*
        if(arrow.localEulerAngles.x<max)
        {
            float newAngle = tiltScale * Time.deltaTime;
            arrow.Rotate(new Vector3(-newAngle, 0, 0), Space.Self);
        }
        */
        transform.rotation = Quaternion.LookRotation(this.GetComponent<Rigidbody>().angularVelocity);
        transform.rotation = Quaternion.Euler(transform.rotation.x +90f, transform.rotation.y, transform.rotation.z);
    }
}
