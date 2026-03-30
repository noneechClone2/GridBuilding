using UnityEngine;

namespace InputHandlers.PlayerInputState
{
    public class CurrentPlayerInputState
    {
        public PlayerInputStates InputState { get; private set; } = PlayerInputStates.GridBuildingMainWindow;

        public void ChangeInputState(PlayerInputStates newInputState)
        {
            Debug.Log(newInputState);
            InputState = newInputState;
        }
    }
}