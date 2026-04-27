using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 recoilTarget;
    private bool initialized = false;

    public float recoilUp = 0.00075f;
    public float recoilBack = 0.0015f;
    public float recoilSpeed = 10f;
    public float returnSpeed = 4f;

    void Update()
    {
        if (!initialized)
        {
            originalPosition = transform.localPosition;
            recoilTarget = originalPosition;
            initialized = true;
            return;
        }

        if (Input.GetMouseButtonDown(0) && PlayerController.gameStarted)
            recoilTarget = originalPosition + new Vector3(0, -recoilBack, recoilUp);

        if (recoilTarget != originalPosition)
            transform.localPosition = Vector3.Lerp(transform.localPosition, recoilTarget, recoilSpeed * Time.deltaTime);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, returnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, recoilTarget) < 0.001f)
            recoilTarget = originalPosition;
    }
}