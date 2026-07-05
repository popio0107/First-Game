using UnityEngine;

// ?? Unityのメニューから右クリックで作れるようにする
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Battle/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int maxHp = 50;
    public int attackPower = 10;

    // 必要に応じて魔法力や素早さなどもここに追加できます
}