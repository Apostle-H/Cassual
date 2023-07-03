using System.Collections;
using TMPro;
using UnityEngine;
using Utils.Services;

namespace Buildings.Camp
{
    public class Camp : MonoBehaviour
    {
        [SerializeField] private int startSpawnAmount;
        [SerializeField] private float spawnFrequency;
        [SerializeField] private float offset;

        [SerializeField] private GameObject prefab;
            
        [SerializeField] private int spawnAmountUpgradeStep;

        [SerializeField] private TextMeshProUGUI lvlText;
        
        private int _spawnAmount;
        
        public int Level { get; private set; }
        
        private void Awake()
        {
            Level = 1;
            _spawnAmount = startSpawnAmount;

            lvlText.text = Level.ToString();

            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnFrequency);
                Spawn();
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < _spawnAmount; i++)
            {
                Vector3 spawnPos = MathService.RadiusPlacePosition(transform.position, offset, i + 1);
                Instantiate(prefab).transform.position += new Vector3(spawnPos.x, 0, spawnPos.z);
            }
        }

        public void Upgrade()
        {
            Level++;
            _spawnAmount += spawnAmountUpgradeStep;
            
            lvlText.text = Level.ToString();
        }
    }
}