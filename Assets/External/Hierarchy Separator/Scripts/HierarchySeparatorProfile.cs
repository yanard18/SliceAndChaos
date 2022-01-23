using UnityEngine;

[CreateAssetMenu(menuName = "Hierarchy Separator/Profile")]
public class HierarchySeparatorProfile : ScriptableObject
{
    public Color BackgroundColor = Color.black;
    public Color TextColor = Color.white;
    public FontStyle FontStyle = FontStyle.Bold;
    public TextAnchor TextAlignment = TextAnchor.MiddleLeft;
    public Vector2 TextOffset = new Vector2(8, 1);
    
}
