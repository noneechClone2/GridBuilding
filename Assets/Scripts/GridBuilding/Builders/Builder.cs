using Grid;
using UnityEngine;
using Buildings;
using Zenject;
using System;

namespace Builders
{
    public class Builder : IInitializable, IDisposable
    {
        private GridView _gridView;
        private BuildHandler _buildHandler;
        private Building _currentBuilding;

        private Plane _rayCastPlane;
        private Vector3Int _buildingPosition;
        private Ray _ray;
        public Camera Camera;
        private float _distanceOnRay;

        public Builder(GridView gridView, BuildHandler buildHandler)
        {
            _gridView = gridView;
            _buildHandler = buildHandler;
        }

        public void SetBuildingPosition()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_rayCastPlane.Raycast(_ray, out _distanceOnRay))
            {
                _buildingPosition = Vector3Int.FloorToInt(_ray.GetPoint(_distanceOnRay));
                _buildingPosition.x = (int)Mathf.Clamp(_buildingPosition.x, _gridView.StartPosition.x, _gridView.EndPosition.x);
                _buildingPosition.y = 0;
                _buildingPosition.z = (int)Mathf.Clamp(_buildingPosition.z, _gridView.StartPosition.z, _gridView.EndPosition.z);

                _currentBuilding.SetPosition(_buildingPosition);
            }
        }

        public void OnBuildingPlaced()
        {
            _currentBuilding.Place();
        }

        public void OnBuildingCreated(Building building)
        {
            _currentBuilding = building;
        }

        public void Initialize()
        {
            _rayCastPlane = new Plane(Vector3.up, _gridView.StartPosition);

            _buildHandler.BuildingPlaced += OnBuildingPlaced;
            _buildHandler.Ticked += SetBuildingPosition;
            _buildHandler.BuildingCreated += OnBuildingCreated;
        }

        public void Dispose()
        {
            _buildHandler.BuildingPlaced -= OnBuildingPlaced;
            _buildHandler.Ticked -= SetBuildingPosition;
        }
    }
}