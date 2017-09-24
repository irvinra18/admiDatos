using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.Clases
{
    public class Punto
    {
        //atributos
        private int x;
        private int y;
        private bool isActive;

        //metodos
        public Punto() {
            this.x = 0;
            this.y = 0;
            this.isActive = false;
        }
        public Punto(int x) { this.x = x;

            this.y = 0;
            this.isActive = false;
        }

        public Punto(int x, int y) {
            this.x = x;
            this.y = y;
            this.isActive = false;
        }

        public Punto(int x, int y, bool isActive) {
            this.x = x;
            this.y = y;
            this.isActive = isActive;
        }

        public int getX() {
            return this.x; }
        public int getY() {
            return this.y; }
        public bool getIsActive() { return this.isActive; }
        public void setX(int x) { this.x = x; }
        public void setY(int y) { this.y = y; }
        public void setIsActivate(bool isActive) { this.isActive = isActive; }

        public int[] getVector() { int[] pts = {this.x,this.y};
            return pts;
        }

        public override string ToString() {
            return string.Format("X: {0}nY: {1}", x, y);

        }

    }
}
