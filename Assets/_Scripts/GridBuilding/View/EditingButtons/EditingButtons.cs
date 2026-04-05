using GridBuilding.Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.GridBuilding.View
{
    public class EditingButtons : MonoBehaviour
    {
        [SerializeField] private RectTransform _continueButton;
        [SerializeField] private RectTransform _editButton;
        [SerializeField] private RectTransform _deleteButton;
        
        private Building _currentBuilding;
        
        public void OnContinueButtonClicked()
        {
            HideButtons();
        }
        
        public void OnEditButtonClicked()
        {
            HideButtons();
        }
        
        public void OnDeleteButtonClicked()
        {
            HideButtons();
            Destroy(_currentBuilding.gameObject);
        }

        public void ShowButtons(Vector2 canvasBuildingPosition)
        {
            Debug.Log(canvasBuildingPosition);
            _continueButton.anchoredPosition = canvasBuildingPosition;
            _continueButton.gameObject.SetActive(true);
        }

        private void HideButtons()
        {
            _continueButton.gameObject.SetActive(false);
            _editButton.gameObject.SetActive(false);
            _deleteButton.gameObject.SetActive(false);
        }

        private void BuildingClicked(Building building)
        {
            
        }
    }
}