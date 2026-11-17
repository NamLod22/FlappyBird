using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _pipePrefab;
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 0.4f;
    [SerializeField] private float _destroyTime = 3f;

    private float _timer = 0f;

    private void OnEnable()
    {
        ResetSpawner();
        SpawnPipe();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _maxTime)
        {
            SpawnPipe();
            _timer = 0f;
        }
    }

    public void ResetSpawner()
    {
        _timer = 0f;
    }

    private void SpawnPipe()
    {
        if (_pipePrefab == null) return;
        float randomHeight = Random.Range(-_heightRange, _heightRange);
        Vector3 spawnPosition = transform.position + Vector3.up * randomHeight;

        GameObject pipe = Instantiate(_pipePrefab, spawnPosition, Quaternion.identity);
        Destroy(pipe, _destroyTime);
    }
}
