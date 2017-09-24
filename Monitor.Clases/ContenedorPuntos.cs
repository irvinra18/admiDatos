using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Monitor.Clases
{
    public class ContenedorPuntos
    {
        //atributos
        private Punto[] pts;
        private int nivel;

        //metodos
        public ContenedorPuntos() {
            this.pts = new Punto[60];
            this.nivel = 1;

            for (int i = 0; i < pts.Length; i++) {
                pts[i] = new Punto();
            }
            pts[0].setIsActivate(true);
            
        }
        public Punto[] getPuntos() { return pts; }
        public int getNivel() { return nivel; }
        public void setNivel(int nivel) { this.nivel = nivel; }
        public bool isFull() { if (pts[59].getIsActive()) return true;
            return false;
        }

        public Punto GetIndex(int i) { return pts[i]; }

      /*  public void Add(int ptY) {
            if (this.isFull()) {
                for (int i = 0; i < pts.Length-1; i++) {
                    pts[i + 1].setY(pts[i].getY());
                }
                pts[0].setY(ptY);
                
            }
            else {

                if (nivel != 0) 
                
                {
                    int aux=pts[0].getY();
                    for (int i = 0; i < nivel; i++)
                    {
                        if (pts[i + 1].getIsActive())
                        {
                            aux = pts[i + 1].getY();
                            pts[i + 1].setY(pts[i].getY());

                        }
                        else
                        {
                            pts[i + 1].setY(aux);
                        }
                    }
                }
                this.pts[0].setY(ptY);
                this.pts[nivel].setIsActivate(true);
                this.nivel++;
            }

        }
        */
        public void Add(int y) {
            if (nivel == 60)
            {
                for (int i = nivel - 1; i > 0; i--) {
                    pts[i - 1].setY(pts[i].getY());
                }

            }
            else {
                pts[nivel].setY(y);
                pts[nivel].setIsActivate(true);
                nivel++;
            }
            pts[nivel-1].setY(y);


        }
        public void PlusPlus() {
            int aux = nivel;
            for (int i = 0; i < nivel; i++) {
                pts[i].setX(aux-1);
                aux--;
            }
        }



    }
}
