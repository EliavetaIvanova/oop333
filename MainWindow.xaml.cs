using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor
{
    /// Главный класс приложения
    public partial class MainWindow : System.Windows.Window
    {
        // префикс стрлки, который будет указываться при создании стрелки на поле
        private string nameArrow = "arrow_";
        // указывает за какую сторону в данный момент тянем стрелку
        bool indexSideArrow;
        // указывает индекс стрелки в массиве, которую в данный момент тянем
        int indexCurrentArrow;
        // указывает индекс окна в массиве, которое в данный момент передвигаем
        int indexCurrentWindow;
        // массив стрелок на поле
        List<Arrow> myArrows;
        // массив окон на поле
        List<Window> myWindows;

        // конструктор класса(инициализация переменных)
        public MainWindow()
        {
            InitializeComponent();
            double radius = 20.0;
            Config.setConfig(radius, borderdrawPanel.Margin.Top, borderdrawPanel.Margin.Left);

            indexSideArrow = false;
            indexCurrentArrow = -1;
            indexCurrentWindow = -1;
            myArrows = new List<Arrow>();
            myWindows = new List<Window>();
        }

        // событие передвидения курсора по полю для рисования
        private void drawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            // при нажатой кнопки
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (indexCurrentWindow != -1)
                {
                    // передвигаем окно
                    myWindows[indexCurrentWindow].move(e.GetPosition(this));
                }
                else if (indexCurrentArrow != -1)
                {
                    // тянем стрелку за её начало
                    if (indexSideArrow)
                    {
                        // передвигаем стрелку
                        myArrows[indexCurrentArrow].setPoint1(e.GetPosition(this));
                    }
                    else // тянем стрелку за её конец
                    {
                        // передвигаем стрелку
                        myArrows[indexCurrentArrow].setPoint2(e.GetPosition(this));
                    }
                    // перерисовываем поля для рисования
                    drawPanel.InvalidateVisual();
                }
            }
            else // при отжатой кнопки
            {
                indexCurrentArrow = -1;
                indexCurrentWindow = -1;

                for (int count = 0; count < myWindows.Count; count++)
                {
                    // проверяем навели ли курсором на окно
                    if (myWindows[count].equalWindowWithPoint(e.GetPosition(this)))
                    {
                        indexCurrentWindow = count;
                        myWindows[indexCurrentWindow].clearLinkArrows();
                        foreach (Arrow myLine in myArrows)
                        {
                            myWindows[indexCurrentWindow].addLinkingArrowsInList(myLine);
                        }
                        break;
                    }
                }

                if (indexCurrentWindow == -1)
                {
                    int numberPointLineIndex = 0;
                    // проверяем навели ли курсором на один из концов стрелки
                    for (int count = 0; count < myArrows.Count; count++)
                    {
                        numberPointLineIndex = myArrows[count].equalLineWithPoint(e.GetPosition(this));
                        if (numberPointLineIndex == 1)
                        {
                            indexCurrentArrow = count;
                            indexSideArrow = true;
                            break;
                        }
                        else if (numberPointLineIndex == 2)
                        {
                            indexCurrentArrow = count;
                            indexSideArrow = false;
                            break;
                        }
                    }
                }
            }
        }

        // Событие при котором отпустили кнопку мыши на поле для рисования
        private void drawPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (indexCurrentArrow != -1)
            {
                // Если по близости с линией, которую только что перетаскивали есть
                // другие линии, то соединяем их
                foreach (Arrow line in myArrows)
                {
                    if (!line.equalName(myArrows[indexCurrentArrow].name()))
                    {
                        Arrow.concateLines(myArrows[indexCurrentArrow], line);
                    }
                }

                int count = 0;
                // Если по близости с линией, которую только что перетаскивали есть
                // окна, то соединяем их
                foreach (Window window in myWindows)
                {
                    if (window.concateWindowWithLine(myArrows[indexCurrentArrow]))
                    {
                        ++count;
                    }

                    if (count >= 2)
                    {
                        break;
                    }
                }
            }
        }

        // Событие DragAndDrop (когда отпускают кнопку мыши при перетаскивании объекта)
        private void drawPanel_Drop(object sender, DragEventArgs e)
        {
            string[] str = (string[])e.Data.GetFormats();
            // Если перетаскивали стрелку
            if (str[0] == "System.Windows.Shapes.Line")
            {
                Line line = (Line)e.Data.GetData("System.Windows.Shapes.Line");
                Canvas canvas = (Canvas)line.Parent;

                Line newLine = Arrow.copyLine(line, nameArrow + myArrows.Count, e.GetPosition(this));
                Line newEndDistantLine = Arrow.patternEndDistantLine(newLine);
                Line newBeginDistantLine = Arrow.patternBeginDistantLine(newLine);

                // Если стрелка первого типа, создаем её шаблон
                if (line.Name == "Arrow_type_1")
                {
                    Line newEndNearLine = Arrow.patternEndNearLine(newLine);
                    Line newBeginNearLine = Arrow.patternBeginNearLine(newLine);
                    myArrows.Add(new MyArrowType1(newLine, newEndDistantLine, newBeginDistantLine, newEndNearLine, newBeginNearLine));
                    drawPanel.Children.Add(newEndNearLine);
                    drawPanel.Children.Add(newBeginNearLine);
                    Canvas.SetZIndex(newEndNearLine, 0);
                    Canvas.SetZIndex(newBeginNearLine, 0);
                }
                // Если стрелка второго типа, создаем её шаблон
                else if (line.Name == "Arrow_type_2")
                {
                    Polyline newPolyline = Arrow.patternEndPolyline(newLine);
                    Line newBeginNearLine = Arrow.patternBeginNearLine(newLine);
                    myArrows.Add(new Arrow2(newLine, newEndDistantLine, newBeginDistantLine, newBeginNearLine, newPolyline));
                    drawPanel.Children.Add(newPolyline);
                    drawPanel.Children.Add(newBeginNearLine);
                    Canvas.SetZIndex(newPolyline, 0);
                    Canvas.SetZIndex(newBeginNearLine, 0);
                }
                // Если стрелка третьего типа, создаем её шаблон
                else if (line.Name == "Arrow_type_3")
                {
                    Polyline newEndPolyline = Arrow.patternEndPolyline(newLine);
                    Polyline newBeginPolyline = Arrow.patternBeginPolyline(newLine);
                    myArrows.Add(new Arrow3(newLine, newEndDistantLine, newBeginDistantLine, newEndPolyline, newBeginPolyline));
                    drawPanel.Children.Add(newEndPolyline);
                    drawPanel.Children.Add(newBeginPolyline);
                    Canvas.SetZIndex(newEndPolyline, 0);
                    Canvas.SetZIndex(newBeginPolyline, 0);
                }

                drawPanel.Children.Add(newLine);
                drawPanel.Children.Add(newEndDistantLine);
                drawPanel.Children.Add(newBeginDistantLine);
                Canvas.SetZIndex(newLine, 0);
                Canvas.SetZIndex(newEndDistantLine, 0);
                Canvas.SetZIndex(newBeginDistantLine, 0);
            }
            // Если перетаскивали окно, создаем его шаблон
            else if (str[0] == "System.Windows.Controls.Border")
            {
                Border border = (Border)e.Data.GetData("System.Windows.Controls.Border");
                Border newBorder = copyBorder(border, e.GetPosition(this));
                myWindows.Add(new Window(newBorder));
                drawPanel.Children.Add(newBorder);
                Canvas.SetZIndex(newBorder, 3);
                Canvas.SetZIndex(newBorder.Child, 2);
            }
        }

        // Добавление эффект DragAndDrop, при перетаскивании объекта на поле для рисования
        private void drawPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        // Добавление стрелки в буфер обмена DragAndDrop
        private void arrow_canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            Line line = null;
            foreach (Line obj in canvas.Children.OfType<Line>())
            {
                line = obj;
                break;
            }

            DragDrop.DoDragDrop(canvas, line, DragDropEffects.Copy);
        }

        // Событие создания нового текстового поля (аттрибута) в окне, при нажатии
        // кнопки Enter внутри самого верхнего текстового поля (аттрибута)
        public void mainTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                StackPanel stackPanel = (StackPanel)textBox.Parent;
                TextBox newTextBox = new TextBox();
                newTextBox.Text = "";
                newTextBox.PreviewKeyDown += new KeyEventHandler(TextBox_PreviewKeyDown);
                stackPanel.Children.Add(newTextBox);
                newTextBox.Focus();
            }
        }

        // Событие создания нового текстового поля (аттрибута) или удаление текущего в окне, при нажатии
        // кнопки Enter или delete, backspace cсоответственно внутри не самого верхнего текстового поля (аттрибута)
        public void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                StackPanel stackPanel = (StackPanel)textBox.Parent;
                TextBox newTextBox = new TextBox();
                newTextBox.Text = "";
                newTextBox.PreviewKeyDown += new KeyEventHandler(TextBox_PreviewKeyDown);
                stackPanel.Children.Add(newTextBox);
                newTextBox.Focus();
            }
            else if ((e.Key == Key.Back || e.Key == Key.Delete) && ((TextBox)sender).Text.Length == 0)
            {
                Console.WriteLine("key Back");
                TextBox textBox = (TextBox)sender;
                textBox.Text = "hi!";
                StackPanel stackPanel = (StackPanel)textBox.Parent;
                stackPanel.Children.Remove(textBox);
            }
        }

        // Добавление окна в буфер обмена DragAndDrop
        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            Border border = null;
            foreach (Border obj in canvas.Children.OfType<Border>())
            {
                border = obj;
                break;
            }
            DragDrop.DoDragDrop(canvas, border, DragDropEffects.Copy);
        }

        // Событие очистки поля рисования от стрелок и окон
        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            drawPanel.Children.Clear();
            myArrows.Clear();
            myWindows.Clear();
        }

        // метод создания нового окна
        private Border copyBorder(Border border, Point point)
        {
            Border newBorder = new Border();
            StackPanel stackPanel = (StackPanel)border.Child;
            newBorder.BorderThickness = border.BorderThickness;
            newBorder.Height = border.Height;
            newBorder.Width = border.Width;
            newBorder.BorderBrush = border.BorderBrush;
            newBorder.Background = Brushes.White;
            newBorder.CornerRadius = border.CornerRadius;

            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Margin = new Thickness(0, 0, 0, 0);

            TextBox newNameTextBox = new TextBox();
            newNameTextBox.Text = "Название";
            newNameTextBox.BorderBrush = Brushes.Black;
            newNameTextBox.BorderThickness = new Thickness(0, 0, 0, 1);
            newNameTextBox.FontWeight = FontWeights.Bold;
            newNameTextBox.Background = Brushes.Transparent;
            newNameTextBox.Padding = new Thickness(10, 10, 0, 0);
            newNameTextBox.FontSize = 14;
            newNameTextBox.FontFamily = new FontFamily("Times New Roman");
            newStackPanel.Children.Add(newNameTextBox);

            TextBox newMainNameAttributeTextBox = new TextBox();
            newMainNameAttributeTextBox.Text = "Атрибут";
            newMainNameAttributeTextBox.VerticalAlignment = VerticalAlignment.Center;
            newMainNameAttributeTextBox.Padding = new Thickness(5, 5, 0, 0);
            newMainNameAttributeTextBox.BorderBrush = Brushes.Black;
            newMainNameAttributeTextBox.FontSize = 14;
            newMainNameAttributeTextBox.BorderThickness = new Thickness(0, 0, 0, 0);
            newMainNameAttributeTextBox.FontFamily = new FontFamily("Times New Roman");
            newMainNameAttributeTextBox.PreviewKeyDown += new KeyEventHandler(mainTextBox_PreviewKeyDown);
            newStackPanel.Children.Add(newMainNameAttributeTextBox);

            newBorder.Child = newStackPanel;
            newBorder.Margin = new Thickness(point.X - Config.getMarginLeftPanel(), point.Y - Config.getMarginTopPanel(), 0, 0);
            return newBorder;
        }

        // Событие сохранения резулатата работы путем создания скриншота
        private async void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string filePath = "./Screenshot.png";
            RenderTargetBitmap renderTargetBitmap =
    new RenderTargetBitmap(1920, 1080, 0, 0, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawPanel);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            using (Stream fileStream = File.Create(filePath))
            {
                pngImage.Save(fileStream);
            }

            ToolTip tooltip = new ToolTip();
            Button button = (Button)sender;
            tooltip.Content = "Сохранение прошло успешно!";
            button.ToolTip = tooltip;
            tooltip.IsOpen = true;
            await Task.Delay(2000);
            tooltip.IsOpen = false;
        }

        // Событие удаления стрелки или окон, при перетаскивании их за пределы поля для рисования
        private void drawPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (indexCurrentWindow != -1)
            {
                myWindows[indexCurrentWindow].remove();
                myWindows.Remove(myWindows[indexCurrentWindow]);
                indexCurrentWindow = -1;
            }
            else if (indexCurrentArrow != -1)
            {
                myArrows[indexCurrentArrow].remove();
                myArrows.Remove(myArrows[indexCurrentArrow]);
                indexCurrentArrow = -1;
            }
        }
    }
}