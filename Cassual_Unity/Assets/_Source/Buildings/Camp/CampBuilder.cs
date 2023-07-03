using BankSystem;
using InputSystem;
using UI.Log.Signals;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Services;
using Zenject;

namespace Buildings.Camp
{
    public class CampBuilder : MonoBehaviour
    {
        [SerializeField] private LayerMask buildableMask;
        
        [SerializeField] private GameObject campPrefab;
        [SerializeField] private Transform campsHolder;

        [SerializeField] private int buildCost;
        [SerializeField] private int upgradeCost;
        [SerializeField] private LayerMask campsMask;

        [SerializeField] private Bank bank;
        
        private MainActions.BuildingsActions _buildingsActions;

        private LogMessageSignal _logMessageSignal;

        [Inject]
        public void Init(MainActions.BuildingsActions buildingsActions)
        {
            _buildingsActions = buildingsActions;

            _logMessageSignal = deVoid.Utils.Signals.Get<LogMessageSignal>();
            
            Bind();
        }

        private void Bind() => _buildingsActions.SetUpCamp.canceled += Build;
        
        private void Build(InputAction.CallbackContext ctx)
        {
            Vector2 screenPoint = _buildingsActions.Position.ReadValue<Vector2>();
            
            RaycastHit hit = PhysicsService.RayFromCamera(screenPoint, campsMask);
            if (hit.transform != null && hit.transform.TryGetComponent(out Camp camp))
            {
                if (bank.Amount >= (camp.Level + 1) * upgradeCost)
                {
                    camp.Upgrade();
                    bank.Add(-(camp.Level + 1) * upgradeCost);
                
                    _logMessageSignal.Dispatch("Camp Upgraded");
                    return;
                }
                
                _logMessageSignal.Dispatch("Not Enough Money To Upgrade The Camp");
                return;
            }
            
            

            hit = PhysicsService.RayFromCamera(screenPoint, buildableMask);
            if (hit.transform == null)
            {
                _logMessageSignal.Dispatch("Can Not Build A Camp Here");
                return;
            }
            
            if (bank.Amount < buildCost)
            {
                _logMessageSignal.Dispatch("Not Enough Money To Build A Camp");
                return;
            }
            
            Vector3 position = hit.point;

            Instantiate(campPrefab, campsHolder).transform.position = new Vector3(position.x, 0, position.z);
            hit.transform.gameObject.SetActive(false);
            
            bank.Add(-buildCost);
            
            _logMessageSignal.Dispatch("Camp Built");
        }
    }
}