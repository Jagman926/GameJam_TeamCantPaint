using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [Header("Player Prefabs")]
        [SerializeField]
        private GameObject playerObject;
        [SerializeField]
        private GameObject sightMimicPrefab;
        [SerializeField]
        private GameObject strengthMimicPrefab;

        [Header("Player Information")]
        private PlayerMovement playerMovementScript;

        [Header("Active Mimic Information")]
        [SerializeField]
        private GameObject currentActiveMimic;
        private bool sightMimicActive;
        private bool strengthMimicActive;
        [SerializeField]
        private float activeMimicDuration;
        private float activeMimicTimer;

        [Header("Mimic Radius")]
        [SerializeField]
        private bool circleVisable;
        [SerializeField]
        [Range(0, 10)]
        private float mimicRadius;
        [SerializeField]
        [Range(0, 50)]
        private int segments = 50;
        LineRenderer mimicRadiusLine;

        [Header("Stealth Zone Variables")]
        public bool canEnterStealthZone;
        public bool isHidden;

        [FMODUnity.EventRef]
        public string summonMimicEvent;
        [FMODUnity.EventRef]
        public string mimicLeaveEvent;

        private void Start()
        {
            //Set active bools
            sightMimicActive = false;
            strengthMimicActive = false;
            //Get player movement script
            playerMovementScript = playerObject.GetComponent<PlayerMovement>();
            //Mimic Radius
            mimicRadiusLine = playerObject.GetComponent<LineRenderer>();
            mimicRadiusLine.positionCount = segments + 1;
            mimicRadiusLine.useWorldSpace = false;
            CreateCircle();
        }

        private void Update()
        {
            //Check Input
            CheckInput();
            //Check Active Mimic
            CheckActiveMimic();
            //Check Mimic Distance
            CheckMimicDistance();
            //Mimic Timer
            CheckMimicTimer();
        }

        private void CheckInput()
        {
            if (Input.GetButtonDown("SightMimic"))
            {
                //If there is no active mimic
                if (!sightMimicActive)
                {
                    //Destroy Active Mimic
                    DestroyActiveMimic();
                    //Spawn Sight Mimic
                    SpawnMimic("SightMimic");
                }
                else
                {
                    //Destroy Active Mimic
                    DestroyActiveMimic();
                }
            }
            if (Input.GetButtonDown("StrengthMimic"))
            {
                if (!strengthMimicActive)
                {
                    //Destroy Active Mimic
                    DestroyActiveMimic();
                    //Spawn Sight Mimic
                    SpawnMimic("StrengthMimic");
                }
                else
                {
                    //Destroy Active Mimic
                    DestroyActiveMimic();
                }
            }
        }

        private void CheckMimicTimer()
        {
            if (activeMimicTimer < activeMimicDuration && currentActiveMimic != null)
            {
                activeMimicTimer += Time.deltaTime;
            }
            else if (activeMimicTimer > activeMimicDuration)
            {
                ResetMimicTimer();
                DestroyActiveMimic();
            }
        }

        private void CheckActiveMimic()
        {
            //Check for active mimic
            if (currentActiveMimic == null)
            {
                playerMovementScript.movementActive = true;
            }
            else
            {
                playerMovementScript.movementActive = false;
            }
        }

        private void CheckMimicDistance()
        {
            //If Distance from mimic to player is greater than mimicRadius
            if(currentActiveMimic != null && 
               Mathf.Abs(Vector3.Distance(playerObject.transform.position, currentActiveMimic.transform.position)) > mimicRadius * playerObject.transform.localScale.x)
            {
                //Destroy mimic
                DestroyActiveMimic();
            }
        }

        private void SpawnMimic(string mimicType)
        {   
            CallSummonMimic();
            if (mimicType == "SightMimic")
            {
                //Spawn Sight Mimic
                currentActiveMimic = Instantiate(sightMimicPrefab, playerObject.transform.position, Quaternion.identity);
                //Set bool
                sightMimicActive = true;
                //Reset timer
                ResetMimicTimer();
                //Set Radius Visable
                ShowMimicRadius();
            }
            else if (mimicType == "StrengthMimic")
            {
                //Spawn Strength Mimic
                currentActiveMimic = Instantiate(strengthMimicPrefab, playerObject.transform.position, Quaternion.identity);
                //Set bool
                strengthMimicActive = true;
                //Reset Timer
                ResetMimicTimer();
                //Set Radius Visable
                ShowMimicRadius();
            }
            else
            {
                Debug.Log("Mimic type INVALID");
            }
        }

        private void DestroyActiveMimic()
        {
            
            //While there is an active mimic
            if (currentActiveMimic != null)
            {
                //Destroy it
                CallMimicLeave(); 
                Destroy(currentActiveMimic);
            }
            //Set active mimic to null
            currentActiveMimic = null;
            //Set bools to false
            sightMimicActive = false;
            strengthMimicActive = false;
            //Hide mimic radius
            HideMimicRadius();
        }

        private void ResetMimicTimer()
        {
            //Reset timer
            activeMimicTimer = 0.0f; ;
        }

        void CreateCircle()
        {
            float x;
            float y;
            float angle = 20f;

            for (int i = 0; i < (segments + 1); i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * mimicRadius;
                y = Mathf.Cos(Mathf.Deg2Rad * angle) * mimicRadius;
                mimicRadiusLine.SetPosition(i, new Vector3(x, y, 0));
                angle += (360f / segments);
            }
            HideMimicRadius();
        }

        private void ShowMimicRadius()
        {
            mimicRadiusLine.enabled = true;
        }

        private void HideMimicRadius()
        {
            mimicRadiusLine.enabled = false;
        }

        void CallSummonMimic()
        {
            FMODUnity.RuntimeManager.PlayOneShot(summonMimicEvent);
        }
        void CallMimicLeave()
        {
            FMODUnity.RuntimeManager.PlayOneShot(mimicLeaveEvent);
        }
    }
}
