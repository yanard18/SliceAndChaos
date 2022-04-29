using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class StairBuilder : OdinEditorWindow
{

    [MenuItem("Tools/Stair Builder")]
    private static void OpenWindow()
    {
        GetWindow<StairBuilder>().Show();
    }

    [SerializeField]
    private float m_Width;

    [SerializeField]
    private float m_Height;

    [SerializeField]
    private int m_nStep;
    
    [Button(ButtonSizes.Large)]
    public void BuildButton() {Build();}

    private void Build()
    {
        var individualStepWidth = m_Width / m_nStep;
        var individualStepHeight = m_Height / m_nStep;

        var stair = new GameObject("Stair").AddComponent<EdgeCollider2D>();

        var pos = new Vector2();

        var nTotalPoint = m_nStep * 2 + 3;
        Vector2[] colliderPoints = new Vector2[nTotalPoint];
        for (var i = 0; i < nTotalPoint; i++)
        {
            switch (i % 2)
            {
                case 0:
                    pos.x += individualStepWidth;
                    if (i == 0) pos.x = 0;
                    break;
                case 1:
                    pos.y += individualStepHeight;
                    if (i == nTotalPoint - 2) pos.y = 0;
                    break;
            }

            colliderPoints[i] = pos;



            // 0 start = 0,0
            // 1 = 0,1 increase y
            // 2 = 1,1 increase x
            // 3 = 1,2 increase y
            // 4 = 2,2 increase x
            // 5 final 2,0

        }

        pos = Vector2.zero;
        colliderPoints[nTotalPoint - 1] = pos;
        stair.points = colliderPoints;
    }

}
