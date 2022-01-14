using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSpace
{

    public class SkillDisplay : MonoBehaviour
    {
        // Get the Scriptable Object for Skill
        public Skills skill;
        // Get the UI components
        public Text skillName;
        public Text SkillDescription;
        public Image skillIcon;
        public Text skillLevel;
        public Text skillXPNeeded;
        public Text skillAttribute;
        public Text skillAttrAmount;

        [SerializeField]
        private PlayerStats m_PlayerHandler;

        void Start()
        {
            m_PlayerHandler = this.GetComponentInParent<PlayerHandler>().Player;
            //listener for the XP change - both of these make it so the changes to the icons are updated automaticaly, otherwise they would only update if you close and open the window.
            m_PlayerHandler.onXPChange += ReactToChange;
            //Listener for the level change
            m_PlayerHandler.onLevelChange += ReactToChange;

            if (skill)
            {
                skill.SetValues(this.gameObject, m_PlayerHandler);
            }

            EnableSkills();
        }

        private void EnableSkills()
        {
            //if the player has the skill already, then show it as enabled
            if (m_PlayerHandler && skill && skill.EnableSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
            else if (m_PlayerHandler && skill && skill.CheckSkills(m_PlayerHandler))
            {
                this.GetComponent<Button>().interactable = true;
                this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);   // change color by hiding different colors. not sure what this means though, took in straight form tutorial. bad fix though.
            }
            else
            {
                TurnOffSkillIcon();
            }
        }

        private void OnEnable()
        {
            EnableSkills();
        }

        // Method to be used when you click the Skill icon
        public void GetSkill()
        {
            if (skill.GetSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
        }

        //Turn on the Skill Icon - stop it from being clickable and disable the UI elements that make it change colour
        private void TurnOnSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
            this.transform.Find("IcontParent").Find("Disabled").gameObject.SetActive(false);
        }

        //Turn off the Skill Icon so it cannot be used - stop it from being clickable and enable the UI elements that make it change color
        private void TurnOffSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Avaliable").gameObject.SetActive(true);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            m_PlayerHandler.onXPChange -= ReactToChange;
        }

        //event for when listener is woken
        void ReactToChange()
        {
            EnableSkills();
        }
    }
}
