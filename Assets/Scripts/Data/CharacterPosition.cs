using UnityEngine;

public class CharacterPosition
{
    
    public enum CharacterType
    {
        Enemy,
        NPC,
    }

    public Vector3 position;
    public CharacterType characterType;

}
