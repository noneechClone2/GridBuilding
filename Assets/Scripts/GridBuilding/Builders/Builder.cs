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
            if (_currentBuilding == null)
                return;

            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_rayCastPlane.Raycast(_ray, out _distanceOnRay))
            {
                _buildingPosition = Vector3Int.FloorToInt(_ray.GetPoint(_distanceOnRay));

                _buildingPosition.x = (int)Mathf.Clamp(_buildingPosition.x, _gridView.StartPosition.x, _gridView.EndPosition.x);
                _buildingPosition.z = (int)Mathf.Clamp(_buildingPosition.z, _gridView.StartPosition.z, _gridView.EndPosition.z);

                _currentBuilding.SetPosition(_buildingPosition);
            }
        }

        public void PlaceBuilding()
        {
            if (_currentBuilding == null)
                return;

            _currentBuilding.Place();

            _currentBuilding = null;
        }

        public void OnBuildingCreated(Building building)
        {
            if (building == null) 
                return;

            _currentBuilding = building;
        }

        public void Initialize()
        {
            _rayCastPlane = new Plane(Vector3.up, _gridView.transform.position);

            _buildHandler.BuildingPlaced += PlaceBuilding;
            _buildHandler.Ticked += SetBuildingPosition;
            _buildHandler.BuildingCreated += OnBuildingCreated;
        }

        public void Dispose()
        {
            _buildHandler.BuildingPlaced -= PlaceBuilding;
            _buildHandler.Ticked -= SetBuildingPosition;
        }
    }
}