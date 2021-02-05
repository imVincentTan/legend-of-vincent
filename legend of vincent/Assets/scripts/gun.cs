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
    public float shootCooldown = 0.1f;
    private float lastShot = 0f;

    // HUD 
    public GameObject bulletAmount;

    // audio
    public AudioSource audioSource;
    public AudioClip biuSound;

    // animation
    public Animator anim;

    // pause game 
    public PauseMenuController pauseMenuController;


    // Start is called before the first frame update
    void Start(){
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauseMenuController.gamePaused){
            // shooting
            if (Input.GetButton("Fire1")){
                Shoot();
            }else if (Input.GetButton("Reload")){
                Reload();
            }

            // check if reloading done
            if(reloading && Time.time > reloadFinish){
                reloading = false;
                currentBullets = maxBullets;
                bulletAmount.GetComponent<UnityEngine.UI.Text>().text = currentBullets.ToString();
            }
        }
    }

    void Shoot(){
        if(!reloading && (Time.time >= lastShot + shootCooldown)){
            
            anim.Play("gun_shoot",-1,0f);
            audioSource.PlayOneShot(biuSound);
            lastShot = Time.time;

            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
                target target = hit.transform.GetComponent<target>();
                if (target != null) target.takeDamage(damage);
            }

            currentBullets -= 1;

            if (currentBullets <= 0){
                Reload();

            }else{
                bulletAmount.GetComponent<UnityEngine.UI.Text>().text = currentBullets.ToString();
            }    
        }   
    }

    void Reload(){
        anim.Play("gun_reload",-1,0f);
        reloading = true;
        bulletAmount.GetComponent<UnityEngine.UI.Text>().text = "Reloading...";
        reloadFinish = Time.time + reloadTime;
    }
}
