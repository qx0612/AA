using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //EndGameUI match3;
    public Transform hospitalSpawn;
    //public Transform pharmacistSpawn;
    //public Transform DoctorSpawn; // to go in doctor office
    //public Transform DoctorExitSpawn; // to go out doctor office
    public GameObject pauseBtn;
    public GameObject menu;

    testScene1 test;

    public static GameObject player;
    public static Vector3 currentPosition;
    public static int sceneCounter;

    GameObject obj; //npc

    public GameObject dialogueM;
    private DialogueManager dm;

    List<string> inventory = new List<string>();

    public static int receptionistStage = 0; // 0, task player go doctor office
    public static int doctorStage = 0; // if receptionistStage = 1, go to pharmacist
    public static int pharmacistStage = 0; // if 1, trigger Match 3 ( yes btn )

    // if match 3 complete, receptionist stage = 2
    //The receptionist tells him that there is a lot of commitments involve in wanting to be a doctor and advises the player to walk around the hub and to ask more doctors and surgeons about the matter at hand.

    public static int surgeonStage = 0; // if receptionistStage = 3, the surgeon asks "what are you doing here little one?, next dialogue "you want to go to medical school?", "do you want to take a free course from me now?"
    // if player clicks ( yes btn ), trigger Tower Defense

    public static int npcdadStage = 0;
    public static int npcmalStage = 0;
    public static int npcbffStage = 0;
    public static int npcnqxStage = 0;
    public static int npctimStage = 0;
    public static int npcjunoStage = 0;
    public static int npcseanStage = 0;
    public static int npcauntyStage = 0;
    public static int npclawyerStage = 0;

    private DialogueTrigger dt;

    public GameObject Receptionist1;
    public GameObject Pharmacist1;
    public GameObject Doctor1;
    public GameObject Surgeon1;

    public GameObject TDPanel;

    public GameObject leftBtn;
    public GameObject rightBtn;
    public GameObject upBtn;
    public GameObject downBtn;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        TDPanel.SetActive(false);
        menu.SetActive(false);

        dm = dialogueM.GetComponent<DialogueManager>();
        test = GetComponent<testScene1>();

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        if (sceneCounter == 2) // after match 3
        {
            player.transform.position = currentPosition;
            sceneCounter = 0;

            dt = Receptionist1.GetComponent<DialogueTrigger>();
            // win match 3 game
            dt.dialogue.sentences[0] = "Do you want to be a doctor?";
            dt.dialogue.sentences[1] = "Walk around the hub.";
            dt.dialogue.sentences[2] = "Talk to a Surgeon to find out more.";
        }

        //if (sceneCounter == 3)
        //{
        //    Debug.Log("win tower defense");
        //    player.transform.position = currentPosition;
        //    sceneCounter = 0;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        checkObj();

        if (obj != null)
        {
            if (obj.name == "Receptionist")
            {
                Receptionist();
            }
            if (obj.name == "Pharmacist")
            {
                Pharmacist();
            }
            if (obj.name == "Doctor")
            {
                Doctor();
            }
            if (obj.name == "Surgeon")
            {
                Surgeon();
            }
            if (obj.name == "NPC_Dad")
            {
                NPCDAD();
            }
            if (obj.name == "NPC_MH")
            {
                NPCMALCOLM();
            }
            if (obj.name == "NPC_BFF")
            {
                NPCBFF();
            }
            if (obj.name == "NPC_NQX")
            {
                NPCNQX();
            }
            if (obj.name == "NPC_SGL")
            {
                NPCSGL();
            }
            if (obj.name == "NPC_TZD")
            {
                NPCTZD();
            }
            if (obj.name == "NPC_JKY")
            {
                NPCJKY();
            }
            if (obj.name == "NPC_Aunty")
            {
                NPCAUNTY();
            }
            if (obj.name == "NPC_Lawyer")
            {
                NPCLAWYER();
            }
        }
        // change npc exclusives dialogue box to trigger everytime instead of one

        if (dm.spawned == true)
        {
            leftBtn.SetActive(false);
            rightBtn.SetActive(false);
            downBtn.SetActive(false);
            upBtn.SetActive(false);
        }

        if (dm.spawned == false)
        {
            leftBtn.SetActive(true);
            rightBtn.SetActive(true);
            downBtn.SetActive(true);
            upBtn.SetActive(true);
        }
    }

    void checkObj()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == true)
            {
                obj = hit.collider.gameObject;

            }

        }
    }
    void Receptionist()
    {
        if (receptionistStage == 0)
        {
            // dialogue to go doctor's office
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            receptionistStage = 1;
        }
    
        if (receptionistStage == 1)
        {

        }

        if (pharmacistStage == 2 && receptionistStage == 2)
        {
            dt.TriggerDialogue();
            receptionistStage = 3; // after match 3
        }
    }

    void Pharmacist()
    {
        if (receptionistStage == 1 && doctorStage == 1 && pharmacistStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            pharmacistStage = 1;
        }
        if (pharmacistStage == 1 && dm.spawned == false)
        {
            currentPosition = player.transform.position;
            sceneCounter = 2;
            test.Load(); // trigger match 3 game
        }
        if (pharmacistStage == 2 && receptionistStage == 2)
        {
            // win match 3 game
        }
    }

    void Doctor()
    {
        if (doctorStage == 0 && receptionistStage == 1)
        {
            // go to pharmacist
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            doctorStage = 1;
        }
    }

    void Surgeon()
    {
        if (surgeonStage == 1 && dm.spawned == false)
        {
            TDPanel.SetActive(true);
            obj = null;
            currentPosition = player.transform.position;
            sceneCounter = 2;
        }

        if (receptionistStage == 3 && surgeonStage == 0 && dm.spawned == false)
        {
            // trigger tower defense if player clicks yes button
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            surgeonStage = 1;
        }
    }

    public void NPCDAD()
    {
        if (npcdadStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcdadStage = 1;
        }
    }

    public void NPCMALCOLM()
    {
        if (npcmalStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcmalStage = 1;
        }
    }

    public void NPCBFF()
    {
        if (npcbffStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcbffStage = 1;
        }
    }

    public void NPCNQX()
    {
        if (npcnqxStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcnqxStage = 1;
        }
    }

    public void NPCSGL()
    {
        if (npcseanStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcseanStage = 1;
        }
    }

    public void NPCTZD()
    {
        if (npctimStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npctimStage = 1;
        }
    }

    public void NPCJKY()
    {
        if (npcjunoStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcjunoStage = 1;
        }
    }

    public void NPCAUNTY()
    {
        if (npcauntyStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcauntyStage = 1;
        }
    }

    public void NPCLAWYER()
    {
        if (npclawyerStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npclawyerStage = 1;
        }

        if (npclawyerStage == 1 && dm.spawned == false)
        {
            currentPosition = player.transform.position;
            sceneCounter = 2;
            SceneManager.LoadScene(11); // trigger endless runner game
        }
    }

    public void StartTD()
    {
        surgeonStage = 2;
        currentPosition = player.transform.position;
        sceneCounter = 2;
        SceneManager.LoadScene(9); // tower defense
    }

    public void RetriggerSurgeon()
    {
        TDPanel.SetActive(false);
        surgeonStage = 0;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        menu.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseBtn.SetActive(true);
        menu.SetActive(false);
    }

}
