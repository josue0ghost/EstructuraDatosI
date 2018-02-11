using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeDatosLineales
{
    public class Nodo<T>
    {
        public T value;
        public Nodo<T> next;
    }

    public class Lista<T>
    {
        public Nodo<T> First;
        int Size;

        public Lista()
        {
            this.First = null;
            this.Size = 0;
        }

        public bool Vacio
        {
            get { return this.Size == 0; }
        }

        public int Count
        {
            get { return this.Size; }
        }

        public Nodo<T> UltimoElemento()
        {
            Nodo<T> nTemp = First;

            while (nTemp.next != null)
            {
                nTemp = nTemp.next;
            }

            return nTemp;
        }

        public void Insertar(int Indice, object T)
        {
            if (Indice < 0)
                throw new ArgumentOutOfRangeException("Indice: " + Indice);

            if (Indice > Size)
                Indice = Size;

            Nodo<T> NodoActual = new Nodo<T> { next = null };

            if (this.Vacio || Indice == 0)
            {
                this.First = NodoActual;
            }
            else
            {
                Nodo<T> NodoFinal = this.First;

                while (NodoFinal.next != null)
                    NodoFinal = NodoFinal.next;

                NodoFinal.next = NodoActual;
            }
            Size++;
        }

        public void Insertar(T dato)
        {
            this.Insertar(Size, dato);
        }

        public void Eliminar(int Indice)
        {
            if (Indice < 0)
                throw new ArgumentOutOfRangeException("Indice: " + Indice);

            if (Indice >= this.Size)
                Indice = Size - 1;

            if (First.next == null)
            {
                First = null;
            }
            else if (Indice >= Size)
            {
                Nodo<T> Final = First;

                while (Final.next.next != null)
                    Final = Final.next;

                Nodo<T> Elimina = Final.next;
                Final.next = null;
                Elimina = null;
            }

            Size--;
        }

        public void Limpiar()
        {
            this.First = null;
            this.Size = 0;
        }

        public int IndexOf(object dato)
        {
            Nodo<T> Actual = this.First;

            for (int i = 0; i < this.Size; i++)
            {
                if (Actual.value.Equals(dato))
                    return i;

                Actual = Actual.next;
            }
            return -1;
        }

        public bool Contiene(object dato)
        {
            return this.IndexOf(dato) >= 0;
        }

        public object Obtener(int Indice)
        {
            if (Indice < 0)
                throw new ArgumentOutOfRangeException("Indice: " + Indice);

            if (this.Vacio)
                return null;

            if (Indice >= this.Size)
                Indice = this.Size - 1;

            Nodo<T> Actual = this.First;

            for (int i = 0; i < Indice; i++)
                Actual = Actual.next;
            
            return Actual.value;
        }

        public object this[int Indice]
        {
            get { return this.Obtener(Indice); }
        }
    }
}