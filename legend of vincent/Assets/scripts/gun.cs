using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 6f;
    public float range = 100f;

    public Camera fpsCam;
    public LayerMask enemymask;

    // Update is called once per frame
    void Update()
    {
        // shooting
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    void Shoot(){
        print("pew");
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            target target = hit.transform.GetComponent<target>();
            if (target != null) target.takeDamage(damage);
        }
    }
}
