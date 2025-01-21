using System.Collections;
using UnityEngine;

public class NoGoZone : MonoBehaviour
{
    [SerializeField] private float triggerDistance = 2f; // Distance for triggering effects
    [SerializeField] private float restartDistance = 1f; // Distance to restart the scene
    [SerializeField] private Color warningColor = Color.red; // Warning color
    [SerializeField] private float shakeIntensity = 0.1f; // Intensity of the shake

    private Renderer zoneRenderer; // General Renderer for visual feedback
    private Color originalColor;

    private void Start()
    {
        if (!TryGetComponent(out zoneRenderer))
        {
            Debug.LogWarning("Renderer not found on NoGoZone!", this);
            return;
        }
        originalColor = zoneRenderer.material.color;
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player?.transform == null) return;

        float distanceX = transform.position.x - player.transform.position.x;
        float distanceY = transform.position.y - player.transform.position.y;
        float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

        if (distance <= triggerDistance)
        {
            StartCoroutine(ShakeZone());

            if (distance <= restartDistance && player.TryGetComponent(out Player playerBehavior))
            {
                playerBehavior.RestartScene();
            }
        }
    }

    private IEnumerator ShakeZone()
    {
        Vector3 originalPosition = transform.position;

        if (zoneRenderer)
        {
            zoneRenderer.material.color = warningColor;
        }

        for (int i = 0; i < 10; i++)
        {
            transform.position = originalPosition + new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0f
            );
            yield return new WaitForSeconds(0.05f);
        }

        transform.position = originalPosition;

        if (zoneRenderer)
        {
            zoneRenderer.material.color = originalColor;
        }
    }
}
