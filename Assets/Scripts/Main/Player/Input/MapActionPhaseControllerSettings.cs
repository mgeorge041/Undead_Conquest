using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapActionPhaseControllerSettings : PhaseInputControllerSettings
{
    // Update is called once per frame
    public override void Update()
    {
        CheckLeftClick();
        CheckRightClick();
        CheckHover();
        CheckKeyboardArrows();
        CheckScroll();
    }
}
