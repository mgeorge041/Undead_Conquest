using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputControllerEventManager
{
    public void Clear()
    {
        onSetControllerSettings.Clear();
        onLeftClick.Clear();
        onRightClick.Clear();
        onHover.Clear();
        onPressKeyboardArrows.Clear();
        onScroll.Clear();
    }


    public GameEvent<PhaseInputControllerSettings> onSetControllerSettings { get; private set; } = new GameEvent<PhaseInputControllerSettings>();
    public GameEvent<Hex> onLeftClick { get; private set; } = new GameEvent<Hex>();
    public GameEvent<Hex> onRightClick { get; private set; } = new GameEvent<Hex>();
    public GameEvent<Hex> onHover { get; private set; } = new GameEvent<Hex>();
    public GameEvent<float, float> onPressKeyboardArrows { get; private set; } = new GameEvent<float, float>();
    public GameEvent<float> onScroll { get; private set; } = new GameEvent<float>();
}
