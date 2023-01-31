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
    }
    private void UnregisterInput()
    {
        _inputs.OnStartRun -= RunPress;
        _inputs.OnEndRun -= RunCancel;

        _inputs.OnStartLight -= FlipFlopLight;

        _inputs.OnStartOption -= Pause;
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
    [Header("Gravity")]
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

    [SerializeField] private LayerMask _lightLayerDetection;
    [SerializeField] private bool _enabledLight = true;
    [SerializeField] private AudioSource lightSource;
    [SerializeField] private AudioClip lightButtonSound;

    void FlipFlopLight()
    {
        if(_enabledLight) DisableLight();
        else EnableLight();
    }

    /// <summary>
    /// Active la lumi�re du joueur.
    /// </summary>
    public void EnableLight() 
    { 
        if (isUnderWater) return; 
        _enabledLight = true; 
        _light.gameObject.SetActive(true); 
        lightSource.PlayOneShot(lightButtonSound); 
    }

    /// <summary>
    /// Desactive la lumi�re du joueur.
    /// </summary>
    public void DisableLight()
    {
        _enabledLight = false;
        _light.gameObject.SetActive(false);
        if (isUnderWater) return;
        lightSource.PlayOneShot(lightButtonSound);
    }


    // tire un raycast tout droit quand la lampe et activer
    private void RayOfLight()
    {
        RaycastHit hit;
        if (Physics.Raycast(_pointLight.transform.position, _pointLight.transform.forward, out hit, 25f, _lightLayerDetection))
        {
            Debug.DrawRay(_pointLight.transform.position, _pointLight.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log($"Did hit {hit.collider.name}");
        }
        else
        {
            Debug.DrawRay(_pointLight.transform.position, _pointLight.transform.TransformDirection(Vector3.forward) * 25f, Color.red);
            Debug.Log("Didn't hit");
        }
    }

    #endregion

    #region water

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

            isUnderWater = true;

            if (_enabledLight)
                DisableLight();
        }
        else 
        {
            if (volume.profile != normalProfile)
                volume.profile = normalProfile;

            isUnderWater = false;
        }
    }

    #endregion

    #endregion
}