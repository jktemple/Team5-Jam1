using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StealthPlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public bool crouching = false;
    public bool hiding = false;
    public MeshRenderer crouchingVersion;
    MeshRenderer normalVersion;
    private Vector3 targetRot;

    public GameObject KnightObj;

    [Header("stone throwing")]
    public float horizontalBound;
    public float verticalBound;
    public RectTransform pixelRawTextureRect;
    public LineRenderer aimLine;
    public bool canThrowStones = true;
    bool aiming = false;
    public GameObject stone;
    public float forceMod = 2;

    [Header("sounds")]
    private AudioSource source;
    public int walkFootstepSoundID = 0;
    public int SneakFootstepSoundID = 1;
    public int crouchSoundID = 2;

    float horizontal;
    float vertical;

    private void Awake() {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        aimLine.enabled = false;
        normalVersion = GetComponent<MeshRenderer>();
    }

    void Update() {
        crouchingVersion.gameObject.SetActive(crouching);

        if (hiding) {
            return;
        }
        if (Input.GetKey(StealthGameManager.instance.SneakKey) && !crouching) {
            crouching = true;
            SteathAudioManager.instance.PlayHere(crouchSoundID, source, restart:true );
        }
        else if (Input.GetKeyUp(StealthGameManager.instance.SneakKey) && crouching) {
            crouching = false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (canThrowStones) {
            aimLine.enabled = aiming;

            if (Input.GetMouseButtonDown(0)) {
                aiming = true;
                aimLine.SetPosition(1, transform.position);
            }
            if (Input.GetMouseButton(0) && aiming) {  
                AimThrow();
            }
            if (Input.GetMouseButtonDown(1)) {
                aiming = false;
            }
            if (Input.GetMouseButtonUp(0)) {
                Throw();
            }
        }
    }

    void AimThrow() {
        aimLine.enabled = true;
        if (!StealthGameManager.instance.pixelShader) {
            Ray mousePosRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mousePosRay, out var hit);
            aimLine.SetPosition(0, transform.position);
            aimLine.SetPosition(1, Vector3.Lerp(aimLine.GetPosition(1), hit.point, 0.025f));
        }
        else {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(pixelRawTextureRect, Input.mousePosition, null, out Vector2 localHit);
            Vector2 viewportClick = new Vector2(localHit.x / pixelRawTextureRect.rect.xMax, localHit.y / (pixelRawTextureRect.rect.yMin *-1));
            Vector3 lineEnd = new Vector3(horizontalBound * viewportClick.x, 0, verticalBound * viewportClick.y);

            aimLine.SetPosition(0, transform.position);
            aimLine.SetPosition(1, Vector3.Lerp(aimLine.GetPosition(1), lineEnd + transform.position, 0.025f));
        }
    }

    void Throw() {
        GameObject newPebble = Instantiate(stone, transform.position, Quaternion.identity);
        Rigidbody Stonerb = newPebble.GetComponent<Rigidbody>();
        Stonerb.AddForce( (aimLine.GetPosition(0) - aimLine.GetPosition(1)) * forceMod, ForceMode.VelocityChange);
        aiming = false;
    }

    private void FixedUpdate() {
        float currentSpeed = crouching ? speed / 2 : speed;
        if (Mathf.Abs(horizontal + vertical) > 0) {
            SteathAudioManager.instance.PlayHere(crouching ? SneakFootstepSoundID : walkFootstepSoundID, source);
            KnightObj.GetComponent<Animator>().enabled = true;
        }
        else {
            KnightObj.GetComponent<Animator>().enabled = false;
            SteathAudioManager.instance.StopSoundHere(crouching ? SneakFootstepSoundID : walkFootstepSoundID, source);
        }
        rb.velocity = new Vector3(horizontal * currentSpeed, 0, vertical * currentSpeed);

        GameObject toTurn = crouching ? crouchingVersion.gameObject : KnightObj;
        if (horizontal > 0 && vertical <= 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (horizontal < 0 && vertical <= 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (vertical > 0 && horizontal <= 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        if (vertical < 0 && horizontal <= 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 270, 0);
        }

        if (horizontal > 0 && vertical < 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 225, 0); // 225
        }
        if (horizontal > 0 && vertical > 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 135, 0);
        }
        if (vertical > 0 && horizontal < 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 45, 0); //45
        }
        if (vertical < 0 && horizontal < 0) {
            toTurn.transform.localEulerAngles = new Vector3(0, 315, 0);
        }

    }


    public void Hide() {
        hiding = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public void UnHide() {
        hiding = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<CapsuleCollider>().enabled = true;
    }
}
