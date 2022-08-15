using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ItemViev
    {
        private Image _image;

        public Sprite ImageSprite
        {
            get { return _image.sprite;}
            set {_image.sprite = value;}
        }
    }
}