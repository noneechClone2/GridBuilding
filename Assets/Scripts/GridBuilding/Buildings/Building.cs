using Grid.Cells;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private List<Cell> _occupiedCells; 

        [SerializeField] private Collider _collider;
        [SerializeField] private Renderer _renderer;
        
        private BuildingData _buildingData;
        
        [field: SerializeField] public int _id { get; set; }
        
        public IReadOnlyCollection<Cell> OccupiedCells => _occupiedCells;
        public Vector3 HalfSize => _collider.bounds.size / 2;
        
        public void Place()
        {
            _renderer.SetMaterials(new() { _buildingData.DefaultMaterial, _buildingData.ShadesMaterial });
            _renderer.materials[0].SetColor("_Color", _buildingData.DefaultColor);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position + HalfSize;
        }

        public void ChangeAvailability(bool isAvailable)
        {
            if (isAvailable)
                _renderer.materials[0].SetColor("_Color", _buildingData.AvailableColor);
            else
                _renderer.materials[0].SetColor("_Color", _buildingData.UnavailableColor);
        }

        public void OnCreated(BuildingData buildingData)
        {
            _buildingData = buildingData;

            _renderer.SetMaterials(new() { _buildingData.EditModMaterial, _buildingData.ShadesMaterial });
            SetPosition(new Vector3(0, 0, 0));
        }
    }
}
