using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

/// <summary>
/// 1st_Person controller test
/// 
/// 
/// Akarisu
/// </summary>
public class Player_Controller_1st_Person : Singleton<Player_Controller_1st_Person>
{
    [SerializeField] private CharacterController _cc;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _targetCamera;
    [SerializeField] private GameObject _light;
    [SerializeField] private GameObject _pointLight;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _flashlightAnimator;

    // inputs
    private InputManager _inputs;

    private void Awake()
    {
        _inputs = InputManager.Instance;
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        RegisterInput();
    }

    private void Start()
    {

    }

    private void Update()
    {
        // everything related to water
        CheckWaterHeight();

        // D�placement du joueur (zqsd + sprint)
        CcMove();

        // gravit�
        GravityHandler();

        // Lumi�re
        if (_enabledLight)
            RayOfLight();

        // interactable
        Detect();
    }

    private void FixedUpdate()
    {

    }

    private void OnDisable()
    {
        UnregisterInput();
    }

    private void OnDestroy()
    {

    }

    #region Sub

    #region Input

    private void RegisterInput()
    {
        _inputs.OnStartRun += RunPress;
        _inputs.OnEndRun += RunCancel;

        _inputs.OnStartLight += FlipFlopLight;

        _inputs.OnStartOption += Pause;

        _inputs.OnStartInteracte += Interacte;
    }
    private void UnregisterInput()
    {
        _inputs.OnStartRun -= RunPress;
        _inputs.OnEndRun -= RunCancel;

        _inputs.OnStartLight -= FlipFlopLight;

        _inputs.OnStartOption -= Pause;

        _inputs.OnStartInteracte -= Interacte;
    }

    #endregion

    void Pause() => OptionsInGame.Instance.CallOption();

    #region cc move

    [Space(5), Header("Move & Run")]
    [SerializeField] private float speedCalculated;
    [Range(1f, 5f)][SerializeField] private float speedOutWater;
    [Range(1f, 5f)][SerializeField] private float speedInWater;
    [SerializeField] private float RunSpeedMultiply = 1.5f;
    [SerializeField] private bool IsRunning;
    private const float Rad2Deg = 57.29578f;
    void CcMove() 
    {
        _targetCamera.transform.rotation = _camera.transform.rotation;

        Vector2 _move = _inputs.GetPlayerMovement();

        Vector3 direction = new Vector3(_move.x, 0, _move.y).normalized;

        if (isUnderWater)
            speedCalculated = speedInWater;
        else
            speedCalculated = speedOutWater;

        if (IsRunning)
            speedCalculated *= RunSpeedMultiply;

        if (direction.magnitude >= 0.1f)
        {
            if (!_playerAnimator.GetBool("Move"))
                _playerAnimator.SetBool("Move", true);
            if (!_flashlightAnimator.GetBool("Move"))
                _flashlightAnimator.SetBool("Move", true);

            float targetAngle = _camera.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Vector3.zero;
            moveDir = transform.forward * direction.z + transform.right * direction.x;

            if (!IsRunning)
            {
                _cc.Move(speedCalculated * Time.deltaTime * moveDir);
                _playerAnimator.speed = (speedOutWater / speedInWater) * direction.magnitude;
                _flashlightAnimator.speed = (speedOutWater / speedInWater) * direction.magnitude;
            }
            else
            {
                if (direction.z > 0)
                {
                    _cc.Move(speedCalculated * Time.deltaTime * moveDir);
                    _playerAnimator.speed = (speedOutWater / speedInWater) * direction.magnitude;
                    _flashlightAnimator.speed = (speedOutWater / speedInWater) * direction.magnitude;
                }
                else 
                { 
                    _cc.Move(speedCalculated * Time.deltaTime * moveDir);
                    _playerAnimator.speed = direction.magnitude;
                    _flashlightAnimator.speed = direction.magnitude;
                }
            }
        }
        else
        {
            if (_playerAnimator.GetBool("Move"))
                _playerAnimator.SetBool("Move", false);
            if (_flashlightAnimator.GetBool("Move"))
                _flashlightAnimator.SetBool("Move", false);
        }
    }

    void RunPress() 
    {
        IsRunning = true;
    }

    void RunCancel()
    {
        IsRunning = false;
    }

    #endregion

    #region Gravit�

    // information de la gravit�
    [Space(5), Header("Gravity")]
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float gravity = 30f;
    void GravityHandler() 
    {
        // Appliquer la gravit�
        if (_cc.isGrounded)
            moveDirection.y = 0f;
        else 
            moveDirection.y -= Time.deltaTime * gravity;

        // Appliquer le mouvement
        _cc.Move(Time.deltaTime * moveDirection);
    }

    #endregion

    #region light

    [Space(5), Header("Light")]

    [SerializeField] private LayerMask _lightLayerDetection;
    [SerializeField] private bool _enabledLight = true;
    [SerializeField] private AudioSource lightSource;
    [SerializeField] private AudioClip lightButtonSound;

    void FlipFlopLight()
    {
        if(_enabledLight) DisableLight();
        else EnableLight();
        FindObjectOfType<AudioManager>().PlaySound("FlashLightSwitch");
    }

    /// <summary>
    /// Active la lumi�re du joueur.
    /// </summary>
    public void EnableLight() 
    { 
        _enabledLight = true; 
        _light.gameObject.SetActive(true); 
        //lightSource.PlayOneShot(lightButtonSound); 
    }

    /// <summary>
    /// Desactive la lumi�re du joueur.
    /// </summary>
    public void DisableLight()
    {
        _enabledLight = false;
        _light.gameObject.SetActive(false);
        //lightSource.PlayOneShot(lightButtonSound);
    }


    // tire un raycast tout droit quand la lampe et activer
    public void RayOfLight()
    {
        RaycastHit hit;
        if (Physics.Raycast(_pointLight.transform.position, _pointLight.transform.forward, out hit, 25f, _lightLayerDetection))
        {
            Debug.DrawRay(_pointLight.transform.position, _pointLight.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log($"Did hit {hit.collider.name}");
            if(hit.collider.TryGetComponent<EntityBrain>(out EntityBrain brain))
            {
                if(brain.IsThePlayerGettingChase) 
                brain.entityPathfing.navMeshAgent.speed -= 0.7f;
            }
        }
        else
        {
            Debug.DrawRay(_pointLight.transform.position, _pointLight.transform.TransformDirection(Vector3.forward) * 25f, Color.red);
            Debug.Log("Didn't hit");
        }
    }

    #endregion

    #region water

    [Space(5), Header("Water")]

    [SerializeField] private bool isUnderWater;
    [SerializeField] private float waterHeigth;
    [SerializeField] private Volume volume;
    [SerializeField] private VolumeProfile normalProfile;
    [SerializeField] private VolumeProfile underWaterProfile;

    void CheckWaterHeight()
    {
        if (transform.position.y < waterHeigth)
        {
            if (volume.profile != underWaterProfile)
                volume.profile = underWaterProfile;
            if(isUnderWater != true){
                FindObjectOfType<AudioManager>().ResumeSound("InWater");
                FindObjectOfType<AudioManager>().getSound("Ambiance1").pitch = 0.6f;
                FindObjectOfType<AudioManager>().getSound("Ambiance1").volume = 0.6f;
            }
            isUnderWater = true;
        }
        else 
        {
            if (volume.profile != normalProfile)
                volume.profile = normalProfile;
            if(isUnderWater == true){
                FindObjectOfType<AudioManager>().PauseSound("InWater");
                FindObjectOfType<AudioManager>().getSound("Ambiance1").pitch = 0.9f;
                FindObjectOfType<AudioManager>().getSound("Ambiance1").volume = 0.8f;
            }
            isUnderWater = false;
        }
    }

    #endregion

    #region Interactable

    [Space(5), Header("Interaction")]

    [SerializeField] private float maxDistanceInteractable;

    void Interacte()
    {
        if (hitInteracte.collider == null) return;

        if (hitInteracte.collider.gameObject.tag == "Generator")
        {
            Generator _generator = hitInteracte.collider.GetComponent<Generator>();
            if (_generator.isOn) return;
            _generator.On();
            _generator.door.generatorManager.ValidateStep();
            //_generator.door.generatorManager.SpawnCurrentStep();
            _generator.door.VerifyAllGenerators();
            hitInteracte.collider.gameObject.layer = 0;
            Debug.Log($"layer {hitInteracte.collider.gameObject.layer}");
        }

        if (hitInteracte.collider.gameObject.tag == "Door")
        {
            hitInteracte.collider.GetComponent<DoorCollider>().door.Exit();
        }
    }


    RaycastHit hitInteracte;
    [SerializeField] private Material materialOutline;
    [SerializeField] private Material OldMaterial;
    [SerializeField] private GameObject OldInteractableObject;

    [SerializeField] private LayerMask layerMaskInteractable;

    public void Detect()
    {
        Vector3 cameraPosition = _camera.transform.position;
        Vector3 cameraForward = _camera.transform.forward;


        if (Physics.Raycast(cameraPosition, cameraForward, out hitInteracte, maxDistanceInteractable, layerMaskInteractable))
        {
            GameObject hitGO = hitInteracte.collider.gameObject;
            MeshRenderer mm = hitGO.GetComponent<MeshRenderer>();

            Debug.Log($" hit {hitGO.name}");


            if (OldInteractableObject != hitGO)
            {
                OldInteractableObject = hitGO;
                if (mm.material == materialOutline) return;

                OldMaterial = hitGO.GetComponent<MeshRenderer>().material;

                Material[] materials = new Material[2];
                materials[0] = hitGO.GetComponent<MeshRenderer>().material;
                materials[1] = materialOutline;

                hitGO.GetComponent<MeshRenderer>().materials = materials;

                Debug.Log("Modify material");
            }

            Debug.DrawRay(cameraPosition, cameraForward * maxDistanceInteractable, Color.green);
        }
        else
        {
            Debug.DrawRay(cameraPosition, cameraForward * maxDistanceInteractable, Color.red);

            Debug.Log(" hit nothing");

            if (OldInteractableObject == null) return;

            Material[] materials = new Material[1];
            materials[0] = OldInteractableObject.GetComponent<MeshRenderer>().materials[0];
            OldInteractableObject.GetComponent<MeshRenderer>().materials = materials;

            OldInteractableObject = null;

            Debug.Log("Remove material");
        }
    }

    #endregion

    #endregion
}