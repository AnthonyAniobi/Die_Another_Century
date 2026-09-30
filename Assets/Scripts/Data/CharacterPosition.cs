using System.Collections.Generic;
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

    // int key is the level number and the value is a list of characters positions for that level
    static readonly Dictionary<int, List<CharacterPosition>> levels = new Dictionary<int, List<CharacterPosition>>
    {
        {
            1,
            new List<CharacterPosition>
            {
                new CharacterPosition { position = new Vector3(0f, 0f, 5f), characterType = CharacterType.NPC },
                new CharacterPosition { position = new Vector3(8f, 0f, 10f), characterType = CharacterType.Enemy }
            }
        },
        {
            2,
            new List<CharacterPosition>
            {
                new CharacterPosition { position = new Vector3(-5f, 0f, 3f), characterType = CharacterType.NPC },
                new CharacterPosition { position = new Vector3(4f, 0f, 12f), characterType = CharacterType.Enemy },
                new CharacterPosition { position = new Vector3(10f, 0f, 7f), characterType = CharacterType.Enemy }
            }
        }
    };
}
