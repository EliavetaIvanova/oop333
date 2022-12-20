using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Editor
{
    // пользовательский класс окна
    class Window
    {
        // координаты точки внутри окна при перемещении этого окна
        private Point offset;
        // рамка окна
        private Border border;
        // связанные с окном стрелки
        private List<LinkArr> linkArrows;

        // конструктор класса
        public Window(Border border)
        {
            this.border = border;
            offset = new Point();
            linkArrows = new List<LinkArr>();
        }

        // координата располодения окна слева (отступ)
        public double getMarginLeft()
        {
            return this.border.Margin.Left;
        }

        // координата располодения окна сверху (отступ)
        public double getMarginTop()
        {
            return this.border.Margin.Top;
        }

        // ширина окна
        public double getWidth()
        {
            return this.border.Width;
        }

        // метод, соединяющий окно с линией
        public bool concateWindowWithLine(Arrow line)
        {
            bool result = false;
            if (Math.Abs(line.getPoint2().X - (this.border.Margin.Left + this.border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint2().Y - this.border.Margin.Top) < Config.getRadius())
            {
                line.setPoint2(this.border.Margin.Left + this.border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(line.getPoint2().X - (border.Margin.Left + border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint2().Y - (border.Margin.Top + border.Height)) < Config.getRadius())
            {
                line.setPoint2(this.border.Margin.Left + this.border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + this.border.Height + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(line.getPoint1().X - (border.Margin.Left + border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint1().Y - border.Margin.Top) < Config.getRadius())
            {
                line.setPoint1(this.border.Margin.Left + this.border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(line.getPoint1().X - (border.Margin.Left + border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint1().Y - (border.Margin.Top + border.Height)) < Config.getRadius())
            {
                line.setPoint1(this.border.Margin.Left + border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(line.getPoint1().X - border.Margin.Left) < Config.getRadius() && Math.Abs(line.getPoint1().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                line.setPoint1(this.border.Margin.Left + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(line.getPoint1().X - (border.Margin.Left + border.Width)) < Config.getRadius() && Math.Abs(line.getPoint1().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                line.setPoint1(this.border.Margin.Left + border.Width + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(line.getPoint2().X - border.Margin.Left) < Config.getRadius() && Math.Abs(line.getPoint2().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                line.setPoint2(this.border.Margin.Left + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                result = true;
            }
            else if (Math.Abs(line.getPoint2().X - (border.Margin.Left + border.Width)) < Config.getRadius() && Math.Abs(line.getPoint2().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                line.setPoint2(this.border.Margin.Left + border.Width + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                result = true;
            }

            return result;
        }

        // метод, добавляющий связанные с окном стрелки в массив
        public void addLinkingArrowsInList(Arrow line)
        {
            if (Math.Abs(line.getPoint2().X - (this.border.Margin.Left + this.border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint2().Y - this.border.Margin.Top) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(false, line, 'T'));
            }
            else if (Math.Abs(line.getPoint2().X - (border.Margin.Left + border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint2().Y - (border.Margin.Top + border.Height)) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(false, line, 'B'));
            }
            else if (Math.Abs(line.getPoint1().X - (border.Margin.Left + border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint1().Y - border.Margin.Top) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(true, line, 'T'));
            }
            else if (Math.Abs(line.getPoint1().X - (border.Margin.Left + border.Width / 2)) < Config.getRadius() && Math.Abs(line.getPoint1().Y - (border.Margin.Top + border.Height)) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(true, line, 'B'));
            }
            else if (Math.Abs(line.getPoint1().X - border.Margin.Left) < Config.getRadius() && Math.Abs(line.getPoint1().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(true, line, 'L'));
            }
            else if (Math.Abs(line.getPoint1().X - (border.Margin.Left + border.Width)) < Config.getRadius() && Math.Abs(line.getPoint1().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(true, line, 'R'));
            }
            else if (Math.Abs(line.getPoint2().X - border.Margin.Left) < Config.getRadius() && Math.Abs(line.getPoint2().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(false, line, 'L'));
            }
            else if (Math.Abs(line.getPoint2().X - (border.Margin.Left + border.Width)) < Config.getRadius() && Math.Abs(line.getPoint2().Y - (border.Margin.Top + border.Height / 2)) < Config.getRadius())
            {
                linkArrows.Add(new LinkArr(false, line, 'R'));
            }
        }

        // метод, перемещающий связанные с окном стрелки
        private void moveArrows()
        {
            foreach (LinkArr link in linkArrows)
            {
                if (link.getIndesSide()) // начало стрелки
                {
                    switch (link.getLocation())
                    {
                        case 'T':
                            link.getLine().setPoint1(this.border.Margin.Left + this.border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + Config.getMarginTopPanel());
                            break;
                        case 'B':
                            link.getLine().setPoint1(this.border.Margin.Left + border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height + Config.getMarginTopPanel());
                            break;
                        case 'L':
                            link.getLine().setPoint1(this.border.Margin.Left + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                            break;
                        case 'R':
                            link.getLine().setPoint1(this.border.Margin.Left + border.Width + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                            break;
                    }
                }
                else // конец стрелки
                {
                    switch (link.getLocation())
                    {
                        case 'T':
                            link.getLine().setPoint2(this.border.Margin.Left + this.border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + Config.getMarginTopPanel());
                            break;
                        case 'B':
                            link.getLine().setPoint2(this.border.Margin.Left + border.Width / 2 + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height + Config.getMarginTopPanel());
                            break;
                        case 'L':
                            link.getLine().setPoint2(this.border.Margin.Left + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                            break;
                        case 'R':
                            link.getLine().setPoint2(this.border.Margin.Left + border.Width + Config.getMarginLeftPanel(), this.border.Margin.Top + border.Height / 2 + Config.getMarginTopPanel());
                            break;
                    }
                }
            }
        }

        // очищает массив связанных стрелок
        public void clearLinkArrows()
        {
            this.linkArrows.Clear();
        }

        // проверяет находится ли окно под курсором
        public bool equalWindowWithPoint(Point point)
        {
            bool result = false;
            if (point.X - Config.getMarginLeftPanel() >= this.border.Margin.Left && point.X - Config.getMarginLeftPanel() <= this.border.Margin.Left + this.border.Width &&
    point.Y - Config.getMarginTopPanel() >= this.border.Margin.Top && point.Y - Config.getMarginTopPanel() <= this.border.Margin.Top + this.border.Height)
            {
                offset.X = point.X - this.border.Margin.Left;
                offset.Y = point.Y - this.border.Margin.Top;
                result = true;
            }
            return result;
        }

        // метод, перемещающий окно в точку
        public void move(Point point)
        {
            this.border.Margin = new Thickness(point.X - offset.X, point.Y - offset.Y, 0, 0);
            this.moveArrows();
        }

        // метод, удаляющий окно с поля для рисования
        public void remove()
        {
            Canvas canvas = (Canvas)this.border.Parent;
            linkArrows.Clear();
            canvas.Children.Remove(this.border);
        }
    }
}
