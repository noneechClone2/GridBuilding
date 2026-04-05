using System;
using System.Collections.Generic;
using GridBuilding.Grid.Cells;
using UnityEngine;

namespace GridBuilding.Buildings
{
    public class Building : MonoBehaviour
    {
        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public Rotation Rotation { get; set; }
        [field: SerializeField] public int XPosition { get; set; }
        [field: SerializeField] public int YPosition { get; set; }
        
        [SerializeField] private List<Cell> _occupiedCells;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Transform _buildingTransform;

        private BuildingMaterials _buildingMaterials;
        
        public Transform CenterTransform => _buildingTransform;
        public IReadOnlyCollection<Cell> OccupiedCells => _occupiedCells;

        public void Initialize(BuildingMaterials buildingMaterials)
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

            if (rotation == Rotation.Forward && Rotation == Rotation.Left)
                angle = 90;
            else
                angle = Mathf.Abs(Rotation - rotation);

            _buildingTransform.RotateAround(transform.position, Vector3.up, angle);

            if (angle == 90)
            {
                foreach (var cell in _occupiedCells)
                {
                    int x = cell.XPosition;
                    
                    cell.XPosition = cell.YPosition;
                    cell.YPosition = x * -1;
                }
            }
            else if (angle == 180)
            {
                foreach (var cell in _occupiedCells)
                {
                    cell.XPosition *= -1;
                    cell.YPosition *= -1;
                }
            }
            else
            {
                foreach (var cell in _occupiedCells)
                {
                    int x = cell.XPosition;
                    
                    cell.XPosition = cell.YPosition * -1;
                    cell.YPosition = x;
                }
            }
            
            Rotation = rotation;
        }

        public void SetPosition(Vector3Int position)
        {
            transform.position = position + new Vector3(0.5f, 0.5f, 0.5f);
            XPosition = position.x;
            YPosition = position.z;
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
            SetPosition(new Vector3Int(0, 0, 0));
        }
    }
}