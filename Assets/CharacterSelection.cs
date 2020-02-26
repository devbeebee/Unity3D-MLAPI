using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [Header("Place Characters as a child of this transform")]
    List<GameObject> playerCharacters;
    public List<GameObject> PlayerCharacters { get => playerCharacters; set => playerCharacters = value; }
    int selection = 0;

    void ChangeLeft()
    {
        selection--;
        if (selection < 0)
        {
            selection = PlayerCharacters.Count - 1;
        }
    }
    void ChangeRight()
    {
        selection++;
        if (selection > PlayerCharacters.Count - 1)
        {
            selection = 0;
        }
    }
}
