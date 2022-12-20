using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Media;

namespace Editor
{
    // пользовательский класс стрелки (несколько линий и ломанных)
    abstract public class Arrow
    {
        // главная линия
        protected Line line;
        // дальняя от конца перекрестная линия
        protected Line endDistantLine;
        // дальняя от начала перекрестная линия
        protected Line beginDistantLine;

        // конструктор класса
        public Arrow() { }

        // метод, меняющий позицию начальной точки
        public abstract void setPoint1(double x1, double y1);
        // метод, меняющий позицию начальной точки
        public abstract void setPoint1(Point point);
        // метод, меняющий позицию конечной точки
        public abstract void setPoint2(double x2, double y2);
        // метод, меняющий позицию конечной точки
        public abstract void setPoint2(Point point);
        // метод, удаляющий все линиии и ломанные из поля для рисования
        public abstract void remove();
        // геттер точки, указывающий на позицию начала стрелки
        public Point getPoint1()
        {
            return new Point(this.line.X1, this.line.Y1);
        }
        // геттер точки, указывающий на позицию конца стрелки
        public Point getPoint2()
        {
            return new Point(this.line.X2, this.line.Y2);
        }

        // метод сравнивающий строку с именем линии
        public bool equalName(string name)
        {
            return this.line.Name == name;
        }
        // геттер имени
        public string name()
        {
            return this.line.Name;
        }

        // проверяет, находится ли стрелка около точки. Если находится, то возвращает
        // 1 - рядом с началом стрелки, 2 - рядом с концом стрелки, 0 - около точки нет стрелки
        public int equalLineWithPoint(Point point)
        {
            int pointLineIndex = 0;
            if (Math.Abs(point.X - this.line.X2 - Config.getMarginLeftPanel()) < Config.getRadius() / 2 && Math.Abs(point.Y - this.line.Y2 - Config.getMarginTopPanel()) < Config.getRadius() / 2)
            {
                pointLineIndex = 2;
            }
            else if (Math.Abs(point.X - this.line.X1 - Config.getMarginLeftPanel()) < Config.getRadius() / 2 && Math.Abs(point.Y - this.line.Y1 - Config.getMarginTopPanel()) < Config.getRadius() / 2)
            {
                pointLineIndex = 1;
            }

            return pointLineIndex;
        }

        // статический метод, соединяющий две стрелки
        public static bool concateLines(Arrow firstLine, Arrow secondLine)
        {
            bool result = false;
            if (Math.Abs(firstLine.getPoint2().X - secondLine.getPoint2().X) < Config.getRadius() && Math.Abs(firstLine.getPoint2().Y - secondLine.getPoint2().Y) < Config.getRadius())
            {
                firstLine.setPoint2(secondLine.getPoint2().X + Config.getMarginLeftPanel(), secondLine.getPoint2().Y + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(firstLine.getPoint2().X - secondLine.getPoint1().X) < Config.getRadius() && Math.Abs(firstLine.getPoint2().Y - secondLine.getPoint1().Y) < Config.getRadius())
            {
                firstLine.setPoint2(secondLine.getPoint1().X + Config.getMarginLeftPanel(), secondLine.getPoint1().Y + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(firstLine.getPoint1().X - secondLine.getPoint1().X) < Config.getRadius() && Math.Abs(firstLine.getPoint1().Y - secondLine.getPoint1().Y) < Config.getRadius())
            {
                firstLine.setPoint1(secondLine.getPoint1().X + Config.getMarginLeftPanel(), secondLine.getPoint1().Y + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(firstLine.getPoint1().X - secondLine.getPoint2().X) < Config.getRadius() && Math.Abs(firstLine.getPoint1().Y - secondLine.getPoint2().Y) < Config.getRadius())
            {
                firstLine.setPoint1(secondLine.getPoint2().X + Config.getMarginLeftPanel(), secondLine.getPoint2().Y + Config.getMarginTopPanel());
                result = true;
            }
            return result;
        }

        // Копирует главную линию
        public static Line copyLine(Line line, string name, Point point)
        {
            Line newLine = new Line();
            newLine.X1 = point.X - Config.getMarginLeftPanel();
            newLine.Y1 = point.Y - Config.getMarginTopPanel();
            newLine.X2 = newLine.X1 + Math.Abs(line.X2 - line.X1);
            newLine.Y2 = newLine.Y1 + Math.Abs(line.Y2 - line.Y1);
            newLine.Stroke = line.Stroke;
            newLine.StrokeThickness = line.StrokeThickness;
            newLine.StrokeDashArray = line.StrokeDashArray;
            newLine.Name = name;
            return newLine;
        }

        // метод, создающий дальнюю от конца линию по шаблону
        public static Line patternEndDistantLine(Line line)
        {
            Line newDistantLine = new Line();
            newDistantLine.X1 = line.X2 - 20;
            newDistantLine.Y1 = line.Y2 - 10;
            newDistantLine.X2 = line.X2 - 20;
            newDistantLine.Y2 = line.Y2 + 10;
            newDistantLine.Stroke = Brushes.Black;
            newDistantLine.StrokeThickness = 1;
            return newDistantLine;
        }

        // метод, создающий дальнюю от начала линию по шаблону
        public static Line patternBeginDistantLine(Line line)
        {
            Line newDistantLine = new Line();
            newDistantLine.X1 = line.X1 + 20;
            newDistantLine.Y1 = line.Y1 + 10;
            newDistantLine.X2 = line.X1 + 20;
            newDistantLine.Y2 = line.Y1 - 10;
            newDistantLine.Stroke = Brushes.Black;
            newDistantLine.StrokeThickness = 1;
            return newDistantLine;
        }

        // метод, создающий ближнюю от конца линию по шаблону
        public static Line patternEndNearLine(Line line)
        {
            Line newNearLine = new Line();
            newNearLine.X1 = line.X2 - 10;
            newNearLine.Y1 = line.Y2 - 10;
            newNearLine.X2 = line.X2 - 10;
            newNearLine.Y2 = line.Y2 + 10;
            newNearLine.Stroke = Brushes.Black;
            newNearLine.StrokeThickness = 1;
            return newNearLine;
        }

        // метод, создающий ближнюю от начала линию по шаблону
        public static Line patternBeginNearLine(Line line)
        {
            Line newNearLine = new Line();
            newNearLine.X1 = line.X1 + 10;
            newNearLine.Y1 = line.Y1 + 10;
            newNearLine.X2 = line.X1 + 10;
            newNearLine.Y2 = line.Y1 - 10;
            newNearLine.Stroke = Brushes.Black;
            newNearLine.StrokeThickness = 1;
            return newNearLine;
        }

        // метод, создающий ломанную линию на конце стрелки по шаблону
        public static Polyline patternEndPolyline(Line line)
        {
            Polyline newPolyline = new Polyline();
            newPolyline.Points.Add(new Point(line.X2, line.Y2 - 10));
            newPolyline.Points.Add(new Point(line.X2 - 20, line.Y2));
            newPolyline.Points.Add(new Point(line.X2, line.Y2 + 10));

            newPolyline.Stroke = Brushes.Black;
            newPolyline.StrokeThickness = 1;
            return newPolyline;
        }

        // метод, создающий ломанную линию в начале стрелки по шаблону
        public static Polyline patternBeginPolyline(Line line)
        {
            Polyline newPolyline = new Polyline();
            newPolyline.Points.Add(new Point(line.X1, line.Y2 - 10));
            newPolyline.Points.Add(new Point(line.X1 + 20, line.Y2));
            newPolyline.Points.Add(new Point(line.X1, line.Y2 + 10));

            newPolyline.Stroke = Brushes.Black;
            newPolyline.StrokeThickness = 1;
            return newPolyline;
        }
    }
}