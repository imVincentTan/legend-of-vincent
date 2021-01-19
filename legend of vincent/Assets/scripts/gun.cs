using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 6f;
    public float range = 100f;

    public Camera fpsCam;
    public LayerMask enemymask;

    public float maxBullets = 12f;
    public float currentBullets = 12f;
    public float reloadTime = 0.5f;
    public float reloadFinish = 0f;
    public bool reloading = false;

    // HUD 
    public GameObject bulletAmount;

    // Update is called once per frame
    void Update()
    {
        // shooting
        if (Input.GetButtonDown("Fire1")) Shoot();

        // check if reloading done
        if(reloading && Time.time > reloadFinish){
            reloading = false;
            currentBullets = maxBullets;
            bulletAmount.GetComponent<UnityEngine.UI.Text>().text = currentBullets.ToString();
        }
    }

    void Shoot(){
        if(!reloading){
            print("pew");
            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
                Debug.Log(hit.transform.name);
                target target = hit.transform.GetComponent<target>();
                if (target != null) target.takeDamage(damage);
            }

            currentBullets -= 1;

            if (currentBullets <= 0){
            
            reloading = true;
            bulletAmount.GetComponent<UnityEngine.UI.Text>().text = "Reloading...";
            reloadFinish = Time.time + reloadTime;

            }else{
                bulletAmount.GetComponent<UnityEngine.UI.Text>().text = currentBullets.ToString();
            }
        }

                
    }
}
