using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Movement : NetworkBehaviour
{
    private CharacterController _cc;
    private Animator _animator;
    public float sensitivityX;
    public float sensitivityY;
    public Transform eyeTransform;
    private float XRotation;
    public float speedMultiplier;
    public float shotgunRange;
    public GameObject bulletHolePrefab;
    public float timeBetweenShots = 0.3333f;
    private float timestamp;
    public float sprintMultiplier;
    public int JumpDuration;
    public float jumpMultiplier;
    public float gravityStrength;
    private float currentFallSpeed = 0;
    public float damage;
    public Transform RaycastOrigin;
    private GunSettings _gunSettings;
    public Animator gunAnimator;
    private audioManager _audioManager;
    public Animator CameraAnimator;
    public GameObject model;
    public GameObject viewModel;



    
    //Lets hope this works
    // Start is called before the first frame update
    void Start()
    {

        if(isLocalPlayer)
        {
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.SetParent(eyeTransform,false);
            model.SetActive(false);
            
            

        }
        if(!isLocalPlayer)
        {
            viewModel.SetActive(false);
        }
        _cc = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        XRotation = eyeTransform.rotation.eulerAngles.x;
        _gunSettings = GetComponentInChildren<GunSettings>();
        _audioManager = GetComponentInChildren<audioManager>();
        _audioManager.PlayEffects();


    }

    // Update is called once per frame
    void Update()
    {
        
        //Movement

        if(!isLocalPlayer)
        {
            return;
        }
        Vector3 inputs = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _cc.Move(transform.rotation * inputs * Time.deltaTime * sprintMultiplier);
        }
        else
        {
            _cc.Move(transform.rotation * inputs * Time.deltaTime * speedMultiplier);
        }
        
        float speed = _cc.velocity.magnitude;
        if (_animator != null)
        {
            _animator.SetFloat("speed", speed);
        }
        if(Input.GetKey(KeyCode.C))
        {
            CameraAnimator.SetBool("isCrouching", true);
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            CameraAnimator.SetBool("isCrouching", false);
        }
        
        //mouse movement
        float mousex = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX;
        float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityY * -1;
        transform.Rotate(0, mousex, 0);
        XRotation += mousey;
        XRotation = Mathf.Clamp(XRotation, -90, 90);
        eyeTransform.localRotation = Quaternion.Euler(XRotation, 0, 0);


        //Shooting
        if (Time.time >= timestamp && Input.GetMouseButton(0))
        {
            _gunSettings.PlayEffects();
            RaycastHit hit;
            if(Physics.Raycast(eyeTransform.position, eyeTransform.forward, out hit, shotgunRange))
            {

                GameObject bulletHole = Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
                bulletHole.transform.parent = hit.collider.transform;
                timestamp = Time.time + timeBetweenShots;
                enemyCollider enemyCollider = hit.collider.GetComponent<enemyCollider>();
                if(enemyCollider != null)
                {
                    enemyCollider.takeDamage(damage);
                    _gunSettings.PlayHitEffects(enemyCollider.thisColliderType);
                }

            }

        }

        if(Input.GetButtonDown("Fire2"))
        {
            gunAnimator.SetTrigger("Aiming");
        }

        if(Input.GetButtonUp("Fire2"))
        {
            gunAnimator.SetTrigger("Hip");
        }

        //Jumping
        if(Physics.Raycast(RaycastOrigin.position, Vector3.down, 0.4f, ~(1<<8)))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(JumpCoroutine());
               
            }
            currentFallSpeed = 0;
        }
        else
        {
            _cc.Move(new Vector3(0, gravityStrength * currentFallSpeed, 0) * Time.deltaTime);
            currentFallSpeed += Time.deltaTime;
        }
        

    }

    IEnumerator JumpCoroutine()
    {
        for (int i = 0; i < JumpDuration; i++)
        {
            _cc.Move(new Vector3(0, (JumpDuration) - i, 0) * Time.deltaTime * jumpMultiplier);
            //Wait one frame
            yield return null;
        }
        
    }

    //Collide with church collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Debug.Log("Church Scene");
            SceneManager.LoadScene(1);
        }
        if(other.gameObject.layer == 10)
        {
            Debug.Log("Tavern Scene");
            SceneManager.LoadScene(2);
        }
    }
}
