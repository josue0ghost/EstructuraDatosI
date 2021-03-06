﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeDatosLineales
{
    public class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T value, BinaryTreeNode<T> Left, BinaryTreeNode<T> Right)
        {
            this.Value = value;
            this.Left = Left;
            this.Right = Right;
        }

        public BinaryTreeNode(T value) : this(value, null, null) { }

        public bool IsLeaf() { return Left == null && Right == null; }

        public bool Full() { return Left != null && Right != null; } //Necesario?
    }

    public class Tree<T> where T : IComparable
    {
        public BinaryTreeNode<T> Root { get; set; }

        public Tree() { Root = null; }

        public void Insert(T value)
        {
            BinaryTreeNode<T> NewNode = new BinaryTreeNode<T>(value);
            if (Root == null)
            {
                Root = NewNode;
            }
            else
            {
                InsertarHijo(NewNode, Root);
            }
        }

        private void InsertarHijo(BinaryTreeNode<T> nNuevo, BinaryTreeNode<T> nPadre)
        {
            if (nPadre != null)
            {
                if (nNuevo.Value.CompareTo(nPadre.Value) <= 0)
                {
                    if (nPadre.Left == null)
                    {
                        nPadre.Left = nNuevo;
                    }
                    else
                    {
                        InsertarHijo(nNuevo, nPadre.Left);
                    }
                }
                else
                {
                    if (nNuevo.Value.CompareTo(nPadre.Value) > 0)
                    {
                        if (nPadre.Right == null)
                        {
                            nPadre.Right = nNuevo;
                        }
                        else
                        {
                            InsertarHijo(nNuevo, nPadre.Right);
                        }
                    }
                }
            }
        }

        public BinaryTreeNode<T> Eliminar(T valor)
        {
            BinaryTreeNode<T> nAux = Root;
            BinaryTreeNode<T> nPadre = Root;
            bool isLeftLeaf = true;

            while (nAux.Value.CompareTo(valor) != 0)
            {
                nPadre = nAux;
                if (valor.CompareTo(nAux.Value) <= 0)
                {
                    isLeftLeaf = true;
                    nAux = nAux.Left;
                }
                else
                {
                    isLeftLeaf = false;
                    nAux = nAux.Left;
                }

                if (nAux == null)
                    return null;
            }

            if (nAux.Left == null && nAux.Right == null)
            {
                if (nAux == Root)
                {
                    Root = null;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = null;
                }
                else
                {
                    nPadre.Right = null;
                }
            }
            else if (nAux.Right == null)
            {
                if (nAux == Root)
                {
                    Root = nAux.Left;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = nAux.Left;
                }
                else
                {
                    nPadre.Right = nAux.Left;
                }
            }
            else if (nAux.Left == null)
            {
                if (nAux == Root)
                {
                    Root = nAux.Right;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = nAux.Right;
                }
                else
                {
                    nPadre.Right = nAux.Right;
                }
            }
            else
            {
                BinaryTreeNode<T> nReplace = Replace(nAux);
                if (nAux == Root)
                {
                    Root = nReplace;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = nReplace;
                }
                else
                {
                    nPadre.Right = nReplace;
                }
                nReplace.Left = nAux.Left;
            }

            return nAux;
        }

        private BinaryTreeNode<T> Replace(BinaryTreeNode<T> nElimina)
        {
            BinaryTreeNode<T> rPadre = nElimina;
            BinaryTreeNode<T> rReplace = nElimina;
            BinaryTreeNode<T> Aux = nElimina.Right;
            while (Aux != null)
            {
                rPadre = rReplace;
                rReplace = Aux;
                Aux = Aux.Left;
            }
            if (rReplace != nElimina.Right)
            {
                rPadre.Left = rReplace.Right;
                rReplace.Right = nElimina.Right;
            }
            return rReplace;
        }

        public BinaryTreeNode<T> Find(T value)
        {
            BinaryTreeNode<T> Aux = Root;
            while (Aux.Value.CompareTo(value) != 0)
            {
                if (value.CompareTo(Aux.Value) < 0)
                {
                    Aux = Aux.Left;
                }
                else
                {
                    Aux = Aux.Right;
                }

                if (Aux == null)
                    return null;
            }

            return Aux;
        }

        bool IsEmpty()
        {
            return Root == null;
        }


        private void InOrder(BinaryTreeNode<T> Root, ref List<T> Elements)
        {
            if (Root != null)
            {
                InOrder(Root.Left, ref Elements);
                Elements.Add(Root.Value);
                InOrder(Root.Right, ref Elements);
            }
        }

        private void PostOrder(BinaryTreeNode<T> Root, ref List<T> Elements)
        {
            if (Root != null)
            {
                PostOrder(Root.Left, ref Elements);
                PostOrder(Root.Right, ref Elements);
                Elements.Add(Root.Value);
            }
        }

        private void PreOrder(BinaryTreeNode<T> Root, ref List<T> Elements)
        {
            if (Root != null)
            {
                Elements.Add(Root.Value);
                PreOrder(Root.Left, ref Elements);
                PreOrder(Root.Right, ref Elements);
            }
        }

        public List<T> Orders(string Order)
        {
            List<T> Elements = new List<T>();
            switch (Order)
            {
                case "PreOrder":
                    PreOrder(Root, ref Elements);
                    break;
                case "InOrder":
                    InOrder(Root, ref Elements);
                    break;
                case "PostOrder":
                    PostOrder(Root, ref Elements);
                    break;
            }
            return Elements;
        }
    }

}
