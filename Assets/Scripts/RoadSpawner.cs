using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] GameObject roadBlockPrefab;
    [SerializeField] GameObject startBlock;

    [SerializeField] Transform shipTrans;

    private float startBlockZPos = 0,
                  currentBlockZPos = 0,
                  blocksCount = 20,
                  blockLenght = 15;
    private int safeZone = 40;

    private List<GameObject> currentBlocks = new List<GameObject>();

    Vector3 startPlayerPos = Vector3.zero;

    void Start()
    {
        startBlockZPos = startBlock.transform.position.z;
        blockLenght = startBlock.GetComponent<BoxCollider>().bounds.size.z;

        StartGame();
    }

    void Update()
    {
        CheckForSpawn();
    }

    public void StartGame()
    {
        currentBlockZPos = startBlockZPos;

        // restart ship position
        shipTrans.gameObject.SetActive(true);
        shipTrans.position = startPlayerPos;
        shipTrans.rotation = Quaternion.identity;

        foreach(var go in currentBlocks)
        {
            Destroy(go);
        }
        currentBlocks.Clear();

        for (int i = 0; i < blocksCount; i++)
        {
            SpawnBlock();
        }
    }

    void CheckForSpawn()
    {
        if (shipTrans.position.z - safeZone > (currentBlockZPos - blocksCount * blockLenght))
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    void SpawnBlock()
    {
        currentBlockZPos += blockLenght;

        GameObject block = Instantiate(roadBlockPrefab, new Vector3(0, -2, currentBlockZPos), Quaternion.identity);

        currentBlocks.Add(block);
    }

    void DestroyBlock()
    {
        Destroy(currentBlocks[0].gameObject);
        currentBlocks.RemoveAt(0);
    }
}
