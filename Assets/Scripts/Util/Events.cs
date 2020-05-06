using UnityEngine.Events;
using UnityEngine;


namespace SingleMagicMoba
{
    public class Events
    {
        [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }

        [System.Serializable] public class EventMobDeath : UnityEvent<MobType, Vector3> { }

        [System.Serializable] public class EventIntegerEvent : UnityEvent<int> { }

        [System.Serializable] public class EventChangeTheAbilitySpell : UnityEvent<bool> { }

        [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState,GameManager.GameState> { }

        [System.Serializable] public class OnClickEnvironmentVector3 : UnityEvent<Vector3> { }

        [System.Serializable] public class SkillIsChangedBySkillPickedUp : UnityEvent<bool> { }
    }

}