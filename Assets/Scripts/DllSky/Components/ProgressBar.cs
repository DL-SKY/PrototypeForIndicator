using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649

namespace DllSky.Components
{
    public class ProgressBar : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float fillAmount;
        public float FillAmount
        {
            get { return fillAmount; }
            set { fillAmount = value; SetFill(); }
        }

        [SerializeField]
        private Image fillImage;
        #endregion

        #region Public methods
        public void SetColor(Color _color)
        {
            fillImage.color = _color;
        }
        #endregion

        #region Private methods
        private void SetFill()
        {
            fillImage.fillAmount = fillAmount;
        }
        #endregion
    }
}
