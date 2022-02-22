
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shotPeriod = 0.2f;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private GameObject _flash;

    private float _timer;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Start()
    {
        HideFlash(false);
    }
    private async void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _shotPeriod)
        {
            if (Input.GetMouseButton(0))
            {
                _timer = 0f;
                GameObject newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
                newBullet.GetComponent<Rigidbody>().velocity = _spawn.forward * _bulletSpeed;
                _shotSound.Play();
                HideFlash(true);

                await UniTask.Delay(TimeSpan.FromSeconds(0.2));
                HideFlash(false);
            }
        }
    }

    private void HideFlash(bool isHide)
    {
        _flash.SetActive(isHide);
    }
}
