using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderersPool : MonoBehaviour
{
    public List<LineRenderer> Renderers = new List<LineRenderer>();

    private void Update()
    {
        for (var index = 0; index < Battle.Instance.PlayerShips.Count; index++)
        {
            var ship = Battle.Instance.PlayerShips[index];
            
            if (ship.selectedTarget)
            {
                Renderers[index].SetPosition(0, ship.transform.position);
                Renderers[index].SetPosition(1, ship.selectedTarget.position);
            }
        }
    }
}
