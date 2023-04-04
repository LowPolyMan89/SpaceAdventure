using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderersPool : MonoBehaviour
{
    public List<LineRenderer> Renderers = new List<LineRenderer>();
    public Material PlayerTarget;
    public Material EnemyTarget;

    private void Update()
    {
        Renderers.ForEach(t => t.gameObject.SetActive(false));
        int indx = 0;
        
        for (var index = 0; index < Battle.Instance.PlayerShips.Count; index++)
        {
            var ship = Battle.Instance.PlayerShips[index];
            
            if (ship.selectedTarget)
            {
                indx++;
                Renderers[index].gameObject.SetActive(true);
                Renderers[index].material = PlayerTarget;
                Renderers[index].SetPosition(0, ship.transform.position);
                Renderers[index].SetPosition(1, ship.selectedTarget.position);
            }
        }
        
        /*  
        for (var index = indx; index < Battle.Instance.EnemyShips.Count; index++)
        {
            var ship = Battle.Instance.EnemyShips[index];
            
            if (ship.selectedTarget)
            {
                Renderers[index].gameObject.SetActive(true);
                Renderers[index].material = EnemyTarget;
                Renderers[index].SetPosition(0, ship.transform.position);
                Renderers[index].SetPosition(1, ship.selectedTarget.position);
            }
        }
        */
    }
}
