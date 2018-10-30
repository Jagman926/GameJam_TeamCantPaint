using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    //Backgrounds
    //Backgrounds MUST be size of camera view and MUST be same length (x)
    [SerializeField]
    private List<GameObject> backgroundsList;
    [SerializeField]
    private List<float> backgroundSpeeds;
    private float backgroundLength;

    //Gameobject container
    private List<GameObject> firstObjects;
    private List<GameObject> secondObjects;

    [SerializeField]
    //Player
    private GameObject playerObject;

    void Awake()
    {
        //Init list
        firstObjects = new List<GameObject>();
        secondObjects = new List<GameObject>();
    }

    void Start()
    {
        //Get player object
        //playerObject = GameObject.Find("Player");
        //If you are looking at this you did this to yourself
        if (playerObject == null)
        {
            Debug.Log("WHO THE FUCK CHANGED THE PLAYERSNAME!!!!");
        }
        //Init background stuff
        InitBackgrounds();
    }

    void Update()
    {
        UpdateLayerMovement();
        CheckLayers();
    }

    private void InitBackgrounds()
    {
        //Background length
        backgroundLength = backgroundsList[0].GetComponent<BoxCollider2D>().size.x;
        //Background offset
        Vector3 offset = new Vector3(transform.position.x + backgroundLength, transform.position.y, transform.position.z);

        for (int i = 0; i < backgroundsList.Count; i++)
        {
            //Put base background image as first object of each layer
            firstObjects.Add(Instantiate(backgroundsList[i], transform.position, Quaternion.identity));
			firstObjects[i].transform.parent = gameObject.transform;
            secondObjects.Add(Instantiate(backgroundsList[i], transform.position + offset, Quaternion.identity));
			secondObjects[i].transform.parent = gameObject.transform;
        }
    }

    private void UpdateLayerMovement()
    {
        for (int i = 0; i < firstObjects.Count; i++)
        {
            firstObjects[i].transform.position = new Vector3(firstObjects[i].transform.position.x - (Input.GetAxisRaw("Horizontal") * 1.0f/backgroundSpeeds[i]),
                                                             firstObjects[i].transform.position.y, firstObjects[i].transform.position.z);
            secondObjects[i].transform.position = new Vector3(secondObjects[i].transform.position.x - (Input.GetAxisRaw("Horizontal") * 1.0f/backgroundSpeeds[i]),
            												 secondObjects[i].transform.position.y, secondObjects[i].transform.position.z);
        }

    }

    private void CheckLayers()
    {
        for (int i = 0; i < firstObjects.Count; i++)
        {

            if (firstObjects[i].transform.position.x > playerObject.transform.position.x + backgroundLength)
            {
                firstObjects[i].transform.position = new Vector3(firstObjects[i].transform.position.x - backgroundLength * 2, firstObjects[i].transform.position.y, firstObjects[i].transform.position.z);
            }
            else if (firstObjects[i].transform.position.x < playerObject.transform.position.x - backgroundLength)
            {
                firstObjects[i].transform.position = new Vector3(firstObjects[i].transform.position.x + backgroundLength * 2, firstObjects[i].transform.position.y, firstObjects[i].transform.position.z);
            }
            else if (secondObjects[i].transform.position.x > playerObject.transform.position.x + backgroundLength)
            {
                secondObjects[i].transform.position = new Vector3(secondObjects[i].transform.position.x - backgroundLength * 2, secondObjects[i].transform.position.y, secondObjects[i].transform.position.z);
            }
            else if (secondObjects[i].transform.position.x < playerObject.transform.position.x - backgroundLength)
            {
                secondObjects[i].transform.position = new Vector3(secondObjects[i].transform.position.x + backgroundLength * 2, secondObjects[i].transform.position.y, secondObjects[i].transform.position.z);
            }
        }
    }
}
