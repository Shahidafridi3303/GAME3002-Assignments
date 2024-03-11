using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CannonScript : MonoBehaviour
{
    [SerializeField] private GameObject cannonballInstance;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource CannonFireSFX;
    [SerializeField] private CannonUIManager uiManager;

    [SerializeField][Range(2f, 30f)] private float powerLevel = 5f;
    [SerializeField][Range(33f, 80f)] private float angle = 35f;
    [SerializeField] private float barrelSpeed = 5f;

    private bool m_bCanShoot = true;
    private float m_ShotsFired = 0;
    private Vector3 targetPosition;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        // Check if the player can shoot before processing input
        if (m_bCanShoot && Input.GetMouseButtonDown(0))
        {
            //raycast to mouse position, if it clicks onto gameobject (the floor) send the Vector3 hit position to FireCannonAtPoint
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                targetPosition = hitInfo.point + (hitInfo.point - spawnPoint.position).normalized;

                // Look at the clicked position
                transform.LookAt(targetPosition);

                // Applied additional rotation towards Z-axis
                transform.Rotate(72f, 0f, 0f);

                FireCannonAtPoint(hitInfo.point);
            }
        }
    }

    private void FireCannonAtPoint(Vector3 point)
    {
        Vector3 direction = (point - spawnPoint.position).normalized;
        var velocity = BallisticVelocity(point, angle, powerLevel); // powerLevel to BallisticVelocity

        uiManager.UpdateVelocityText(velocity.ToString());
        uiManager.UpdateTargetLocationText(point);
        float TargetDis = Vector3.Distance(spawnPoint.position, targetPosition);
        uiManager.UpdateTargetDistanceText(TargetDis);

        StartCoroutine("Shoot", direction * velocity.magnitude); // Multiply direction by magnitude of velocity
    }

    private Vector3 BallisticVelocity(Vector3 destination, float angle, float powerLevel)
    {
        Vector3 dir = destination - spawnPoint.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude using power level
        float velocity = powerLevel * Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }

    private IEnumerator Shoot(Vector3 velocity)
    {
        m_bCanShoot = false;

        GameObject cannonBall = GameObject.Instantiate(cannonballInstance, spawnPoint.position, Quaternion.identity);
        cannonBall.GetComponent<Rigidbody>().velocity = velocity;
        GameObject.Destroy(cannonBall, 4.0f);
        yield return new WaitForSeconds(0);  // cannon firing delay

        m_bCanShoot = true;
        m_ShotsFired++;
        CannonFireSFX.Play();
        uiManager.UpdateShotsFiredText(m_ShotsFired);
    }

    private void UpdateUI()
    {
        uiManager.UpdateVelocityText("0");
        uiManager.UpdateAngleText(angle);
        uiManager.UpdatePowerLevelText(powerLevel);
        uiManager.UpdateShotsFiredText(m_ShotsFired);
    }

    public void IncreaseAngle()
    {
        angle = Mathf.Clamp(angle + barrelSpeed, 33f, 80f);
        transform.eulerAngles = new Vector3(0f, 0f, 90f - angle);
        uiManager.UpdateAngleText(angle);
    }

    public void DecreaseAngle()
    {
        angle = Mathf.Clamp(angle - barrelSpeed, 33f, 80f);
        transform.eulerAngles = new Vector3(0f, 0f, 90f - angle);
        uiManager.UpdateAngleText(angle);
    }

    public void IncreasePower()
    {
        powerLevel += 1f;
        uiManager.UpdatePowerLevelText(powerLevel);
    }

    public void DecreasePower()
    {
        powerLevel = Mathf.Max(powerLevel - 1f, 0f);
        uiManager.UpdatePowerLevelText(powerLevel);
    }
}
