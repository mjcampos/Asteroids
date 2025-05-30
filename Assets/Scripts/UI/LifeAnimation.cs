using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class LifeAnimation : MonoBehaviour {
        [SerializeField] Sprite[] spriteArray;
        [SerializeField] float changeSpeed = .1f;
        
        Image _image;
        int _spriteArrayLength;

        void Start() {
            _image = GetComponent<Image>();
            _spriteArrayLength = spriteArray.Length;
            
            StartCoroutine(ChangeSprite());
        }

        IEnumerator ChangeSprite() {
            int currentSpriteIndex = 0;

            do {
                Sprite currentSprite = spriteArray[currentSpriteIndex];
                _image.sprite = currentSprite;

                yield return new WaitForSecondsRealtime(changeSpeed);

                currentSpriteIndex++;

                if (currentSpriteIndex >= _spriteArrayLength)
                {
                    currentSpriteIndex = 0;
                }
            } while (true);
        }
    }
}
