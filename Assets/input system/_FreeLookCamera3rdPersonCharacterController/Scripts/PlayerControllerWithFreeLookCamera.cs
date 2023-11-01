using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.AI;
using RehtseStudio.FreeLookCamera3rdPersonCharacterController.Scripts;
using Cinemachine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerControllerWithFreeLookCamera : NetworkBehaviour
    {
        [Header("Player Inputs")]
        private Vector2 _inputs;

        [SerializeField] 
        private UIVirtualJoystick _joystickInput;
        
        private PlayerInput _inputActions;
        private InputAction _runAction;
        
        [Header("Animation Section")]
        public Animator _animator;
        private int _animSpeedId;
        private int _animRunId;
        private float _animSpeed;

        private Rigidbody _rigidBody;
        private Vector3 _movement;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _playerSpeed;
        private float _standardSpeed = 4;
        private float _runSpeed = 5;

        [Header("Camera Reference")]
        public Camera _mainCamera;

        //general vars
        public Transform myTransform;
        private GameObject Target;
        private float Distance_Player_Enemy;
        private bool yesos;
        public GameObject CloudOfSskills;
        public UI ui;
        //[SyncVar/*(hook = nameof(OnHealthChanged))*/]
        public float Myhealth=100f;
        [SyncVar(hook = nameof(OnRefreshQuestChanged))]
        public int RefreshQuestLog = 0;
        public QuestListController questListController;
        public GameObject QuestLog;

        //omar vars
        public GameObject Skill1_omar;
        public GameObject Skill2_omar;
        public GameObject Skill2_omar_secondphase;
        private Rigidbody rigidbomb;
        private Transform Bombposition;
        private BoxCollider colliderbomb;

        // younes vars
        public GameObject Skill1_younes;
        public GameObject Skill1_younes_secondphase;
        public GameObject Skill2_younes;

        //oubeid vars 
        public GameObject Skill1_oubeid;
        public GameObject Skill2_oubeid;

        //rami vars
        public GameObject Skill1_rami;
        public GameObject Skill1_rami_secondphase;
        public GameObject Skill1_rami_thirdphase;
        public GameObject Skill2_rami;
        public GameObject Skill2_rami_secondphase;

        // audio 
        public AudioClip sound1omar;
        public AudioClip sound2omar;
        public AudioClip sound1younes;
        public AudioClip sound2younes;
        public AudioClip sound1oubeid;
        public AudioClip sound2oubeid;
        public AudioClip sound1rami;
        public AudioClip sound2rami;


        public override void OnStartLocalPlayer()
        {
            Debug.Log("Local Player Started.");
            
        }

    public void Start()
        {
            if (!isLocalPlayer)
            {

            CinemachineFreeLook freelook = gameObject.GetComponentInChildren<CinemachineFreeLook>();
            freelook.gameObject.SetActive(false);
            }
            _joystickInput = GameObject.Find("UI_Mobile").gameObject.transform.GetChild(2).gameObject.GetComponent<UIVirtualJoystick>();
            _rigidBody = GetComponent<Rigidbody>();        
            _inputActions = GetComponent<PlayerInput>();
            _runAction = _inputActions.actions["Run"];       
            _animator = GetComponent<Animator>();
            _animSpeedId = Animator.StringToHash("Speed");
            _animRunId = Animator.StringToHash("isPlayerRunning");         
            _mainCamera = Camera.main;
            ui = FindObjectOfType<UI>();
            questListController = FindObjectOfType<QuestListController>();
    }

    public void Update()
        {

            if (isLocalPlayer)
            {
                myTransform = NetworkClient.localPlayer.gameObject.transform;
                FindClosestEnemy();
                Distance_Player_Enemy = Vector3.Distance(myTransform.position, Target.transform.position);
                GameObject.Find("UI_Mobile").transform.GetChild(0).GetComponent<Slider>().value = Myhealth;
                QuestLog = GameObject.Find("Quest Log");

             }

        Skill2_Omar_Second_Phase();
    }

    public void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            Movement();
        OnPlayerDeath();
    }

    public void OnRefreshQuestChanged(int oldValue, int newValue)
    {

        Debug.Log($"Player's refreshquest changed from {oldValue} to {newValue}");

        if (!isServer)
        {
            questListController.OnRefreshQuestLogChanged(newValue);
        }

            


    }


    public void OnPlayerDeath()
    {
        if (Myhealth <= 0f)
        {
            CmdOnPlayerDeath(gameObject);

        }
    }

    [Command]
    public void CmdOnPlayerDeath(GameObject player)
    {
        RpcOnPlayerDeath(player);
        StartCoroutine(WaittwoSeconds(player.transform));

    }

    [ClientRpc]
    public void RpcOnPlayerDeath(GameObject player)
    {
        player.GetComponent<Animator>().SetBool("Death", true);
        Destroy(player, 2.5f);

    }

    public void OnEnemyDeath(GameObject enemy, float monsterhealth)
    {
        if (monsterhealth <= 0f)
        {
            //enemy.GetComponent<LootSpawner>().spawnLoot();
            Destroy(enemy);
            GameObject cloud = Instantiate(CloudOfSskills, enemy.transform.position, Quaternion.identity);
            Destroy(cloud, 1f);
        }
    }

    public void QuestLogCheck()
    {
        if (QuestLog != null && QuestLog.activeSelf == true)
        {
            RefreshQuestLog += 1;
        }
    }

    public void Skill1_Omar()
    {
            if (!ui.Cooldown_Skill1_omar)
            {
                ui.Cooldown_Skill1_omar = true;
                ui.fillImage.fillAmount = 1;
                Collider[] colliders = Physics.OverlapSphere(myTransform.position, 12f);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    foreach (GameObject enemy in enemies)
                    {
                        if (rb != null && rb == enemy.GetComponent<Rigidbody>()&& rb.gameObject!= null)
                        {
                                yesos = true;
                        //GetComponent<AudioSource>().PlayOneShot(sound1omar);
                        QuestLogCheck();
                        CmdSpawnSkill1_Omar(myTransform, enemy, RefreshQuestLog);
                        }
                    }
                }
                if (yesos)
                {
                    ui.fillImage_ULT.fillAmount = ui.fillImage_ULT.fillAmount + 0.33333f;
                    yesos = false;
                }
            }
    }

    [Command]
    void CmdSpawnSkill1_Omar(Transform transform, GameObject enemy, int refreshQuestLog)
    {
        //visuals here 
            RefreshQuestLog = refreshQuestLog;
            GameObject vfx = Instantiate(Skill1_omar, transform.position, transform.rotation);
            NetworkServer.Spawn(vfx);
            Rpc_Monster_skill1_omar(enemy/*, refreshQuestLog*/);
            Destroy(vfx, 1.0f);
    }


    [ClientRpc]
    void Rpc_Monster_skill1_omar(GameObject enemy/*, int refreshQuestLog*/)
    {
        if(enemy != null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound1omar);
            float monsterhealth = enemy.transform.GetChild(0).transform.GetChild(1).GetComponent<ProgressBar>().ProgressImage.fillAmount -= 0.1f;
        OnEnemyDeath(enemy, monsterhealth);
        }

    }
    public void Skill2_Omar()
        {
            if (ui.fillImage_ULT.fillAmount >= 0.99999f)
            {
                if (Distance_Player_Enemy <= 10f)
                {
                    ui.fillImage_ULT.fillAmount = 0;
                    if (GameObject.Find("Bomb(Clone)") == null)
                    {
                        bool bomb = true;
                        QuestLogCheck();
                        CmdSpawnSkill2_Omar(bomb, Target);
                    }
                }
            }
        }

        public void Skill2_Omar_Second_Phase()
        {
            if (rigidbomb != null)
            {
                if (rigidbomb.IsSleeping())
                {
                    Collider[] colliders = Physics.OverlapSphere(Bombposition.position, 18f);
                    foreach (Collider hit in colliders)
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.freezeRotation = true;
                            rb.AddExplosionForce(100f, Bombposition.position, 15f, 3.0F);
                            RpcSpawnSkill2_Omar(Target);
                        }
                    }
                    GameObject afterbomb= Instantiate(Skill2_omar_secondphase, Bombposition.position, Bombposition.rotation);
                    NetworkServer.Spawn(afterbomb);
                    Destroy(GameObject.Find("Bomb(Clone)"));
                    Destroy(GameObject.Find("Explosion01(Clone)"), 2.0f);
                }
            }
        }

    [Command]
    void CmdSpawnSkill2_Omar(bool skill, GameObject target)
    {
        //visuals here 
            GameObject _cloud = Instantiate(CloudOfSskills, target.transform.position + new Vector3(0, 5f, 1f), target.transform.rotation);
            NetworkServer.Spawn(_cloud);
            Destroy(_cloud, 1f);
            GameObject _bomb = Instantiate(Skill2_omar, target.transform.position + new Vector3(0, 5f, 1f), target.transform.rotation);
            NetworkServer.Spawn(_bomb);
            rigidbomb = GameObject.Find("Bomb(Clone)").AddComponent<Rigidbody>();
            colliderbomb = GameObject.Find("Bomb(Clone)").AddComponent<BoxCollider>();
            Bombposition = GameObject.Find("Bomb(Clone)").transform;


    }

    [ClientRpc]
    void RpcSpawnSkill2_Omar(GameObject enemy)
    {
        //damage here 
        if(enemy!= null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound2omar);
            float monsterhealth = enemy.transform.GetChild(0).transform.GetChild(1).GetComponent<ProgressBar>().ProgressImage.fillAmount -= 0.5f;
        OnEnemyDeath(enemy, monsterhealth);
        }



    }


    public void Skill1_Younes()
        {
        if (isLocalPlayer)
        {
            if (!ui.Cooldown_Skill1_younes)
            {
                ui.Cooldown_Skill1_younes = true;
                ui.fillImage.fillAmount = 1;
                if (Distance_Player_Enemy <= 15f)
                {
                    bool harboucha = true;
                    Vector3 direction = Target.transform.position - transform.position;
                    direction.y = Mathf.Max(0.5f, direction.y);
                    direction.Normalize();
                    Invoke("Skill1_Younes_Second_Phase", 2.0f);
                    yesos = true;
                    QuestLogCheck();
                    CmdSpawnSkill1_Younes(harboucha, myTransform, direction, RefreshQuestLog);

                }
                if (yesos)
                {
                    ui.fillImage_ULT.fillAmount = ui.fillImage_ULT.fillAmount + 0.33333f;
                    yesos = false;
                }
            }
        }

        }

        [Command]
        void Skill1_Younes_Second_Phase()
        {
            GameObject poison = Instantiate(Skill1_younes_secondphase, GameObject.Find("pill(Clone)").transform.position, Quaternion.identity);
            NetworkServer.Spawn(poison);
            Collider[] colliders = Physics.OverlapSphere(poison.transform.position, 12f);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            StartCoroutine(PoisonEnemies(enemies, poison));
            Destroy(poison, 3.0f);
        }

        public IEnumerator PoisonEnemies(GameObject[] enemies, GameObject poison)
        {

            while (poison != null)
            {
                RpcSpawnSkill1_Second_Younes(poison, enemies);
                yield return new WaitForSeconds(1.0f);
            }
         }

    [Command]
    void CmdSpawnSkill1_Younes(bool skill, Transform transform, Vector3 dir, int refreshQuestLog)
    {
        //visuals here 
        if (skill)
        {
            RefreshQuestLog = refreshQuestLog;
            skill = false;
            // Instantiate the prefab
            GameObject cloud = Instantiate(CloudOfSskills, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
            NetworkServer.Spawn(cloud);
            Destroy(cloud, 1f);
            GameObject projectile = Instantiate(Skill1_younes, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
            NetworkServer.Spawn(projectile);
            projectile.AddComponent<Rigidbody>();
            projectile.AddComponent<BoxCollider>();
            // Calculate the initial velocity required for the projectile to reach the target with the declared magnitude
            float timeToReachDirection = dir.magnitude / 10f;
            Vector3 initialVelocity = dir.normalized * 0.7f / timeToReachDirection - 0.5f * Physics.gravity * timeToReachDirection;
            // Apply the initial velocity to the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = initialVelocity;
            // Destroy the projectile after a set amount of time
            Destroy(projectile, 3.0f);

        }

    }

    [ClientRpc]
    void RpcSpawnSkill1_Second_Younes(GameObject poi, GameObject[] enemis )
    {
        //damage here 
        foreach (GameObject enemy in enemis)
        {
            if (enemy != null)
            {
                // damage code here
                GetComponent<AudioSource>().PlayOneShot(sound1younes);
                float monsterhealth= enemy.transform.GetChild(0).transform.GetChild(1).GetComponent<ProgressBar>().ProgressImage.fillAmount -= 0.05f;
                OnEnemyDeath(enemy, monsterhealth);

            }
        }
    }

    public void Skill2_Younes()
        {

            if (ui.fillImage_ULT.fillAmount >= 0.99999f)
            {
                ui.fillImage_ULT.fillAmount = 0;
                CmdSpawnSkill2_Younes();
            }
        }

    [Command]
    void CmdSpawnSkill2_Younes()
    {
            //visuals here 
            GameObject[] allies = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject ally in allies)
            {
                RpcSpawnSkill2_Dmg_Younes(ally);
                GameObject ULT = Instantiate(Skill2_younes, ally.transform.position, Quaternion.identity);
                NetworkServer.Spawn(ULT);
                Destroy(ULT, 1.0f);
            }
    }

    [ClientRpc]
    void RpcSpawnSkill2_Dmg_Younes(GameObject ally)
    {
        //damage here 
        if(ally!= null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound2younes);
            ally.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Myhealth += 50f;
        }

    }

    public void Skill1_Oubeid()
        {
 

            if (!ui.Cooldown_Skill1_oubeid)
            {
                ui.Cooldown_Skill1_oubeid = true;
                ui.fillImage.fillAmount = 1;

                if (Distance_Player_Enemy <= 12f)
                {
                QuestLogCheck();
                    Invoke("Skill1_Oubeid_Second_Phase", 0f);
                    yesos = true;

            }
                if (yesos)
                {
                    ui.fillImage_ULT.fillAmount = ui.fillImage_ULT.fillAmount + 0.33333f;
                    yesos = false;
                }
            }
        }


    void Skill1_Oubeid_Second_Phase()
        {
            CmdSpawnSkill1_Oubeid_second(myTransform, RefreshQuestLog);
        }

    [Command]
    void CmdSpawnSkill1_Oubeid_second(Transform transform, int refreshQuestLog)
    {
        //visuals here 
        RefreshQuestLog = refreshQuestLog;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 12f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            foreach (GameObject enemy in enemies)
            {
                if (rb != null && rb == enemy.GetComponent<Rigidbody>())
                {
                    // damage code here;
                    RpcSpawnSkill1_Dmg_Oubeid(enemy);
                    GameObject frequency = Instantiate(Skill1_oubeid, rb.gameObject.transform.position, Quaternion.identity);
                    NetworkServer.Spawn(frequency);
                    Destroy(frequency, 2.0f);
                    StartCoroutine(PushRigidbody(rb, new Vector3(2f, 0f, 0f), 5f, 1f));
                    StartCoroutine(FreezeConstraints(rb, 3f));

                }
            }
        }
    }

    [ClientRpc]
    void RpcSpawnSkill1_Dmg_Oubeid(GameObject enemy)
    {
        //damage here 
        if(enemy!= null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound1oubeid);
            float monsterhealth= enemy.transform.GetChild(0).transform.GetChild(1).GetComponent<ProgressBar>().ProgressImage.fillAmount -= 0.1f;
            OnEnemyDeath(enemy, monsterhealth);

        }

    }

    public IEnumerator PushRigidbody(Rigidbody rb, Vector3 direction, float distance, float duration)
        {
            // Store the initial position of the Rigidbody
            Vector3 initialPosition = rb.transform.position;

            // Initialize the hit variable
            RaycastHit hit;

            // Cast a ray in the push direction to check for obstacles
            if (Physics.Raycast(initialPosition, direction, out hit, distance))
            {
                // If there is an obstacle, reduce the distance to the distance to the obstacle
                distance = hit.distance;
            }

            // Calculate the target position based on the direction and distance
            Vector3 targetPosition = initialPosition + direction.normalized * distance;

            // Calculate the speed at which to move the Rigidbody
            float speed = distance / duration;

            // Move the Rigidbody towards the target position over time
            float startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                rb.transform.position = Vector3.Lerp(initialPosition, targetPosition, t * speed);

                // Cast a ray in the push direction to check for obstacles
                if (Physics.Raycast(rb.transform.position, direction, out hit, distance))
                {
                    // If there is an obstacle, set the target position to the hit point
                    targetPosition = hit.point;
                    distance = Vector3.Distance(initialPosition, targetPosition);
                    speed = distance / duration;
                }

                yield return null;
            }

        }

        public IEnumerator FreezeConstraints(Rigidbody rb, float duration)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            yield return new WaitForSeconds(duration);
            rb.constraints = RigidbodyConstraints.None;
        }

        public void Skill2_Oubeid()
        {

            if (ui.fillImage_ULT.fillAmount >= 0.99999f)
            {
                ui.fillImage_ULT.fillAmount = 0;
                CmdSpawnSkill2_Oubeid();

            }

        }

    [Command]
    public void CmdSpawnSkill2_Oubeid()
    {

        //visuals here 
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in allies)
        {
            RpcSpawnSkill2_Dmg_Oubeid(player);
        }     
    }

    private IEnumerator ActivateULT(GameObject ULT, GameObject player, float oldhealth)
    {
       
        // Set the "active" flag to true for 10 seconds
        ULT.SetActive(true);
        player.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Myhealth = 999f;
        yield return new WaitForSeconds(10f);

        // Restore the initial state of the ULT object
        ULT.SetActive(false);
        player.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Myhealth=  oldhealth;
    }

    [ClientRpc]
    void RpcSpawnSkill2_Dmg_Oubeid(GameObject player)
    {
        //damage here 
        if(player!= null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound2oubeid);
            GameObject ULT = player.transform.GetChild(0).gameObject;
        float oldhealth= player.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Myhealth;
        StartCoroutine(ActivateULT(ULT, player, oldhealth));
        }


    }

    public void Skill1_Rami()
    {

        if (!ui.Cooldown_Skill1_rami)
        {
            ui.Cooldown_Skill1_rami = true;
        ui.fillImage.fillAmount = 1;

        if (Distance_Player_Enemy <= 20f)
        {
                QuestLogCheck();
                CmdSpawnSkill1_Rami(myTransform, Target, RefreshQuestLog);

            }

            ui.fillImage_ULT.fillAmount += 0.33333f;
        }

    }

    [Command]
    void CmdSpawnSkill1_Rami(Transform transform, GameObject target, int refreshQuestLog)
    {
        //visuals here 
        RefreshQuestLog= refreshQuestLog;
        GameObject cloud1 = Instantiate(CloudOfSskills, transform.position + Vector3.left, Quaternion.identity);
        NetworkServer.Spawn(cloud1);
        Destroy(cloud1);
        GameObject cloud2 = Instantiate(CloudOfSskills, transform.position + Vector3.right, Quaternion.identity);
        NetworkServer.Spawn(cloud2);
        Destroy(cloud2);
        GameObject cloud3 = Instantiate(CloudOfSskills, transform.position + Vector3.forward, Quaternion.identity);
        NetworkServer.Spawn(cloud3);
        Destroy(cloud3);
        GameObject mushroom1 = Instantiate(Skill1_rami, transform.position + Vector3.left, Quaternion.identity);
        NetworkServer.Spawn(mushroom1);
        GameObject mushroom2 = Instantiate(Skill1_rami, transform.position + Vector3.right, Quaternion.identity);
        NetworkServer.Spawn(mushroom2);
        GameObject mushroom3 = Instantiate(Skill1_rami, transform.position + Vector3.forward, Quaternion.identity);
        NetworkServer.Spawn(mushroom3);

        NavMeshAgent nav1 = mushroom1.GetComponent<NavMeshAgent>();
        NavMeshAgent nav2 = mushroom2.GetComponent<NavMeshAgent>();
        NavMeshAgent nav3 = mushroom3.GetComponent<NavMeshAgent>();

        nav1.SetDestination(target.transform.position);
        nav2.SetDestination(target.transform.position);
        nav3.SetDestination(target.transform.position);

        Destroy(mushroom1, 10f);
        Destroy(mushroom2, 10f);
        Destroy(mushroom3, 10f);

        StartCoroutine(Launch_Mushrooms_Projectiles(mushroom1, mushroom2, mushroom3, target));
    }

    public IEnumerator Launch_Mushrooms_Projectiles(GameObject mushroom1, GameObject mushroom2, GameObject mushroom3, GameObject target)
    {
        List<Vector3> projectilePositions = new List<Vector3>();

        while (mushroom1 != null && mushroom2 != null && mushroom3 != null)
        {
            yield return new WaitForSeconds(0.1f);

            bool allMushroomsStopped = true;
            foreach (GameObject mushroom in new[] { mushroom1, mushroom2, mushroom3 })
            {
                NavMeshAgent nav = mushroom.GetComponent<NavMeshAgent>();
                if (Vector3.Distance(target.transform.position, mushroom.transform.position) > nav.stoppingDistance)
                {
                    allMushroomsStopped = false;
                    break;
                }
            }

            if (allMushroomsStopped)
            {
                while (true)
                {
                    yield return new WaitForSeconds(1f);

                    foreach (GameObject mushroom in new[] { mushroom1, mushroom2, mushroom3 })
                    {
                        if (mushroom != null && target!= null)
                        {
                            GameObject projectile = Instantiate(Skill1_rami_secondphase, mushroom.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                            NetworkServer.Spawn(projectile);
                            projectile.AddComponent<BoxCollider>();
                            RpcSpawnSkill1_Dmg_Rami(target);

                            Vector3 direction = target.transform.position - mushroom.transform.position;
                            direction.y = Mathf.Max(0.5f, direction.y);
                            direction.Normalize();

                            float timeToReachDirection = direction.magnitude / 5f;
                            Vector3 initialVelocity = direction.normalized * 1f / timeToReachDirection - 0.5f * Physics.gravity * timeToReachDirection + new Vector3(0f, 4f, 0f);

                            Rigidbody rb = projectile.GetComponent<Rigidbody>();
                            rb.velocity = initialVelocity;
                            Destroy(projectile, 1.5f);

                            projectilePositions.Add(projectile.transform.position);
                        }

                    }



                    if (projectilePositions.Count > 0)
                    {
                        GameObject small_explosion = Instantiate(Skill1_rami_thirdphase, target.transform.position, Quaternion.identity);
                        NetworkServer.Spawn(small_explosion);
                        Destroy(small_explosion, 1f);

                        foreach (Vector3 position in projectilePositions)
                        {
                            GameObject small_explosion1 = Instantiate(Skill1_rami_thirdphase, position, Quaternion.identity);
                            NetworkServer.Spawn(small_explosion1);
                            Destroy(small_explosion1, 1f);
                        }

                        projectilePositions.Clear();
                    }
                }
            }
        }
    }

    [ClientRpc]
    void RpcSpawnSkill1_Dmg_Rami(GameObject x)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null && x!= null)
            {
                if (Vector3.Distance(enemy.transform.position, x.transform.position) < 10f)
                {
                    GetComponent<AudioSource>().PlayOneShot(sound1rami);
                    float monsterhealth =enemy.transform.GetChild(0).transform.GetChild(1).GetComponent<ProgressBar>().ProgressImage.fillAmount -= 0.01f;
                    OnEnemyDeath(enemy, monsterhealth);

                }
            }

        }
    }


    public void Skill2_Rami()
        {
            if (ui.fillImage_ULT.fillAmount >= 0.99999f)
            {
                if (Distance_Player_Enemy <= 20f)
                {
                    ui.fillImage_ULT.fillAmount = 0;
                    QuestLogCheck();
                    CmdSpawnSkill2_Rami(myTransform, Target);
                }

            }
        }

    [Command]
    public void CmdSpawnSkill2_Rami(Transform transform, GameObject target)
    {

        //visuals here 
        GameObject cloud = Instantiate(CloudOfSskills, transform.transform.position + new Vector3(3f, 0f, 0.5f), Quaternion.identity);
        NetworkServer.Spawn(cloud);
        Destroy(cloud);
        GameObject Giant = Instantiate(Skill2_rami, transform.transform.position + new Vector3(3f, 0f, 0.5f), Quaternion.identity);
        NetworkServer.Spawn(Giant);
        NavMeshAgent nav_giant = Giant.GetComponent<NavMeshAgent>();
        Animator GiantAnim = Giant.GetComponent<Animator>();
        GiantAnim.SetBool("attack", false);
        Destroy(Giant, 10f);
        // Wait until the agent has reached its destination
        StartCoroutine(WaitForDestinationReached(nav_giant, Giant, transform, target));
    }

    public IEnumerator WaitForDestinationReached(NavMeshAgent agent, GameObject obj, Transform transform, GameObject Target)
        {

            while (agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // The agent has reached its destination, do something
            Animator objAnim = obj.GetComponent<Animator>();
            objAnim.SetBool("attack", true);

            // Keep instantiating the beam GameObject every one second
            while (obj != null && Target!= null)
            {
                agent.SetDestination(Target.transform.position);
                Collider[] colliders = Physics.OverlapSphere(transform.position, 30f);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    foreach (GameObject enemy in enemies)
                    {
                        if (rb != null && rb == enemy.GetComponent<Rigidbody>())
                        {
                            // damage code here;
                            GameObject beam = Instantiate(Skill2_rami_secondphase, rb.gameObject.transform.position, Quaternion.identity);
                            NetworkServer.Spawn(beam);
                            Destroy(beam, 1f);
                            //rb.AddForce(new Vector3(2f, 0f, 0f) * 0.2f, ForceMode.Impulse);
                            RpcSpawnSkill2_Dmg_Rami(enemy);
                        }
                    }
                }

                yield return new WaitForSeconds(1f);
            }

        }

    [ClientRpc]
    void RpcSpawnSkill2_Dmg_Rami(GameObject enemy)
    {  
        if(enemy!= null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound2rami);
            float monsterhealth = enemy.transform.GetChild(0).transform.GetChild(1).GetComponent<ProgressBar>().ProgressImage.fillAmount -= 0.05f;
            OnEnemyDeath(enemy, monsterhealth);
        }


    }


    public void FindClosestEnemy()
        {
            float Distancetoclosestbacteria = Mathf.Infinity;
            GameObject Closestbacteria = null;
            GameObject[] Allbacteria = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject Currentbacteria in Allbacteria)
            {
                if (Currentbacteria != null)
                {
                    float Distancetobacteria = (Currentbacteria.transform.position - myTransform.position).sqrMagnitude;
                    if (Distancetobacteria < Distancetoclosestbacteria)
                    {
                        Distancetoclosestbacteria = Distancetobacteria;
                        Closestbacteria = Currentbacteria;
                        Target = Closestbacteria;

                    }
                }
            }
        }



        #region MovementSection
        private void Movement()
        {
            _inputs = _joystickInput.PlayerJoystickOutputVector();
            _playerSpeed = IsPlayerRunning() ? _runSpeed : _standardSpeed;
            _animSpeed = Mathf.Abs(_inputs.x) + Mathf.Abs(_inputs.y);

            if(_inputs != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(_inputs.x, _inputs.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, 0.12f);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

                _movement = targetDirection * _playerSpeed + new Vector3(0, _rigidBody.velocity.y, 0) ;

                Animations(IsPlayerRunning(), _animSpeed);
            }
            else
            {
                _playerSpeed = 0.0f;
                _movement = new Vector3(0, _rigidBody.velocity.y, 0);
                Animations(false, _animSpeed);
            }
            _rigidBody.velocity = _movement;


        }

        private bool IsPlayerRunning()
        {
            bool isPlayerRunning = _runAction.ReadValue<float>() != 0 ? true : false;
            return isPlayerRunning;
        }
        #endregion

        #region AnimationSection
        private void Animations(bool isRunning, float animInt)
        {
            Cmdmouve(gameObject, isRunning, animInt);
        }

    [Command]
    public void Cmdmouve(GameObject player, bool isRunning, float animInt)
    {
        Rpcmouve(player, isRunning, animInt);
    }

    [ClientRpc]
    public void Rpcmouve(GameObject player, bool isRunning, float animInt)
    {
        player.GetComponent<Animator>().SetBool(_animRunId, isRunning);
        player.GetComponent<Animator>().SetFloat(_animSpeedId, animInt);
    }
    #endregion

    private IEnumerator WaittwoSeconds(Transform transform)
    {
        yield return new WaitForSeconds(1f);
        GameObject end = Instantiate(CloudOfSskills, transform.position, Quaternion.identity);
        NetworkServer.Spawn(end);
        Destroy(end,1f);

    }
}



