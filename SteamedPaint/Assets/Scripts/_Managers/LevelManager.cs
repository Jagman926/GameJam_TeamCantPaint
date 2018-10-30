using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        Managers.ScenesManager scenceManager;

        public MapRegion_MASTER regionMasterReference;

        static public float playerStreetCred = 0f;
        void Start()
        {
            scenceManager = Managers.ScenesManager.Instance;
           
        }
        void Update()
        {
            if (regionMasterReference.regionData != null)
            {
                CheckStreetCred(); // check the player's street cred
                CheckColors();
            }
            
        }

        public void LoadLevel(string levelName)
        {
            scenceManager.LoadLevel(levelName);
        }
        void CheckStreetCred()
        {
            for (int i = 0; i < regionMasterReference.regionData.Count; i++)
            {
                if (playerStreetCred >= regionMasterReference.regionData[i].requiredStreetCred)
                {
                    regionMasterReference.regionData[i].isLocked = false; // unlock the level		
                }
                else if (playerStreetCred < regionMasterReference.regionData[i].requiredStreetCred)
                {
                    regionMasterReference.regionData[i].isLocked = true; // the level is locked if the player doesn't have enough street cred
                }
            }
        }

        void CheckColors()
        {
            for (int i = 0; i < regionMasterReference.regionData.Count; i++)
            {
                if (!regionMasterReference.regionData[i].isLocked) // if the player has enough street cred
                {
                    GameObject.Find(regionMasterReference.regionData[i].regionName).GetComponent<Image>().color = Color.green;
                }
                else
                {
                    GameObject.Find(regionMasterReference.regionData[i].regionName).GetComponent<Image>().color = Color.red; // make the region red
                }
            }
        }
    }
}




