using UnityEngine;

namespace View
{
    public class GameObjectSimplePoolObject<T> : SimplePoolObject<T> where T : Component
    {
        public override void Push(T obj)
        {
            obj.gameObject.SetActive(false);
            base.Push(obj);
        }
    }
}