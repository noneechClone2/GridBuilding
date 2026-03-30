using UnityEngine;

namespace InputHandlers.PlayerInputState
{
    public class GridBuildingPlayerInputStateChanger : MonoBehaviour
    {
        private CurrentPlayerInputState _currentPlayerInputState;

        public void Initialize(CurrentPlayerInputState currentPlayerInputState)
        {
            _currentPlayerInputState = currentPlayerInputState;
        }

        public void EnterBuildingEditor()
        {
            _currentPlayerInputState.ChangeInputState(PlayerInputStates.BuildingEditor);
        }
        
        public void ExitBuildingEditor()
        {
            _currentPlayerInputState.ChangeInputState(PlayerInputStates.GridBuildingMainWindow);
        }
    }
}