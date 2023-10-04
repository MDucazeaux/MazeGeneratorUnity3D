
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Component : MonoBehaviour
{
    // List of the prefabs that are open in the direction announced
    public List<GameObject> PrefabsS; //South
    public List<GameObject> PrefabsN; //North
    public List<GameObject> PrefabsW; //West
    public List<GameObject> PrefabsE; //East

    //List of all the position that are taken by the wall spawned
    public List<Vector3> PositionTaken = new List<Vector3>();

    // set this component as an instance
    public static Spawner_Component instance;

    //Get the transform where it will first spawn
    public Transform OriginalSpawn;

    //Get the minimum and maximum number of prefabs spawned
    public int MinimumLength;
    public int MaximumLength;

    private bool canspawn = true;

    //Get the current number of spawned prefabs
    private int currentLength;
    //Get the number of spawned item there will be
    private int randomLength;

    private void Awake()
    {
        //Set the instance of this component
        instance = this;
    }

    void Start()
    {
        List<GameObject> firstListSpawned = new List<GameObject>();
        //Set the number of prefabs that will be spawned
        randomLength = Random.Range(MinimumLength, MaximumLength);
        //Instantiate the first ^refabs and add it to the list
        firstListSpawned.Add(Instantiate(PrefabsS[2], OriginalSpawn.position, Quaternion.identity));
        //Add the position of the prefab spawn in the list of the position that are taken
        PositionTaken.Add(GameObject.FindGameObjectWithTag("Spawned").transform.position);
        //Call the spawn function and give it the list of the prefabs that are already spawned
        Spawner(firstListSpawned);
        
    }

    //Function that spawn prefabs next to other prefabs that have a place avaliable and nned a list of the previous spawned prefabs
    private void Spawner(List<GameObject> SpawnedPrefabs)
    {   
        
        Transform[] nextSpawn;

        List<GameObject> Spawned = new List<GameObject>();
        
        //Do for every spawned prefabs
        foreach (var item in SpawnedPrefabs)
        {
            //Get all the empty object whildren of "item" needed to know where to spawn
            nextSpawn = item.GetComponent<Prefabs_Component>().NextSpawn;
            //Do for every empty object
            foreach (Transform t in nextSpawn)
            {      
                //for every prefabs that are already spawned
                foreach (GameObject v in GameObject.FindGameObjectsWithTag("Spawned"))
                {   
                    //Check if this place is available or not
                    if (Vector3.Distance(v.transform.position, t.position) < 0.1f)
                    {   //If it is not set canspawn to false and break the for each
                        canspawn = false;
                        break;
                    }
                    //if it is set can spawn to true
                    canspawn = true;
                }
               
                //Check if it can spawn and if so check wich way is available
                if (t.GetComponentInParent<Prefabs_Component>().S && canspawn)
                { 
                    GameObject p = PrefabsN[Random.Range(0, PrefabsN.Count - 1)];               //Random prefabs choosen in thos that are in the right way
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));    //instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    currentLength++;                                                            //Add one to the length
                    continue;
                }
                else if (t.GetComponentInParent<Prefabs_Component>().N && canspawn )
                {
                    GameObject p = PrefabsS[Random.Range(0, PrefabsS.Count - 1)];               //Random prefabs choosen in thos that are in the right way
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));    //instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    currentLength++;                                                            //Add one to the length
                    continue;
                }
                else if (t.GetComponentInParent<Prefabs_Component>().W && canspawn )
                {
                    GameObject p = PrefabsE[Random.Range(0, PrefabsE.Count - 1)];               //Random prefabs choosen in thos that are in the right way
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));    //instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    currentLength++;                                                            //Add one to the length
                    continue;
                }
                else if (t.GetComponentInParent<Prefabs_Component>().E && canspawn )
                {
                    GameObject p = PrefabsW[Random.Range(0, PrefabsW.Count - 1)];               //Random prefabs choosen in thos that are in the right way
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));    //instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    currentLength++;                                                            //Add one to the length
                    continue;
                }
            }
        }
        //if we are at he random length we stop and close all the gate
        if (currentLength >= randomLength)
        {
            LastSpawn(Spawned);
            return;
        }


        Spawner(Spawned);
    }

    private void LastSpawn(List<GameObject> SpawnedPrefabs)
    {
        Transform[] nextSpawn;

        List<GameObject> Spawned = new List<GameObject>();
        //Do for every spawned prefabs
        foreach (var item in SpawnedPrefabs)
        {
            //Get all the empty object whildren of "item" needed to know where to spawn
            nextSpawn = item.GetComponent<Prefabs_Component>().NextSpawn;
            //Do for every empty object
            foreach (Transform t in nextSpawn)
            {
                //for every prefabs that are already spawned
                foreach (GameObject v in GameObject.FindGameObjectsWithTag("Spawned"))
                {
                    //Check if this place is available or not
                    if (Vector3.Distance(v.transform.position, t.position) < 0.1f)
                    {   //If it is not set canspawn to false and break the for each
                        canspawn = false;
                        break;
                    }
                    //if it is set can spawn to true
                    canspawn = true;
                }

                if (t.GetComponentInParent<Prefabs_Component>().S && canspawn)
                {
                    GameObject p = PrefabsN[PrefabsN.Count - 1];
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));//instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    continue;
                }
                else if (t.GetComponentInParent<Prefabs_Component>().N && canspawn)
                {
                    GameObject p = PrefabsS[PrefabsN.Count - 1];
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));//instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    continue;
                }
                else if (t.GetComponentInParent<Prefabs_Component>().W && canspawn)
                {
                    GameObject p = PrefabsE[PrefabsN.Count - 1];
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));//instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    continue;
                }
                else if (t.GetComponentInParent<Prefabs_Component>().E && canspawn)
                {
                    GameObject p = PrefabsW[PrefabsN.Count - 1];
                    Spawned.Add(Instantiate(p, t.transform.position, p.transform.rotation));//instantiate the prefabs with the reight tranform and add it to the list of object that have been spawned
                    continue;
                }

            }
        }
    }

}
