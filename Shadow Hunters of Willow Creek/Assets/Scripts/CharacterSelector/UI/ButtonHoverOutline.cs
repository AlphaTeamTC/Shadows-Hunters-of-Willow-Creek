using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharacterSelector.UI
{
    public class ButtonHoverOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _outlineImage;

        private void Start()
        {
            _outlineImage.enabled = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _outlineImage.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        { 
            _outlineImage.enabled = false;
        }
    }
}