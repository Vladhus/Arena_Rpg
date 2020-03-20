using UnityEngine;


namespace SingleMagicMoba
{
    public interface IDestructible
    {
        void OnDestruction(GameObject destroyer);
    }

}