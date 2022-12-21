using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    // класс конфигурации приложения
    class Config
    {
        // расстояние до точек, при котором будут соединяться линии к другим линиям или к окну.
        private static double rad;
        // расстояние от панели рисования до верхней границы окна.
        private static double toppanel;
        // расстояние от панели рисования до левой границы окна.
        private static double leftpanel;

        // Конструктор классса (приватный, чтобы не создать экземпляр класса)
        private Config()
        {
        }

        // геттер
        public static double getRadius()
        {
            return rad;
        }

        // геттер
        public static double getMarginTopPanel()
        {
            return toppanel;
        }

        // геттер
        public static double getMarginLeftPanel()
        {
            return leftpanel;
        }

        // сеттер
        public static void setConfig(double newRadius, double newMarginTopPanel, double newMarginLeftPanel)
        {
            rad = newRadius;
            toppanel = newMarginTopPanel;
            leftpanel = newMarginLeftPanel;
        }
    }
}