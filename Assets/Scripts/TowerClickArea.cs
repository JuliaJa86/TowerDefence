using UnityEngine;

public class TowerClickArea : MonoBehaviour
{
    //Set in Inspector
    public Tower _tower;

    private void OnMouseUpAsButton()
    {
        _tower.UpgradeTower();
    }
}
