using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "RPG/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;       // アイテム名（例：やくそう）
    public int price;             // 買い値（例：8ゴールド）
    public UnityEngine.Sprite icon;         // アイテムのアイコン画像
    [TextArea]
    public string description;    // 説明文（例：ＨＰを ３０ほど かいふくする）
}