using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CannonUIManager : MonoBehaviour
{
    public Text m_velocityText;
    public Text m_AngleText;
    public Text m_PowerLevelText;
    public Text m_ShotsFiredText;
    public Text m_TargetLocationText;
    public Text m_TargetDistanceText;

    public void UpdateVelocityText(string velocity)
    {
        m_velocityText.text = "Last Velocity: " + velocity + "m/s";
    }

    public void UpdateAngleText(float angle)
    {
        m_AngleText.text = "Angle: " + angle + "°";
    }

    public void UpdatePowerLevelText(float powerLevel)
    {
        m_PowerLevelText.text = "Power: " + powerLevel;
    }

    public void UpdateShotsFiredText(float shotsFired)
    {
        m_ShotsFiredText.text = "Shots Fired: " + shotsFired;
    }

    public void UpdateTargetLocationText(Vector3 targetLocation)
    {
        m_TargetLocationText.text = "Target Position: " + targetLocation;
    }

    public void UpdateTargetDistanceText(float distance)
    {
        m_TargetDistanceText.text = "Target Distance: (" + distance + ")m";
    }
}
