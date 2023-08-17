using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayCardsPhaseControllerSettings : PhaseInputControllerSettings
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
