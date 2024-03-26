//watch 43:50

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private float m_fSpringConst = 0f;
    [SerializeField] private float m_fOriginalPos = 0f;
    [SerializeField] private float m_fPressedPos = 0f;
    [SerializeField] private float m_fFlipperSpringDamp = 0f;
    [SerializeField] private KeyCode m_flipperInput;
    [SerializeField] private float m_flipCooldown = 0.5f;

    private HingeJoint m_hingeJoint = null;
    private JointSpring m_jointSpring;
    private bool m_isFlipping = false;

    private void Start()
    {
        m_hingeJoint = GetComponent<HingeJoint>();
        m_hingeJoint.useSpring = true;

        // Initialize JointSpring
        m_jointSpring = new JointSpring();
        m_jointSpring.spring = m_fSpringConst;
        m_jointSpring.damper = m_fFlipperSpringDamp;
        m_hingeJoint.spring = m_jointSpring;
    }

    private void OnFlipperPressedInternal()
    {
        if (!m_isFlipping)
        {
            m_isFlipping = true;
            StartCoroutine(FlipperCooldown());
            m_jointSpring.targetPosition = m_fPressedPos;
            m_hingeJoint.spring = m_jointSpring;
        }
    }

    private void OnFlipperReleasedInternal()
    {
        m_jointSpring.targetPosition = m_fOriginalPos;
        m_hingeJoint.spring = m_jointSpring;
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_flipperInput))
        {
            OnFlipperPressedInternal();
        }

        if (Input.GetKeyUp(m_flipperInput))
        {
            OnFlipperReleasedInternal();
        }
    }

    private IEnumerator FlipperCooldown()
    {
        yield return new WaitForSeconds(m_flipCooldown);
        m_isFlipping = false;
    }
}
