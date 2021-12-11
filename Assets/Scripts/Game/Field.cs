using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private Chip[] _chips = default;
    [SerializeField]
    private Sprite[] _enableSprites = default;


    private void Awake()
    {
        _chips = GetComponentsInChildren<Chip>();

        Test();
    }

    private void Test()
    {
        foreach (var chip in _chips)
        {
            chip.GetComponent<SpriteRenderer>().sprite = _enableSprites[Random.Range(0, _enableSprites.Length)];
        }
    }


    [ContextMenu("Fill")]
    private void Fill()
    {
        float x = -4f;
        float y = 8.1f;
        float z = 0f;
        foreach (var chip in _chips)
        {
            Vector3 position = chip.transform.position;
            chip.transform.localPosition = new Vector3(position.x, position.y, z);
            z -= .0001f;
        }
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Debug.Log(j + i * 5);
                Vector3 position = _chips[j + i * 5].transform.position;
                _chips[j + i * 5].transform.position = new Vector3(x, y, position.z);
                x += 2f;
                if (x > 4)
                {
                    x = -4f;
                }
            }
            y -= 2.7f;
        }
    }
}
