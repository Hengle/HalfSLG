﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ELGame
{
    public class UIViewBattleFieldUnitInfo 
        : UIViewBase
    {
        [SerializeField] TextMeshProUGUI tmpBattleUnitInfo;
        [SerializeField] TextMeshProUGUI tmpGridUnitInfo;
        [SerializeField] GameObject separateLine;

        private GridUnit focusGridUnit;
        private BattleUnit focusBattleUnit;

        private BattleUnit FocusBattleUnit
        {
            set
            {
                if (focusBattleUnit != null && focusBattleUnit.battleUnitRenderer) 
                    focusBattleUnit.battleUnitRenderer.linkedUnitInfoView = null;

                focusBattleUnit = value;
                if (focusBattleUnit != null && focusBattleUnit.battleUnitRenderer) 
                    focusBattleUnit.battleUnitRenderer.linkedUnitInfoView = this;
            }
        }

        protected override void InitUIObjects()
        {
            base.InitUIObjects();


        }

        protected override void UpdateArguments(params object[] args)
        {
            if (args.Length == 0)
            {
                ErrorClose("Args error");
                return;
            }

            focusGridUnit = args[0] as GridUnit;
            FocusBattleUnit = args[1] as BattleUnit;

            if (focusGridUnit == null && focusBattleUnit == null)
            {
                Close();
                return;
            }
        }
        
        public override void UpdateView()
        {
            base.UpdateView();

            if (focusGridUnit != null)
                tmpGridUnitInfo.text = string.Format("GridUnit:\n{0}", focusGridUnit.ToString());

            if (focusBattleUnit != null)
                tmpBattleUnitInfo.text = string.Format("BattleUnit:\n{0}\nHp:{1}/{2}\nAtk:{3}\nMobility:{4}",
                    focusBattleUnit.ToString(),
                    focusBattleUnit.hp, focusBattleUnit.maxHp,
                    focusBattleUnit.atk,
                    focusBattleUnit.mobility
                    );

            //设置显示
            separateLine.SetActive(focusGridUnit != null && focusBattleUnit != null);
            tmpGridUnitInfo.gameObject.SetActive(focusGridUnit != null);
            tmpBattleUnitInfo.gameObject.SetActive(focusBattleUnit != null);
        }

        protected override void CloseWithEvent(BaseEventData eventData)
        {
            PointerEventData ped = eventData as PointerEventData;
            if (ped != null)
            {
                BattleFieldRenderer.Instance.ClickedBattleField(ped.position);
            }
        }
    }
}