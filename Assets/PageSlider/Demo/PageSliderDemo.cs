#region Includes
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

namespace TS.PageSlider.Demo
{
    public class PageSliderDemo : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private PageSlider _slider;
        [SerializeField] private SliderPage _pagePrefab;

        [Header("Configuration")]
        private List<SliderItem> _items;

        #endregion

        private IEnumerator InitImage()
        {
            yield return new WaitUntil(() => LevelSelection.Instance != null);
            yield return new WaitUntil(() => LevelSelection.Instance._currentLevelSelect != null);
            Debug.Log("PageSlider Tải lại ảnh động vật");
            var level = LevelSelection.Instance._currentLevelSelect;

            if(_items == null)
            {
                _items = new List<SliderItem>();
            }
            else 
            {
                _items.Clear();
            }

            foreach(var cfimage in level.levelAnimalImageGallery.animalImageItems)
            {
                SliderItem sliderItem = new SliderItem() {
                    _text = cfimage.text,
                    _image = cfimage.image,
                };

                _items.Add(sliderItem);

                Debug.Log("PageSlider cfname" + cfimage.text);
            }
            Debug.Log(_pagePrefab == null);
            for (int i = 0; i < _items.Count; i++)
            {
                var page = Instantiate(_pagePrefab);
                page.Image = _items[i].Image;

                _slider.AddPage((RectTransform)page.transform);
            }
        }

        private void Start()
        {
            StartCoroutine(InitImage());
        }
    }

    [Serializable]
    public class SliderItem
    {
        public string _text;
        public Sprite _image;

        public string Text { get { return _text; } }
        public Sprite Image { get { return _image; } }
    }
}
