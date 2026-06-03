using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[,] inventory;
    public GameObject equiped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack()
    {
        Weapon weapon = equiped.GetComponent<Weapon>();
        if(weapon != null)
        {
            weapon.attack();
        }
    }
}
