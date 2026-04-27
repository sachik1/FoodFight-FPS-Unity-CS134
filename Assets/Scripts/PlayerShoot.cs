using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public CrosshairExpand crosshairExpand;
    public float damage = 25f;
    public float range = 100f;
    public float spreadScale = 1f; // <1 tighter than visual, >1 looser
    public float bulletRadius = 0.1f;
    public AudioSource gunShotSound;
    public ParticleSystem muzzleFlash;

    void Update()
    {
        if (!PlayerController.gameStarted) return;

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    // FOR DISPERSION TESTING
    public RectTransform debugDotPrefab; // a small black UI Image prefab
    public Canvas canvas;
    void SpawnDebugDot(Vector2 screenPos)
    {
        RectTransform dot = Instantiate(debugDotPrefab, canvas.transform);
        dot.anchoredPosition = screenPos - new Vector2(Screen.width / 2, Screen.height / 2);
        Destroy(dot.gameObject, 2f);
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunShotSound.PlayOneShot(gunShotSound.clip);
        float spread = crosshairExpand.GetSpreadRadius() * spreadScale;
        Vector2 randomOffset = Random.insideUnitCircle * spread;

        Vector3 screenCenter = new Vector3(Screen.width / 2 + randomOffset.x, Screen.height / 2 + randomOffset.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        bool hitEnemy = false;

        if (Physics.SphereCast(ray, bulletRadius, out hit, range))
        {
            if (hit.collider.CompareTag("Target"))
            {
                hit.collider.GetComponent<TargetHealth>()?.TakeDamage(damage);
                hitEnemy = true;
            }
        }

        // UNCOMMENT TO TEST
        //SpawnDebugDot(new Vector2(Screen.width / 2 + randomOffset.x, Screen.height / 2 + randomOffset.y));
        crosshairExpand.OnShoot(hitEnemy);
}
}