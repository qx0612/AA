using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform hospitalSpawn; // place where the player spawns after walking into the hospital

    public GameObject pauseBtn; // pause button
    public GameObject menu; // menu that appears after clicking on the pause button

    testScene1 test; // testScene1 script

    public static GameObject player; // store the player game object
    public static Vector3 currentPosition; // store the current position of the player
    public static int sceneCounter; // store the sceneCounter as integer so that when player completes a mini game and comes back to the main scene, the position of the player would be the same as where the player was last at before the minigame.

    GameObject obj; //npc game object

    public GameObject dialogueM; // get the dialogue manager game object
    private DialogueManager dm; // get the DialogueManager script

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

    private DialogueTrigger dt; // get DialogueTrigger script

    public GameObject Receptionist1; // get the receptionist game object
    public GameObject Pharmacist1; // get the pharmacist game object
    public GameObject Doctor1; // get the doctor game object
    public GameObject Surgeon1; // get the surgeon game object

    public GameObject TDPanel; // panel that allow player to start tower defense game

    public GameObject leftBtn; // left arrow button for movement of character
    public GameObject rightBtn; // right arrow button for movement of character
    public GameObject upBtn; // up arrow button for movement of character
    public GameObject downBtn; // down arrow button for movement of character

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // find player game object
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z); // set player position to be the position of the player

        TDPanel.SetActive(false); // disable tower defense trigger panel
        menu.SetActive(false); // disable menu to show at start

        dm = dialogueM.GetComponent<DialogueManager>(); // get the DialogueManager component of the dialogueM game object
        test = GetComponent<testScene1>(); // get the testScene1 script

        if (Time.timeScale == 0) // if game is paused
        {
            Time.timeScale = 1; // unpause the game
        }

        if (sceneCounter == 2) // after match 3
        {
            player.transform.position = currentPosition; // set player position to be the last position the player was at before going into the match 3 scene
            sceneCounter = 0; // reset the counter to 0

            dt = Receptionist1.GetComponent<DialogueTrigger>();
            // win match 3 game, change the dialogue of the receptionist
            dt.dialogue.sentences[0] = "Do you want to be a doctor?";
            dt.dialogue.sentences[1] = "Walk around the hub.";
            dt.dialogue.sentences[2] = "Talk to a Surgeon to find out more.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkObj(); // function to check for the npc that the player clicks

        if (obj != null) // if the npc is not null
        {
            if (obj.name == "Receptionist") // if npc name is Receptionist
            {
                Receptionist(); // call the Receptionist function
            }
            if (obj.name == "Pharmacist") // if npc name is Pharmacist
            {
                Pharmacist(); // call the Pharmacist function
            }
            if (obj.name == "Doctor") // if npc name is Doctor
            {
                Doctor(); // call the Doctor function
            }
            if (obj.name == "Surgeon") // if npc name is Surgeon
            {
                Surgeon(); // call the Surgeon function
            }
            if (obj.name == "NPC_Dad") // if npc name is NPC_Dad
            {
                NPCDAD(); // call the NPCDAD function
            }
            if (obj.name == "NPC_MH") // if npc name is NPC_MH
            {
                NPCMALCOLM(); // call the NPCMALCOLM function
            }
            if (obj.name == "NPC_BFF") // if npc name is NPC_BFF
            {
                NPCBFF(); // call the NPCBFF function
            }
            if (obj.name == "NPC_NQX") // if npc name is NPC_NQX
            {
                NPCNQX(); // call the NPCNQX function
            }
            if (obj.name == "NPC_SGL") // if the npc name is NPC_SGL
            {
                NPCSGL(); // call the NPCSGL function
            }
            if (obj.name == "NPC_TZD") // if the npc name is NPC_TZD
            {
                NPCTZD();  // call the NPCTZD function
            }
            if (obj.name == "NPC_JKY")  // if the npc name is NPC_JKY
            {
                NPCJKY(); // call the NPCJKY function
            }
            if (obj.name == "NPC_Aunty") // if the npc name is NPC_Aunty
            {
                NPCAUNTY(); // call the NPCAUNTY function
            }
            if (obj.name == "NPC_Lawyer") // if the npc name is NPC_Lawyer
            {
                NPCLAWYER(); // call the NPCLAWYER function
            }
        }
        // change npc exclusives dialogue box to trigger everytime instead of one

        if (dm.spawned == true) // if dialogue box is showned
        {
            // disable movement buttons from showing on the screen 
            leftBtn.SetActive(false); 
            rightBtn.SetActive(false);
            downBtn.SetActive(false);
            upBtn.SetActive(false);
        }

        if (dm.spawned == false) // if dialogue box is not showned
        {
            // enable movement buttons to be shown on the screen 
            leftBtn.SetActive(true);
            rightBtn.SetActive(true);
            downBtn.SetActive(true);
            upBtn.SetActive(true);
        }
    }

    void checkObj() // function to check if player has clicked on the npc
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
        if (receptionistStage == 0) // receptionist stage 0
        {
            // dialogue to go doctor's office
            obj.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger the dialogue for the receptionist npc
            receptionistStage = 1; // after talking to the receptionist, the receptionist stage goes to 1
        }
    
        if (receptionistStage == 1) 
        {
            // do nothing
        }

        if (pharmacistStage == 2 && receptionistStage == 2) // if player has completed the match 3 mini game
        {
            dt.TriggerDialogue(); // trigger the dialogue for that npc after the player talks to the npc after completing the match 3 mini game
            receptionistStage = 3; // receptionist stage 3
        }
    }

    void Pharmacist()
    {
        if (receptionistStage == 1 && doctorStage == 1 && pharmacistStage == 0) // if player has talked to the receptionist and doctor at least once
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger the dialogue for the pharmacist to start the match 3 mini game
            pharmacistStage = 1; // pharmacist stage 1 ( player done talking to the pharmacist )
        }
        if (pharmacistStage == 1 && dm.spawned == false) // if pharmacist stage 1 and dialogue is over
        {
            currentPosition = player.transform.position; // get current position of the player
            sceneCounter = 2; // set scenecounter to 2 to change to next scene
            test.Load(); // trigger match 3 game
        }
        if (pharmacistStage == 2 && receptionistStage == 2) // if player wins match 3 game
        {
            // Do nothing
        }
    }

    void Doctor()
    {
        if (doctorStage == 0 && receptionistStage == 1) // if player has talked to the receptionist before finding the doctor
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger doctor dialogue to ask player to go to pharmacist
            doctorStage = 1; // doctor stage 1
        }
    }

    void Surgeon()
    {
        if (surgeonStage == 1 && dm.spawned == false)  // if surgeon dialogue showned
        {
            TDPanel.SetActive(true); // show the option to start tower defense mini game
            obj = null; 
            currentPosition = player.transform.position; // get current position of the player
            sceneCounter = 2; // set scenecounter to 2 to change scene
        }

        if (receptionistStage == 3 && surgeonStage == 0 && dm.spawned == false) // if player has completed match 3 game and has talked to the receptionist stage 2
        {
            // trigger tower defense if player clicks yes button
            obj.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger surgeon dialogue before showing panel to start tower defense game
            surgeonStage = 1; // surgeon stage 1
        }
    }

    public void NPCDAD() // function to show npc dad dialogue
    {
        if (npcdadStage == 0) 
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcdadStage = 1;
        }
    }

    public void NPCMALCOLM() // function to show npc malcolm dialogue
    {
        if (npcmalStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcmalStage = 1;
        }
    }

    public void NPCBFF() // function to show npc bff dialogue
    {
        if (npcbffStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcbffStage = 1;
        }
    }

    public void NPCNQX() // function to show npc nqx dialogue
    {
        if (npcnqxStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcnqxStage = 1;
        }
    }

    public void NPCSGL() // function to show npc sgl dialogue
    {
        if (npcseanStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcseanStage = 1;
        }
    }

    public void NPCTZD() // function to show npc tzd dialogue
    {
        if (npctimStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npctimStage = 1;
        }
    }

    public void NPCJKY() // function to show npc jky dialogue
    {
        if (npcjunoStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcjunoStage = 1;
        }
    }

    public void NPCAUNTY() // function to show npc aunty dialogue
    {
        if (npcauntyStage == 0)
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue();
            npcauntyStage = 1;
        }
    }

    public void NPCLAWYER() // function to show npc lawyer dialogue
    {
        if (npclawyerStage == 1) // if player completed both match 3 and tower defense minigame
        {
            obj.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger lawyer dialogue
            npclawyerStage = 2; // lawyer stage 2 
        }

        if (npclawyerStage == 2 && dm.spawned == false) // if lawyer finished dialogue
        {
            currentPosition = player.transform.position; // get current position of player
            sceneCounter = 2; // set scenecounter to 2 to change scene
            SceneManager.LoadScene(11); // trigger endless runner game
        }
    }

    public void StartTD() // function to start tower defense game
    {
        surgeonStage = 2;
        currentPosition = player.transform.position;
        sceneCounter = 2;
        SceneManager.LoadScene(9); // tower defense
    }

    public void RetriggerSurgeon() // function to retrigger surgeon if player clicks no
    {
        TDPanel.SetActive(false);
        surgeonStage = 0;
    }

    public void PauseGame() // functio to pause the game if player clicks on the pause button
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        menu.SetActive(true);
    }

    public void UnpauseGame() // function to unpause the game if player clicks on the resume button
    {
        Time.timeScale = 1;
        pauseBtn.SetActive(true);
        menu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit(); // quit the game instantly
    }
}
