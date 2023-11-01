using Cinemachine;
using UnityEngine;

namespace RehtseStudio.FreeLookCamera3rdPersonCharacterController.Scripts
{
    public class CinemachineMobileInputFeeder : MonoBehaviour
    {


        //[SerializeField] private UITouchPanel _touchPanelInput;
        [SerializeField] private UITouchPanel _touchInput;

        private Vector2 _lookInput;

        [SerializeField] private float _touchSpeedSensitivityX = 3f;
        [SerializeField] private float _touchSpeedSensitivityY = 3f;

        private string _touchXMapTo = "Mouse X";
        private string _touchYMapTo = "Mouse Y";

        void Start()
        {

            CinemachineCore.GetInputAxis = GetInputAxis;
            _touchInput = GameObject.Find("UI_Mobile").gameObject.transform.GetChild(1).gameObject.GetComponent<UITouchPanel>();

        }

        private float GetInputAxis(string axisName)
        {

            _lookInput = _touchInput.PlayerJoystickOutputVector();

            if (axisName == _touchXMapTo)
                return _lookInput.x / _touchSpeedSensitivityX;

            if (axisName == _touchYMapTo)
                return _lookInput.y / _touchSpeedSensitivityY;

            return Input.GetAxis(axisName);
        }
    }
}

