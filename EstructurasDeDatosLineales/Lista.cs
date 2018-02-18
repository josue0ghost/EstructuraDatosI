using System;
using System.Collections;
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

    public class Lista<T> : IEnumerable<T>
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

        public Nodo<T> Insertar(int Indice, T obj)
        {
            if (Indice < 0)
                throw new ArgumentOutOfRangeException("Indice: " + Indice);

            if (Indice > Size)
                Indice = Size;

            Nodo<T> NodoActual = new Nodo<T>
            {
                value = obj,
                next = null
            };

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

            return NodoActual;
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
                Indice = this.Size - 1;

            if (First.next == null)
            {
                First = null;
            }
            else if (Indice == 0)
            {
                Nodo<T> Actual = this.First;
                First = Actual.next;
                Actual = null;
            }
            else if (Indice == Size)
            {
                Nodo<T> Final = this.First;

                while (Final.next.next != null)
                    Final = Final.next;

                Nodo<T> Elimina = Final.next;
                Final.next = null;
                Elimina = null;
            }
            else if (Indice < Size)
            {
                Nodo<T> Actual = this.First;
                for (int i = 0; i < Indice - 1; i++)
                {
                    Actual = Actual.next;
                }
                Nodo<T> Elimina = Actual.next;
                Actual.next = Actual.next.next;
                Elimina = null;
            }


            this.Size--;
        }

        public void Limpiar()
        {
            this.First = null;
            this.Size = 0;
        }

        public int IndexOf(T dato)
        {
            Nodo<T> Actual = this.First;

            for (int i = 0; i < this.Size; i++)
            {
                if (Actual.value.Equals(dato))
                {
                    return i;
                }
                Actual = Actual.next;
            }
            return -1;
        }

        public bool Remove(T item)
        {
            int Index = this.IndexOf(item);
            if (Index >= 0)
            {
                this.Eliminar(Index);
                return true;
            }
            return false;
        }

        public bool Contiene(T dato)
        {
            return this.IndexOf(dato) >= 0;
        }

        public Nodo<T> Obtener(int Indice)
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

            return Actual;
        }

        public object this[int Indice]
        {
            get { return this.Obtener(Indice); }
        }

        public Lista<T> Where(Func<T, bool> delegado)
        {
            var filtered = new Lista<T>();
            var current = First;
            while (current != null)
            {
                if (delegado.Invoke(current.value))
                {
                    filtered.Insertar(current.value);
                }
                current = current.next;
            }
            return filtered;
        }

        //public Nodo<T> Find(object dato)
        //{
        //    Nodo<T> nActual = this.First;
        //    while (nActual.next != null)
        //    {
        //        if (true)
        //        {
        //            return nActual;
        //        }
        //        nActual = nActual.next;
        //    }
        //    return null;
        //}
        public Lista<T> Find(Func<T, bool> delegado)
        {
            var filtered = new Lista<T>();
            var current = First;
            while (current != null)
            {
                if (delegado.Invoke(current.value))
                {
                    filtered.Insertar(current.value);
                }
                current = current.next;
            }
            return filtered;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var node = First;
            while (node != null)
            {
                yield return node.value;
                node = node.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
