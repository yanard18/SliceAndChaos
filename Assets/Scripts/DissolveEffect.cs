using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    private bool m_bIsDissolving;
    private float m_DissolveAmount;
    
    [SerializeField] [Required]
    private Material m_DissolveMaterial;

    [SerializeField]
    private float m_DissolveSpeed;


    private void Update()
    {
        if(!m_bIsDissolving) return;

        m_DissolveAmount = Mathf.Clamp01(m_DissolveAmount + Time.deltaTime * m_DissolveSpeed);
        m_DissolveMaterial.SetFloat("_DissolveAmount", m_DissolveAmount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_bIsDissolving = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        m_bIsDissolving = false;
    }
}
