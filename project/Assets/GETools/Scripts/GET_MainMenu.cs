using UnityEngine;
using System.Collections;

namespace GETools
{
    public class GET_MainMenu : MonoBehaviour
    {

        void Start()
        {

        }

        void Update()
        {

        }

        /// <summary>
        /// 主菜单处理
        /// </summary>
        /// <param name="aid">动作Id</param>
        public void OnMemuClick(int aid)
        {
            LogMgr.D("onClick action :" + aid);
            switch (aid)
            {
                case 1:
                    OnRoleEdit();
                    break;
                case 2:
                    OnWeaponEdit();
                    break;
            }
        }

        private void OnRoleEdit()
        {
            LogMgr.D("OnRoleEdit!");
        }

        private void OnWeaponEdit()
        {
        }


        private void OnActiveChanged()
        {
            LogMgr.D("in GET_MainMenu OnActiveChanged!");
        }
    }
}