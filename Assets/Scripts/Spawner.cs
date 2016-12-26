using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

    public GameObject[] blocks;
	// Use this for initialization
	void Start () {
        InitGroup();

        spawnNext();
	}
	
    void InitGroup()
    {
        int amount = Difficulty.InitGroupAmount;

        Vector2 firstPos = transform.position - new Vector3((int)(Grid.w / 2 - 1), Grid.h);
        float y = firstPos.y + 3;
        float x = firstPos.x - 2;
        while (amount > 0)  
        {
            int rotation = Random.Range(0, 1);
            rotation = rotation == 1 ? 0 : 90;

            GameObject obj = spawn(new Vector2(x, y), rotation);

            if (!Group.isValidGridPos(obj.transform))
                Destroy(obj);
            else
            {
                amount--;
                Group.updateGrid(obj.transform);
            }

            x += 1;
            if (x > Grid.w - 1)
            {
                y += 1;
                x = firstPos.x - 2;
            }
        }
    }

    public void spawnNext()
    {
        GameObject obj = spawn(transform.position);
        Group group = (Group)obj.GetComponent(typeof(Group));

        group.Run();
    }

    private GameObject spawn(Vector2 position, int rotation = 0)
    {
        int shape = Random.Range(0, blocks.Length);

        GameObject obj = Instantiate(blocks[shape], new Vector2(position.x, position.y), Quaternion.identity);
        obj.transform.Rotate(0, 0, -rotation);

        return obj;
    }
}
