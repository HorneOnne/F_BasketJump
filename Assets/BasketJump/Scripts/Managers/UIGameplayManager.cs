﻿using UnityEngine;

namespace BasketJump
{
    public class UIGameplayManager : MonoBehaviour
    {
        public static UIGameplayManager Instance { get; private set; }

        public UIGameplay UIGameplay;
        public UIPause UIPause;
        public UIGameover UIGameover;



        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            CloseAll();
        }

        public void CloseAll()
        {
            DisplayPauseMenu(false);
            DisplayGameoverMenu(false);

        }

        public void DisplayPauseMenu(bool isActive)
        {
            UIPause.DisplayCanvas(isActive);
        }

        public void DisplayGameoverMenu(bool isActive)
        {
            UIGameover.DisplayCanvas(isActive);
        }
    }
}
