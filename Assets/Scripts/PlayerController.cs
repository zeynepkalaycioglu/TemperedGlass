using System;
using System.Collections;
using System.Collections.Generic;
using MiscUtil.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [SerializeField]
    public List<GameObject> leftPositions;
    public List<GameObject> rightPositions;
    public List<GameObject> winPosition;
    public List<GameObject> playerList;
     public List<float> playerYOffsetList = new List<float>();
     
     
    [SerializeField]
    private Ease animEase;


    public Rigidbody playerRb;

    public Camera winCam;
    public TemperedGlassManager temperedGlassManager;
    public int playerCount = 9;
    public GameObject player;
    public Animator playerAnimator;
    public bool isJumping = false;
   
    
    public float ButtonReactivateDelay = 2f;
    private int playerRotation = 180;

    private Vector3 _playerPos = new Vector3(0f,-0.83f,0f) ;

    public Button right, left;
    private int currentPlayer = 10;
    private int currentStep = 0;

    private bool isAdPlayed = false;

    private Vector3 velocity;

   
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponentInChildren<Rigidbody>();
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.isGameStarted)
        {
            //MouseMovement();
            //PlayerMovement();
            //Jump();
        }
    }
    public void StartGame()
    {
        //transform.position = _initialPos;
        /*playerRb.useGravity = true;
        playerRb.isKinematic = false;
        */
    }

    public void GameEnded()
    {
        player.SetActive(false);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("WrongGlass"))
        {
            right.interactable = false;
            left.interactable = false;
            GlassManager.Instance.GlassBroke();
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Crash);
            collision.transform.GetComponent<MeshRenderer>().enabled = false;
            collision.transform.GetComponent<BoxCollider>().enabled = false;
            
        } 
        if (collision.transform.CompareTag("TemperedGlass"))
        {
            GameManager.Instance.GlassPassed(collision.gameObject);
        }
        
        if (collision.transform.CompareTag("DeathGround"))
        {
            CheckPlayerCounter();
        }

    }

    public void CheckPlayerCounter()
    {
        if (playerCount == 0)
        {
            if (isAdPlayed == true)
            {
                GameManager.Instance.GameOver();
                GameEnded();
            }
            else
            {
                GameManager.Instance.PlayAd();
            }
        }
            
        if(playerCount > 0)
        {
            //player.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(WaitForDeath());
            right.interactable = true;
            left.interactable = true;
            currentStep = 0;
            GameManager.Instance.score = 0;
        }
    }
    

    IEnumerator WaitForDeath()
    {
        right.interactable = false;
        left.interactable = false;
        yield return new WaitForSeconds(2.5f);
        currentPlayer--;
        Debug.Log(currentPlayer);
        PlayerChange();
        
    }
    
    public void WhenClicked() 
    {
        right.interactable = false;
        StartCoroutine(EnableButtonAfterDelay(right, ButtonReactivateDelay));
        left.interactable = false;
        StartCoroutine(EnableButtonAfterDelay(left, ButtonReactivateDelay));

        // Do whatever else your button is supposed to do.
    }
 
    IEnumerator EnableButtonAfterDelay(Button button, float seconds) {
        yield return new WaitForSeconds(seconds);
        button.interactable = true;
    }

    public void RightTaskOnClick()
    {
        WhenClicked();
        if (playerAnimator == null)
        {
            playerAnimator = player.GetComponentInChildren<Animator>();
        }

        if (playerAnimator)
        {
            playerAnimator.SetBool("isRightJumping", true);
        }
        //player.transform.position = rightPositions[currentStep].transform.position;
        transform.DOMove(rightPositions[currentStep].transform.position, 1f);
        currentStep++;
        SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Jump);

        if (currentStep == 10)
        {
            StartCoroutine(Win());
        }
    }
    
     private void LeftTaskOnClick()
    {
        WhenClicked();
        if (playerAnimator == null)
        {
            playerAnimator = player.GetComponentInChildren<Animator>();
        }

        if (playerAnimator)
        {
             playerAnimator.SetBool("isLeftJumping", true);
        }
        // player.transform.position = leftPositions[currentStep].transform.position;
        transform.DOMove(leftPositions[currentStep].transform.position, 1f);
        currentStep++;
        SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Jump);

        if (currentStep == 10)
        {
            StartCoroutine(Win());
        }
    }

     IEnumerator Win()
    {
        yield return new WaitForSeconds(1f);
        player.transform.Rotate(0f,playerRotation,0f);
        playerAnimator.SetBool("isWinning", true);
        winCam.transform.position = new Vector3(0f,3.37f,31.7f);
        GameManager.Instance.Win();
        

    }

    public void ActivateButton()
    {
        Button rightButton = right.GetComponent<Button>();
        rightButton.onClick.AddListener(RightTaskOnClick);
        Button leftButton = left.GetComponent<Button>();   
        leftButton.onClick.AddListener(LeftTaskOnClick);
    }

    public void PlayerChange()
    {
        right.interactable = true;
        left.interactable = true;
        Destroy(player);
        player = Instantiate(playerList[currentPlayer], new Vector3(0, -0.83f, 0), Quaternion.identity, transform);
        CapsuleCollider collider = player.GetComponentInParent<CapsuleCollider>();
        collider.center = new Vector3(collider.center.x, collider.center.x - playerYOffsetList[currentPlayer], collider.center.z);
        playerAnimator = player.GetComponentInChildren<Animator>();
        player.transform.localPosition = Vector3.zero;
        transform.position = _playerPos;
    }

}
