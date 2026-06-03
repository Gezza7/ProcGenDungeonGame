using UnityEngine;

public class Bow : Weapon
{
    public bool isEquiped = true;
    public GameObject arrow;
    public Vector3 shot;
    public Transform pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void attack()
    {
        Debug.Log("Shot Fired");
        GameObject a = Instantiate(arrow, pos.position+new Vector3(0,1f,0), Quaternion.Euler(85, 0, -pos.localEulerAngles.y));
        Rigidbody rb = a.GetComponent<Rigidbody>();
        if(rb != null)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            Vector3 shotDir = forward * shot.z + right * shot.x;

            rb.AddForce(shotDir, ForceMode.Impulse);
        }
    }
}
