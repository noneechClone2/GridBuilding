using System.Collections.Generic;
using GridBuilding.Grid.Cells;
using UnityEngine;

namespace GridBuilding.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private List<Cell> _occupiedCells;
        [SerializeField] private Collider _collider;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Transform _buildingTransform;

        private BuildingMaterials _buildingMaterials;

        [field: SerializeField] public int Id { get; set; }
        public Rotation Rotation { get; private set; }

        public IReadOnlyCollection<Cell> OccupiedCells => _occupiedCells;
        public Vector3 HalfSize => _collider.bounds.size / 2;

        public void Init(BuildingMaterials buildingMaterials)
        {
            _buildingMaterials = buildingMaterials;
        }

        public void Place()
        {
            _renderer.SetMaterials(new() { _buildingMaterials.DefaultMaterial, _buildingMaterials.ShadesMaterial });
            _renderer.materials[0].SetColor("_Color", _buildingMaterials.DefaultColor);
        }

        public void Rotate(Rotation rotation)
        {
            int angle;
            
            if(Rotation == Rotation.Left && rotation == Rotation.Forward)
                angle = 90;
            else
                angle = Mathf.Abs(Rotation - rotation);
            
            // Debug.Log(angle + " " + rotation);

            _buildingTransform.RotateAround(transform.position, Vector3.up, angle);

            if (angle == 90)
            {
                foreach (var cell in _occupiedCells)
                {
                    int x = cell.XPosition;
                    
                    cell.XPosition = cell.YPosition;
                    cell.YPosition = x;
                    
                    cell.YPosition = cell.YPosition * -1;
                }
            }
            
            Rotation = rotation;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position + new Vector3(0.5f, 0.5f, 0.5f);
        }

        public void ChangeAvailability(bool isAvailable)
        {
            if (isAvailable)
                _renderer.materials[0].SetColor("_Color", _buildingMaterials.AvailableColor);
            else
                _renderer.materials[0].SetColor("_Color", _buildingMaterials.UnavailableColor);
        }

        public void OnCreated()
        {
            _renderer.SetMaterials(new() { _buildingMaterials.EditModMaterial, _buildingMaterials.ShadesMaterial });
            Rotation = Rotation.Forward;
            SetPosition(new Vector3(0, 0, 0));
        }
    }
}