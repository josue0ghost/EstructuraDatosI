using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeDatosLineales
{
    internal class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
    class Tree<T> where T : IComparable
    {
        private BinaryTreeNode<T> Root { get; set; }
        public void Add(T value)
        {
            //recorrer árbol buscando posición
            var current = Root;
            if(current.Value.CompareTo(value) > 0)
            {
                //der
            }
            else
            {
                //izq
            }
            //insertar
        }
    }
}
