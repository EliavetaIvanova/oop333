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
        private static double radius;
        // расстояние от панели рисования до верхней границы окна.
        private static double marginTopPanel;
        // расстояние от панели рисования до левой границы окна.
        private static double marginLeftPanel;

        // Конструктор классса (приватный, чтобы не создать экземпляр класса)
        private Config()
        {
        }

        // геттер
        public static double getRadius()
        {
            return radius;
        }

        // геттер
        public static double getMarginTopPanel()
        {
            return marginTopPanel;
        }

        // геттер
        public static double getMarginLeftPanel()
        {
            return marginLeftPanel;
        }

        // сеттер
        public static void setConfig(double newRadius, double newMarginTopPanel, double newMarginLeftPanel)
        {
            radius = newRadius;
            marginTopPanel = newMarginTopPanel;
            marginLeftPanel = newMarginLeftPanel;
        }
    }
}