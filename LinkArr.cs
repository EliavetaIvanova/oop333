using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    // класс, связывающий стрелки с окном
    class LinkArr
    {
        // указывает какая сторона стрелки связана с окном
        bool indexSide;
        // указывает какая стрлка связана с окном
        Arrow line;
        // указывает какая сторона окна связана со стрелкой
        char location;

        // конструктор класса
        public LinkArr(bool indexSide, Arrow line, char location)
        {
            this.indexSide = indexSide;
            this.line = line;
            this.location = location;
        }

        // геттер
        public bool getIndesSide()
        {
            return this.indexSide;
        }

        // геттер
        public Arrow getLine()
        {
            return this.line;
        }

        // геттер
        public char getLocation()
        {
            return this.location;
        }
    }
}
