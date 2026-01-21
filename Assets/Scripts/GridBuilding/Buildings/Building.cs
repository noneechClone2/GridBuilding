using Grid.Cells;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
        private readonly Color DefaultColor = new Color(10, 10, 10);

        [SerializeField] private List<Cell> _occupiedCells; 

        [SerializeField] private Collider _colider;
        [SerializeField] private Renderer _renderer;

        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _shadesMaterial;
        [SerializeField] private Material _editModMaterial;

        [SerializeField] private Color _availableColor;
        [SerializeField] private Color _unavailableColor;

        [SerializeField] public BuildingTypes _type;

        public BuildingTypes Type => _type;
        public IReadOnlyCollection<Cell> OccupiedCells => _occupiedCells;

        public void Place()
        {
            _renderer.SetMaterials(new() { _defaultMaterial, _shadesMaterial });
            _renderer.materials[0].SetColor("_Color", DefaultColor);
        }

        public void SetPosition(Vector3Int position)
        {
            transform.position = position + _colider.bounds.size / 2;
        }

        public void ChangeAvailability(bool isAvailable)
        {
            if (isAvailable)
                _renderer.materials[0].SetColor("_Color", _availableColor);
            else
                _renderer.materials[0].SetColor("_Color", _unavailableColor);
        }

        public void OnCreated()
        {
            _renderer.SetMaterials(new() { _editModMaterial, _shadesMaterial });
        }
    }
}
