using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Editor
{
    // пользовательский класс стрелки первого типа
    class Arrow1 : Arrow
    {
        // ближняя линия от конца стрелки
        private Line endNearLine;
        // ближняя линия от начала стрелки
        private Line beginNearLine;

        // конструктор класса
        public Arrow1(Line line, Line endDistantLine, Line beginDistantLine, Line endNearLine, Line beginNearLine)
        {
            this.line = line;
            this.endDistantLine = endDistantLine;
            this.beginDistantLine = beginDistantLine;
            this.endNearLine = endNearLine;
            this.beginNearLine = beginNearLine;
        }

        public override void setPoint1(double x1, double y1)
        {
            this.line.X1 = x1 - Config.getMarginLeftPanel();
            this.line.Y1 = y1 - Config.getMarginTopPanel();
            this.moveArrow();
        }

        public override void setPoint1(Point point)
        {
            this.line.X1 = point.X - Config.getMarginLeftPanel();
            this.line.Y1 = point.Y - Config.getMarginTopPanel();
            this.moveArrow();
        }

        public override void setPoint2(double x2, double y2)
        {
            this.line.X2 = x2 - Config.getMarginLeftPanel();
            this.line.Y2 = y2 - Config.getMarginTopPanel();
            this.moveArrow();
        }

        public override void setPoint2(Point point)
        {
            this.line.X2 = point.X - Config.getMarginLeftPanel();
            this.line.Y2 = point.Y - Config.getMarginTopPanel();
            this.moveArrow();
        }

        // метод, меняющий расположение стрелок на концах
        private void moveArrow()
        {
            double d = Math.Sqrt(Math.Pow(this.getPoint2().X - this.getPoint1().X, 2) + Math.Pow(this.getPoint2().Y - this.getPoint1().Y, 2));

            double X = this.getPoint2().X - this.getPoint1().X;
            double Y = this.getPoint2().Y - this.getPoint1().Y;

            double X3 = this.getPoint2().X - (X / d) * 10;
            double Y3 = this.getPoint2().Y - (Y / d) * 10;

            double Xp = this.getPoint2().Y - this.getPoint1().Y;
            double Yp = this.getPoint1().X - this.getPoint2().X;

            double X4 = X3 + (Xp / d) * 11;
            double Y4 = Y3 + (Yp / d) * 11;
            double X5 = X3 - (Xp / d) * 11;
            double Y5 = Y3 - (Yp / d) * 11;

            this.endNearLine.X1 = X4;
            this.endNearLine.Y1 = Y4;

            this.endNearLine.X2 = X5;
            this.endNearLine.Y2 = Y5;

            X3 = this.getPoint2().X - (X / d) * 20;
            Y3 = this.getPoint2().Y - (Y / d) * 20;

            Xp = this.getPoint2().Y - this.getPoint1().Y;
            Yp = this.getPoint1().X - this.getPoint2().X;

            X4 = X3 + (Xp / d) * 11;
            Y4 = Y3 + (Yp / d) * 11;
            X5 = X3 - (Xp / d) * 11;
            Y5 = Y3 - (Yp / d) * 11;

            this.endDistantLine.X1 = X4;
            this.endDistantLine.Y1 = Y4;

            this.endDistantLine.X2 = X5;
            this.endDistantLine.Y2 = Y5;

            X = -1 * X;
            Y = -1 * Y;

            X3 = this.getPoint1().X - (X / d) * 10;
            Y3 = this.getPoint1().Y - (Y / d) * 10;

            Xp = -1 * Xp;
            Yp = -1 * Yp;

            X4 = X3 + (Xp / d) * 11;
            Y4 = Y3 + (Yp / d) * 11;
            X5 = X3 - (Xp / d) * 11;
            Y5 = Y3 - (Yp / d) * 11;

            this.beginNearLine.X1 = X4;
            this.beginNearLine.Y1 = Y4;

            this.beginNearLine.X2 = X5;
            this.beginNearLine.Y2 = Y5;


            X3 = this.getPoint1().X - (X / d) * 20;
            Y3 = this.getPoint1().Y - (Y / d) * 20;


            X4 = X3 + (Xp / d) * 11;
            Y4 = Y3 + (Yp / d) * 11;
            X5 = X3 - (Xp / d) * 11;
            Y5 = Y3 - (Yp / d) * 11;

            this.beginDistantLine.X1 = X4;
            this.beginDistantLine.Y1 = Y4;

            this.beginDistantLine.X2 = X5;
            this.beginDistantLine.Y2 = Y5;
        }

        override public void remove()
        {
            Canvas canvas = (Canvas)this.line.Parent;
            canvas.Children.Remove(this.line);
            canvas.Children.Remove(this.endDistantLine);
            canvas.Children.Remove(this.beginDistantLine);
            canvas.Children.Remove(this.endNearLine);
            canvas.Children.Remove(this.beginNearLine);
        }
    }
}