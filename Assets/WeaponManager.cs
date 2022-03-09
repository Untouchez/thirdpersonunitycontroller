using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class WeaponManager : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float recoilResetTimer;
    public float FireRate;
    public Vector2[] recoil;
    public bool isShooting;

    public int magazineCount;
    public int currentMagazineCount;

    int currentRecoilBullet;
    float lastFired;

    void Start()
    {
        currentMagazineCount = magazineCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFired > 1 / FireRate)
            {
                lastFired = Time.time;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        freeLookCamera.m_YAxis.Value -= GetRecoil().y * Time.deltaTime;
        freeLookCamera.m_XAxis.Value -= GetRecoil().x * Time.deltaTime;
        currentRecoilBullet++;

        if (resetRecoilCoroutine != null)
            StopCoroutine(resetRecoilCoroutine);
        resetRecoilCoroutine = StartCoroutine(resetRecoil());
    }

    Coroutine resetRecoilCoroutine;
    IEnumerator resetRecoil()
    {
        isShooting = true;
        yield return new WaitForSeconds(recoilResetTimer);
        currentRecoilBullet = 0;
        isShooting = false;
    }
    Vector2 GetRecoil()
    {
        return recoil[currentRecoilBullet];
    }
}
