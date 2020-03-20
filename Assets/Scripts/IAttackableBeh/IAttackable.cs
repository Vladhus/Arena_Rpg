using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SingleMagicMoba
{
    public interface IAttackable
    {
        void OnAttack(GameObject attacker, Attack attack);
    }

}