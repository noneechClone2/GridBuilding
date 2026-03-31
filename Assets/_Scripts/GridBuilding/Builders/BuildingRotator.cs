using System;
using System.Collections.Generic;
using GridBuilding.Buildings;
using InputHandlers;

namespace GridBuilding.Builders
{
    public class BuildingRotator : IDisposable
    {
        private Dictionary<Rotation, Rotation> _nextRotation;

        private BuildHandler _buildHandler;
        private IInputHandler _inputHandler;

        private Building _currentBuilding;

        public BuildingRotator(BuildHandler buildHandler, IInputHandler inputHandler)
        {
            _buildHandler = buildHandler;
            _inputHandler = inputHandler;
        }

        public void Initialize()
        {
            _buildHandler.BuildingCreated += OnBuildingCreated;
            _inputHandler.BuildingRotated += Rotate;
            
            FillCollection();
        }

        private void FillCollection()
        {
            _nextRotation = new Dictionary<Rotation, Rotation>()
            {
                { Rotation.Forward, Rotation.Right },
                { Rotation.Right, Rotation.Backward },
                { Rotation.Backward, Rotation.Left },
                { Rotation.Left, Rotation.Forward }
            };
        }

        private void Rotate()
        {
            _currentBuilding.Rotate(_nextRotation[_currentBuilding.Rotation]);
        }

        private void OnBuildingCreated(Building building)
        {
            _currentBuilding = building;
        }

        public void Dispose()
        {
        }
    }
}