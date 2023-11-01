using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PuzzleMechanism : NetworkBehaviour
{
    public Button repairButton;
    public GameObject wall;
    [SyncVar] private bool playerInRange;
    //[SyncVar] private bool wallup = false;

    [SyncVar]public int startMinutes = 5;
    [SyncVar]public int startSeconds = 00;
    public TextMeshProUGUI countdownText;
    public GameObject QuestLog;
    [SyncVar] public bool hasDetectedPlayer = false;

    public void Start()
    {
        if (isLocalPlayer)
        {
        repairButton = GameObject.Find("UI_Mobile").transform.GetChild(5).gameObject.GetComponent<Button>();
        repairButton.onClick.AddListener(OnClickRepairButton);
        QuestLog = GameObject.Find("Quest Log");
        QuestLog.SetActive(false);
        wall = GameObject.Find("Wall");
        wall.SetActive(false);
        }

    }

    public void Update()
    {
        if (isLocalPlayer)
        {
            //if (wallup == true)
            //{
            //    Walls_Up();
            //}

            Player_detection();
        }

        
    }

    public void OnClickRepairButton()
    {
        if (isLocalPlayer)
        {
            //wallup = true;
            CmdClickRepairButton(QuestLog, startMinutes, startSeconds);
            repairButton.gameObject.SetActive(false); // hide the button
        }
    }


    [Command(requiresAuthority =false)]
    public void CmdClickRepairButton(GameObject QuestLog, int startMinutes, int startSeconds)
    {
        RpcClickRepairButton(QuestLog, startMinutes, startSeconds);
        //StartCoroutine(DoCountdown(startMinutes, startSeconds));
    }

    [ClientRpc]
    public void RpcClickRepairButton(GameObject QuestLog, int startMinutes, int startSeconds)
    {
        QuestLog.SetActive(true);
    }

    //public void Walls_Up()
    //{
    //    wall.SetActive(true);
    //    wallup = false;
    //}

    public void Player_detection()
    {
        if (!hasDetectedPlayer)
        {
            playerInRange = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("tower"))
                {
                    playerInRange = true;
                    hasDetectedPlayer = true;
                    break;
                }
            }
            repairButton.gameObject.SetActive(playerInRange);
        }
    }

    //IEnumerator DoCountdown(int minutes, int seconds)
    //{
    //    int totalSeconds = (minutes * 60) + seconds;

    //    while (totalSeconds > 0)
    //    {
    //        int remainingMinutes = totalSeconds / 60;
    //        int remainingSeconds = totalSeconds % 60;
    //        countdownText.text = remainingMinutes.ToString("D2") + ":" + remainingSeconds.ToString("D2");
    //        yield return new WaitForSeconds(1f);
    //        totalSeconds--;
    //    }

    //    countdownText.text = "Countdown Complete!";
    //}

}
