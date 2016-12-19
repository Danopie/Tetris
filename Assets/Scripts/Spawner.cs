using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

    public GameObject[] blocks;
	// Use this for initialization
	void Start () {
        spawnNext();
	}
	
    public void spawnNext()
    {
        int i = Random.Range(0, blocks.Length);
        GameObject block = Instantiate(blocks[i], transform.position, Quaternion.identity);

    }
}
