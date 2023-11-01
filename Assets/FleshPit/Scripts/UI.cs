using UnityEngine;
using Button = UnityEngine.UI.Button;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using Mirror;
using RehtseStudio.FreeLookCamera3rdPersonCharacterController.Scripts;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    public Button Skill1_Button;
    public Button Skill2_Button;
    public Image fillImage;
    public Image fillImage_ULT;
    public GameObject inventory;
    public GameObject store;
    public NetworkManagerFleshPit networkManagerFleshPit;
    public CooldownManager cooldownManager;
    public float Skill1CooldownTime = 2f;
    public bool Cooldown_Skill1_omar = false;
    public bool Cooldown_Skill1_younes = false;
    public bool Cooldown_Skill1_oubeid = false;
    public bool Cooldown_Skill1_rami = false;

    public Sprite Sprite_omar;
    public Sprite Sprite_younes;
    public Sprite Sprite_oubeid;
    public Sprite Sprite_rami;  
    
    public Sprite Sprite_omar2;
    public Sprite Sprite_younes2;
    public Sprite Sprite_oubeid2;
    public Sprite Sprite_rami2;


    // InvV3 variables 
    public Camera mainCamera; // Assign the main camera in the Inspector window
    public Camera additiveCamera; // Assign the additive scene camera in the Inspector window
    public NetworkManager networkManager;
    private bool isInventoryVisible = false;
    private bool isStoreVisible = false;


    public void Start()
    {

        Debug.Log("UI started");
        networkManagerFleshPit = FindObjectOfType<NetworkManagerFleshPit>();
        networkManager = FindObjectOfType<NetworkManager>();
    }


    public void Update()
    {

        if (networkManagerFleshPit.addplayer)
        {
            Scene_Skills_Changer();
            networkManagerFleshPit.addplayer = false;
        }
        Cooldown_Visual();
    }

    public void Scene_Skills_Changer()
    {
        PlayerControllerWithFreeLookCamera playerController = NetworkClient.localPlayer.GetComponent<PlayerControllerWithFreeLookCamera>();

        cooldownManager = new CooldownManager();

        if (NetworkClient.localPlayer == null)
        {
            return;
        }
        
        if (NetworkClient.localPlayer.gameObject.name == "Omar(Clone)")
        {
            // Execute code for ObjectName1
            Skill1_Button.onClick.AddListener(() => OnButtonClick("Skill1_Omar", Skill1CooldownTime));
            Skill2_Button.onClick.AddListener(NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Skill2_Omar);
            fillImage.sprite = Sprite_omar;
            fillImage_ULT.sprite = Sprite_omar2;
        }
        else if (NetworkClient.localPlayer.gameObject.name == "Younes(Clone)")
        {
            // Execute code for ObjectName2
            Skill1_Button.onClick.AddListener(() => OnButtonClick("Skill1_Younes", Skill1CooldownTime + 3f));
            Skill2_Button.onClick.AddListener(NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Skill2_Younes);
            fillImage.sprite = Sprite_younes;
            fillImage_ULT.sprite = Sprite_younes2;

        }
        else if (NetworkClient.localPlayer.gameObject.name == "Oubeid(Clone)")
        {
            // Execute code for ObjectName3
            Skill1_Button.onClick.AddListener(() => OnButtonClick("Skill1_Oubeid", Skill1CooldownTime + 4f));
            Skill2_Button.onClick.AddListener(NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Skill2_Oubeid);
            fillImage.sprite = Sprite_oubeid;
            fillImage_ULT.sprite = Sprite_oubeid2;

        }
        else if (NetworkClient.localPlayer.gameObject.name == "Rami(Clone)")
        {
            // Execute code for ObjectName4
            Skill1_Button.onClick.AddListener(() => OnButtonClick("Skill1_Rami", Skill1CooldownTime + 8f));
            Skill2_Button.onClick.AddListener(NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Skill2_Rami);
            fillImage.sprite = Sprite_rami;
            fillImage_ULT.sprite = Sprite_rami2;

        }
        else
        {
            // Code to execute if the object name is not recognized
            Debug.Log("wakaranai");
        }
    }


    private void OnButtonClick(string functionName, float cooldownTime)
    {
        if (!cooldownManager.IsOnCooldown(functionName))
        {
            if (functionName == "Skill1_Omar")
            {
                ResetSkill1_omar(); // Set Cooldown_Skill1_omar to false before invoking Skill1_Omar
            }

            if (functionName == "Skill1_Younes")
            {
                ResetSkill1_younes(); // Set Cooldown_Skill1_younes to false before invoking Skill1_Younes

            }

            if (functionName == "Skill1_Oubeid")
            {
                ResetSkill1_oubeid(); // Set Cooldown_Skill1_oubeid to false before invoking Skill1_Oubeid

            }

            if (functionName == "Skill1_Rami")
            {
                ResetSkill1_rami(); // Set Cooldown_Skill1_rami to false before invoking Skill1_Rami

            }

            NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Invoke(functionName, 0f);
            cooldownManager.StartCooldown(functionName, cooldownTime);

            if (functionName == "Skill1_Omar")
            {
                Invoke("ResetSkill1_omar", Skill1CooldownTime);
            }

            if (functionName == "Skill1_Younes")
            {
                Invoke("ResetSkill1_younes", Skill1CooldownTime + 3f);

            }

            if (functionName == "Skill1_Oubeid")
            {
                Invoke("ResetSkill1_oubeid", Skill1CooldownTime + 4f);

            }

            if (functionName == "Skill1_Rami")
            {
                Invoke("ResetSkill1_rami", Skill1CooldownTime + 8f);

            }
        }
    }

    private void ResetSkill1_omar()
    {
        Cooldown_Skill1_omar = false;
    }

    private void ResetSkill1_younes()
    {
        Cooldown_Skill1_younes = false;

    }

    private void ResetSkill1_oubeid()
    {
        Cooldown_Skill1_oubeid = false;

    }
    
    private void ResetSkill1_rami()
    {
        Cooldown_Skill1_rami = false;

    }



    public class CooldownManager
    {
        private Dictionary<string, float> cooldowns = new Dictionary<string, float>();

        public bool IsOnCooldown(string functionName)
        {
            return cooldowns.ContainsKey(functionName) && cooldowns[functionName] > Time.time;
        }

        public void StartCooldown(string functionName, float cooldownTime)
        {
            cooldowns[functionName] = Time.time + cooldownTime;

        }
    }

    private void Cooldown_Visual()
    {
        if (Cooldown_Skill1_omar)
        {
            fillImage.fillAmount -= 1 / Skill1CooldownTime * Time.deltaTime;

            if (fillImage.fillAmount <= 0)
            {
                fillImage.fillAmount = 0;
                Cooldown_Skill1_omar = false;
            }
            else if (fillImage.fillAmount < 0.01f)
            {
                fillImage.fillAmount = 1;
                if (fillImage.fillAmount == 1)
                {
                    Cooldown_Skill1_omar = false;
                }
            }
        }

        if (Cooldown_Skill1_younes)
        {
            fillImage.fillAmount -= 1 / (Skill1CooldownTime + 3f) * Time.deltaTime;

            if (fillImage.fillAmount <= 0)
            {
                fillImage.fillAmount = 0;
                Cooldown_Skill1_younes = false;
            }
            else if (fillImage.fillAmount < 0.01f)
            {
                fillImage.fillAmount = 1;
                if (fillImage.fillAmount == 1)
                {
                    Cooldown_Skill1_younes = false;
                }
            }
        }

        if (Cooldown_Skill1_oubeid)
        {
            fillImage.fillAmount -= 1 / (Skill1CooldownTime + 4f) * Time.deltaTime;

            if (fillImage.fillAmount <= 0)
            {
                fillImage.fillAmount = 0;
                Cooldown_Skill1_oubeid = false;
            }
            else if (fillImage.fillAmount < 0.01f)
            {
                fillImage.fillAmount = 1;
                if (fillImage.fillAmount == 1)
                {
                    Cooldown_Skill1_oubeid = false;
                }
            }
        }

        if (Cooldown_Skill1_rami)
        {
            fillImage.fillAmount -= 1 / (Skill1CooldownTime + 8f) * Time.deltaTime;

            if (fillImage.fillAmount <= 0)
            {
                fillImage.fillAmount = 0;
                Cooldown_Skill1_rami = false;
            }
            else if (fillImage.fillAmount < 0.01f)
            {
                 fillImage.fillAmount = 1;
                if (fillImage.fillAmount == 1)
                {
                    Cooldown_Skill1_rami = false;
                }
            }
        }
    }



    public void Toggle_Inventory()
    {
        if (isInventoryVisible)
        {
            inventory.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            isInventoryVisible = false;
        }
        else
        {
            inventory.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            isInventoryVisible = true;
        }
    }


    public void Toggle_Store()
    {

        if (isInventoryVisible)
        {
            store.SetActive(false);
            isStoreVisible = false;
        }
        else
        {
            store.SetActive(true);
            isStoreVisible = true;
        }

    }



}

