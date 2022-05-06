using UnityEngine;

public class MovePlatformWithInput : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 5.0f;
    private void Update()
    {
        float dir = 0;
        if (Input.GetKey(KeyCode.DownArrow))
            dir = -1f;
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = 1f;
        
        transform.Translate(Vector2.down * dir * Time.deltaTime * m_Speed);
    }
}