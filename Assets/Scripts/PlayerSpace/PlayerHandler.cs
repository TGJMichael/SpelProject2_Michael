using UnityEngine;

namespace PlayerSpace
{

public class PlayerHandler : MonoBehaviour
{
        public PlayerStats Player;

        [SerializeField]
        private Canvas m_Canvas;   // drag and drop the panel part of the canvas - will possibly change names of it, don't know yet if the whole canvas or only the part that will be the skill tree.
        private bool m_SeeCanvas;

        private void Update()
        {
            // if you press the tab key
            if (Input.GetKeyDown("tab"))
            {
                if (m_Canvas)
                {
                    m_SeeCanvas = !m_SeeCanvas;
                    m_Canvas.gameObject.SetActive(m_SeeCanvas); // Display or not the canvas (folowing the state of the bool)
                }
            }
        }
    }
}
