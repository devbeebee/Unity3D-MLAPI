using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [Header("Place Characters as a child of this transform")]
    List<GameObject> playerCharacters;
    public List<GameObject> PlayerCharacters { get => playerCharacters; set => playerCharacters = value; }
}
